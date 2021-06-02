﻿using Bitfit.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitfit.Pages
{
    public partial class SchedulePage
    {
        protected List<Schedule> AllSchedules { get; set; }
        public static List<Workout> ScheduleWorkouts { get; set; }
        protected List<Workout> TempWorkouts { get; set; }
        public static Schedule CurrentSchedule;
        protected override void OnInitialized()
        {
            AllSchedules = DbFunctions.GetSchedules();
            ScheduleWorkouts = DbFunctions.GetCurrentWorkouts();
            StateHasChanged();
        }
        public void CreateSchedule()
        {
            SetWorkouts((DbFunctions.GetAvailableWorkouts(UserPage.CurrentUser.Rank)));
            CurrentSchedule = new Schedule
            {
                Id = UserPage.CurrentUser.ScheduleId,
                Rank = UserPage.CurrentUser.Rank,
                Workout1Id = ScheduleWorkouts[0].Id,
                Workout2Id = ScheduleWorkouts[1].Id,
                Workout3Id = ScheduleWorkouts[2].Id
            };
            string query = $"UPDATE Users " +
                $"SET ScheduleId = {CurrentSchedule.Id} " +
                $"WHERE Id = {UserPage.CurrentUser.Id}";
            DbFunctions.ExcQuery(query);
            query = $"INSERT INTO Schedules (Rank, Workout1Id, Workout2Id, Workout3Id)" +
                $"VALUES ({CurrentSchedule.Rank}, {CurrentSchedule.Workout1Id}, {CurrentSchedule.Workout2Id}, {CurrentSchedule.Workout3Id})";
            DbFunctions.ExcQuery(query);
            foreach (var workout in ScheduleWorkouts)
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
            AllSchedules = DbFunctions.GetSchedules();
            StateHasChanged();
        }
        public void RefreshSchedule()
        {
            SetWorkouts((DbFunctions.GetAvailableWorkouts(UserPage.CurrentUser.Rank)));
            CurrentSchedule.Workout1Id = ScheduleWorkouts[0].Id;
            CurrentSchedule.Workout2Id = ScheduleWorkouts[1].Id;
            CurrentSchedule.Workout3Id = ScheduleWorkouts[2].Id;
            string query = $"UPDATE Schedules " +
                $"SET Workout1Id = {CurrentSchedule.Workout1Id}, Workout2Id = {CurrentSchedule.Workout2Id}, Workout3Id = {CurrentSchedule.Workout3Id} " +
                $"WHERE Id = {CurrentSchedule.Id}";
            DbFunctions.ExcQuery(query);
            foreach (var workout in ScheduleWorkouts)
            {
                switch (workout.Type)
                {
                    case "HIIT":
                        HiitPage.CurrentWorkout = workout;
                        System.Diagnostics.Debug.WriteLine($"----------------- \n{workout.Type}\n ----------------");
                        break;
                    case "Endurance":
                        EndurancePage.CurrentWorkout = workout;
                        System.Diagnostics.Debug.WriteLine($"----------------- \n{workout.Type}\n ----------------");
                        break;
                    case "Strength":
                        StrengthPage.CurrentWorkout = workout;
                        System.Diagnostics.Debug.WriteLine($"----------------- \n{workout.Type}\n ----------------");
                        break;
                }
            }
            AllSchedules = DbFunctions.GetSchedules();
            StateHasChanged();
        }
        public void SetWorkouts(List<Workout> workouts)
        {
            var HiitWorkouts = new List<Workout>(); var EnduranceWorkouts = new List<Workout>(); var StrengthWorkouts = new List<Workout>();
            foreach (var workout in workouts)
            {
                switch (workout.Type)
                {
                    case "HIIT":
                        HiitWorkouts.Add(workout);
                        break;
                    case "Endurance":
                        EnduranceWorkouts.Add(workout);
                        break;
                    case "Strength":
                        StrengthWorkouts.Add(workout);
                        break;
                }
            }
            TempWorkouts = new List<Workout>
            {
                HiitWorkouts.ElementAt(new Random().Next(HiitWorkouts.Count)),
                EnduranceWorkouts.ElementAt(new Random().Next(EnduranceWorkouts.Count)),
                StrengthWorkouts.ElementAt(new Random().Next(StrengthWorkouts.Count))
            };

            ScheduleWorkouts = new List<Workout>();
            int length = TempWorkouts.Count;
            for(int i = 0; i < length; i++)
            {
                GetRandomWorkout();
            }
        }
        public void GetRandomWorkout()
        {
            int randomIndex = new Random().Next(TempWorkouts.Count);
            Workout randomWorkout = TempWorkouts.ElementAt(randomIndex);
            ScheduleWorkouts.Add(randomWorkout);
            TempWorkouts.Remove(randomWorkout);
        }
    }
}
