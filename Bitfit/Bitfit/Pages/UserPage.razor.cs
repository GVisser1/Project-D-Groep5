using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitfit.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Data;

namespace Bitfit.Pages
{
    public partial class UserPage
    {
        protected List<User> AllUsers { get; set; }
        protected List<Schedule> AllSchedules { get; set; }
        protected List<Workout> AllWorkouts { get; set; }
        public static User CurrentUser;
        public static bool SignedIn;
        protected bool AddingUser { get; set; }
        protected override void OnInitialized()
        {
            AllUsers = DbFunctions.GetUsers();
            AllSchedules = DbFunctions.GetSchedules();
            AllWorkouts = DbFunctions.GetWorkouts();
            StateHasChanged();
        }
        public void SelectUser(User user)
        {
            CurrentUser = user;
            foreach (var schedule in AllSchedules)
            {
                if(schedule.Id == CurrentUser.ScheduleId)
                {
                    SchedulePage.CurrentSchedule = schedule;
                    break;
                }
            }
            SignedIn = true;
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
        public void SignOut()
        {
            CurrentUser = null;
            SignedIn = false;
            HiitPage.CurrentWorkout = null;
            EndurancePage.CurrentWorkout = null;
            StrengthPage.CurrentWorkout = null;
        }
        public void OnNewUser()
        {
            AddingUser = true;
            CurrentUser = new User();
        }
        public void AddUser(EditContext editContext)
        {
            CurrentUser.Rank = Calculations.CalcEnduranceRank(CurrentUser);
            string query = $"INSERT INTO Users (FullName, Gender, Age, RestHeartRate, Rank, ScheduleId)" +
                $"VALUES ('{CurrentUser.FullName}', '{CurrentUser.Gender}', {CurrentUser.Age}, {CurrentUser.RestHeartRate}, {CurrentUser.Rank}, {CurrentUser.ScheduleId})";
            DbFunctions.ExcQuery(query);
            AllUsers = DbFunctions.GetUsers();
            StateHasChanged();
            AddingUser = false;
            CurrentUser = null;
        }

    }
}
