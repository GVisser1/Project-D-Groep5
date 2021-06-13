using System.Collections.Generic;
using Bitfit.Models;
using Microsoft.AspNetCore.Components.Forms;
using Fitbit.Api.Portable.OAuth2;
using Fitbit.Models;
using Fitbit.Api.Portable;
using System;
using System.Diagnostics;

namespace Bitfit.Pages
{
    public partial class UserPage
    {
        protected List<User> AllUsers { get; set; }
        protected List<Schedule> AllSchedules { get; set; }
        protected List<Workout> AllWorkouts { get; set; }
        protected List<FitbitCreds> AllFitbitCreds { get; set; }

        private HeartActivitiesTimeSeries heartActivities;
        public static User CurrentUser;
        public static bool SignedIn;
        protected bool AddingUser { get; set; }
        public static OAuth2AccessToken AccessToken { get; set; }
        public FitbitClient Client { get; set; }
        public FitbitAppCredentials AppCredentials { get; set; }
        protected override void OnInitialized()
        {
            AllUsers = DbFunctions.GetUsers();
            AllSchedules = DbFunctions.GetSchedules();
            AllWorkouts = DbFunctions.GetWorkouts();
            AllFitbitCreds = DbFunctions.GetFitbitCreds();
            AppCredentials = new FitbitAppCredentials()
            {
                ClientId = "23B29X",
                ClientSecret = "4cc4197e96410a6d7007a1a63a4e3477"
            };
            StateHasChanged();
        }
        // Signs the selected user in and retrieves data from an Fitbit Account for the user which id is 1
        public async void SelectUser(User user)
        {
            CurrentUser = user;
            SchedulePage.CurrentSchedule = null;
            foreach (var schedule in AllSchedules)
            {
                if (schedule.Id == CurrentUser.ScheduleId)
                {
                    SchedulePage.CurrentSchedule = schedule;
                    break;
                }
            }
            SignedIn = true;
            if (SchedulePage.CurrentSchedule != null)
            {
                var TempWorkouts = DbFunctions.GetCurrentWorkouts();
                foreach (var workout in TempWorkouts)
                {
                    switch (workout.Type)
                    {
                        case "HIIT":
                            HiitPage.CurrentWorkout = workout;
                            break;
                        case "Endurance":
                            EndurancePage.CurrentWorkout = workout;
                            break;
                        case "Strength":
                            StrengthPage.CurrentWorkout = workout;
                            break;
                    }
                }
            }

            // Call this method to get an authorization code -> Check the debugger for a link and get the code from the url when it is entered in a browser
            //FitbitFunctions.Authorize();
            // Call this method to get an accestoken with token an refreshtoken -> Use the obtained authorization code from the Authorize method
            //AccessToken = await FitbitFunctions.ExchangeAuthCodeForAccessTokenAsync("d75dbc04465ab1ce13f4dc5fad825e9de220fc8f");  

            // If the Id of the selected user is 1, than the fitbit api is called and data is retrieved -> In this case the resting heart rate
            if (CurrentUser.Id == 1)
            {
                AccessToken = new OAuth2AccessToken
                {
                    Token = AllFitbitCreds[0].Token,
                    RefreshToken = AllFitbitCreds[0].RefreshToken,
                    Scope = "heartrate sleep activity nutrition settings social profile location weight",
                    ExpiresIn = 28800,
                    TokenType = "Bearer",
                    UserId = "9F9CGM"
                };
                Client = new FitbitClient(AppCredentials, AccessToken);

                // Accestoken is refreshed using the refreshtoken -> Database will be updated
                AccessToken = await FitbitFunctions.RefreshTokenAsync(Client, AllFitbitCreds);
                string query = $"UPDATE Fitbit " +
                    $"SET Token = '{AccessToken.Token}', RefreshToken = '{AccessToken.RefreshToken}' " +
                    $"WHERE Id = 1";
                DbFunctions.ExcQuery(query);
                Debug.WriteLine(AccessToken.Token + "\n" + AccessToken.RefreshToken + "\n");
                Client = new FitbitClient(AppCredentials, AccessToken);
                AllFitbitCreds = DbFunctions.GetFitbitCreds();

                // Retrieves the resting heart rate and updates the users data in the database
                heartActivities = await Client.GetHeartRateTimeSeries(DateTime.UtcNow, DateRangePeriod.OneWeek);
                if (heartActivities == null || heartActivities.HeartActivities == null)
                {
                    heartActivities = new HeartActivitiesTimeSeries
                    {
                        HeartActivities = new List<HeartActivities>()
                    };
                }
                // Gets the average resting heart rate of the last 7 days
                int days = 0; int restHeartRate = 0;
                foreach (var heartActivity in heartActivities.HeartActivities)
                {
                    if(heartActivity.RestingHeartRate != 0)
                    {
                        days++; restHeartRate += heartActivity.RestingHeartRate;
                    }
                }
                restHeartRate /= days;
                if (CurrentUser.Id == 1)
                {
                    CurrentUser.RestHeartRate = restHeartRate;
                    CurrentUser.Rank = Calculations.CalcEnduranceRank(CurrentUser);
                }
                query = $"UPDATE Users " +
                    $"SET RestHeartRate = '{restHeartRate}', Rank = {CurrentUser.Rank} " +
                    $"WHERE Id = 1";
                DbFunctions.ExcQuery(query);
                AllUsers = DbFunctions.GetUsers();
                StateHasChanged();
            }
        }
        // The current user is signed out
        public void SignOut()
        {
            CurrentUser = null;
            SignedIn = false;
            HiitPage.CurrentWorkout = null;
            EndurancePage.CurrentWorkout = null;
            StrengthPage.CurrentWorkout = null;
            ChallengePage.CurrentChallenge = null;
            ChallengeWorkoutPage.CurrentWorkout1 = null;
            ChallengeWorkoutPage.CurrentWorkout2 = null;
            ChallengeWorkoutPage.CurrentWorkout3 = null;
        }
        // Is called when a user is being added
        public void OnNewUser()
        {
            AddingUser = true;
            CurrentUser = new User();
        }
        // Adds a new user based on the inserted data
        public void AddUser(EditContext editContext)
        {
            CurrentUser.Rank = Calculations.CalcEnduranceRank(CurrentUser);
            string query = $"INSERT INTO Users (FullName, Gender, Age, RestHeartRate, Rank, ScheduleId)" +
                $"VALUES ('{CurrentUser.FullName}', '{CurrentUser.Gender}', {CurrentUser.Age}, {CurrentUser.RestHeartRate}, {CurrentUser.Rank}, -1)";
            DbFunctions.ExcQuery(query);
            AllUsers = DbFunctions.GetUsers();
            StateHasChanged();
            AddingUser = false;
            CurrentUser = null;
        }

    }
}
