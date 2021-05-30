using Bitfit.Models;
using Bitfit.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitfit.Pages
{
    public partial class SchedulePage
    {
        [Inject]
        private DatabaseService DatabaseService { get; set; }
        protected List<Schedule> AllSchedules { get; set; }
        protected Workout HiitWorkout { get; set; }
        protected Workout EnduranceWorkout { get; set; }
        protected Workout StrengthWorkout { get; set; }
        public static Schedule CurrentSchedule;
        protected override void OnInitialized()
        {
            AllSchedules = DatabaseService.DB.Schedules.ToList();
            StateHasChanged();
        }
        public async Task AddSchedule()
        {
            CurrentSchedule = new Schedule();
            CurrentSchedule.UserId = UserPage.CurrentUser.Id;
            CurrentSchedule.Rank = UserPage.CurrentUser.Rank;
            CurrentSchedule.Workouts = new List<Workout> {
                new Workout
                {
                    Type = "HIIT",
                    Rank = CurrentSchedule.Rank,
                    Description = "A High-Intensity Interval Training that involves short bursts of intense exercise alternated with low-intensity recovery periods."
                },
                new Workout
                {
                    Type = "Endurance",
                    Rank = CurrentSchedule.Rank,
                    Description = "A long distance training that involves running for a long period of time."
                },
                new Workout
                {
                    Type = "Strength",
                    Rank = CurrentSchedule.Rank,
                    Description = "A strength training that involves executing different exercises, each focusing on a different muscle group."
                }
            };
            HiitPage.CurrentWorkout = CurrentSchedule.Workouts[0]; 
            EndurancePage.CurrentWorkout = CurrentSchedule.Workouts[1];
            StrengthPage.CurrentWorkout = CurrentSchedule.Workouts[2];
            DatabaseService.DB.Schedules.Add(CurrentSchedule);
            await DatabaseService.DB.SaveChangesAsync();
            AllSchedules = DatabaseService.DB.Schedules.ToList();
            StateHasChanged();
        }
        /*public void ViewWorkout(Workout workout)
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
        }*/
    }
}
