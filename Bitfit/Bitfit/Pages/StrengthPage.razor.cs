using Bitfit.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitfit.Pages
{
    public partial class StrengthPage
    {
        protected List<Workout> AllWorkouts { get; set; }
        public static Workout CurrentWorkout { get; set; }
        protected int Exercises { get; set; } // Number of times an exercise needs to be performed EX. StrengtExercises = 20 => 20 push-ups
        protected int StretchTime { get; set; }
        protected int BreakTime { get; set; }
        protected override void OnInitialized()
        {
            if (SchedulePage.CurrentSchedule != null)
                AllWorkouts = SchedulePage.CurrentSchedule.Workouts.ToList();
            StateHasChanged();
            if (AllWorkouts != null)
            {
                foreach (var workout in AllWorkouts)
                {
                    if (workout.Type == "Endurance")
                    {
                        InitializeStrengthTraining(workout.Rank);
                        CurrentWorkout = workout;
                    }
                }
            }
        }
        public void InitializeStrengthTraining(int rank)
        {
            switch (rank)
            {
                case 1:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
                case 2:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
                case 3:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
                case 4:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
                case 5:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
                case 6:
                    Exercises = 5;
                    StretchTime = 30; // sec
                    BreakTime = 30; // sec
                    break;
            }
        }
    }
}
