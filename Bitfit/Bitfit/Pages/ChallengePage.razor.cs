using Bitfit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bitfit.Pages
{
    public partial class ChallengePage
    {
        protected List<Challenge> AllChallenges { get; set; }
        protected List<Schedule> AllSchedules { get; set; }
        public static Challenge CurrentChallenge { get; set; }
        public static Schedule ChallengeSchedule { get; set; }
        public static string[] FormattedDescription { get; set; }
        protected override void OnInitialized()
        {
            AllChallenges = DbFunctions.GetChallenges();
            AllSchedules = DbFunctions.GetSchedules();
            CurrentChallenge = null;
            StateHasChanged();
        }

        // Selects the chosen challenge
        public void SelectChallenge(Challenge challenge)
        {
            CurrentChallenge = challenge;
            FormattedDescription = CurrentChallenge.Description.Split(';');
            foreach (var schedule in AllSchedules)
            {
                if (schedule.Id == CurrentChallenge.ScheduleId)
                {
                    ChallengeSchedule = schedule;
                    break;
                }
            }
        }

        // Sets the selected challenge as the new schedule
        public void StartChallenge()
        {
            foreach (var schedule in AllSchedules)
            {
                UserPage.CurrentUser.ScheduleId = ChallengeSchedule.Id;
                if (schedule.Id == UserPage.CurrentUser.ScheduleId)
                {
                    schedule.Workout1Id = ChallengeSchedule.Workout1Id;
                    schedule.Workout2Id = ChallengeSchedule.Workout2Id;
                    schedule.Workout3Id = ChallengeSchedule.Workout3Id;

                    string query = $"UPDATE Schedules " +
                        $"SET Workout1Id = {schedule.Workout1Id}, Workout2Id = {schedule.Workout2Id}, Workout3Id = {schedule.Workout3Id} " +
                        $"WHERE Id = {schedule.Id}";
                    DbFunctions.ExcQuery(query);
                    query = $"UPDATE Users " +
                        $"SET ScheduleId = {UserPage.CurrentUser.ScheduleId} " +
                        $"WHERE Id = {UserPage.CurrentUser.Id}";
                    DbFunctions.ExcQuery(query);

                    SchedulePage.CurrentSchedule = schedule;
                    StateHasChanged();
                    break;
                }
            }
        }
    }
}