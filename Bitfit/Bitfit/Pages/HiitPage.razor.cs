using Bitfit.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitfit.Pages
{
    public partial class HiitPage
    {
        protected List<Workout> AllWorkouts { get; set; }
        public static Workout CurrentWorkout { get; set; }
        protected int Length { get; set; }
        protected int Accelerations { get; set; }
        protected int AccelerationTime { get; set; }
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
                    if (workout.Type == "HIIT") 
                    {
                        InitializeHiitTraining(workout.Rank);
                        CurrentWorkout = workout;
                    }
                }
            }
        }
        public void InitializeHiitTraining(int rank)
        {
            switch (rank)
            {
                case 1:
                    Length = 15; // min
                    Accelerations = 4;
                    AccelerationTime = 15; // sec
                    BreakTime = 2; // min
                    break;
                case 2:
                    Length = 20; // min
                    Accelerations = 5;
                    AccelerationTime = 20; // sec
                    BreakTime = 2; // min
                    break;
                case 3:
                    Length = 30; // min
                    Accelerations = 6;
                    AccelerationTime = 25; // sec
                    BreakTime = 2; // min
                    break;
                case 4:
                    Length = 35; // min
                    Accelerations = 6;
                    AccelerationTime = 30; // sec
                    BreakTime = 1; // min
                    break;
                case 5:
                    Length = 40; // min
                    Accelerations = 7;
                    AccelerationTime = 35; // sec
                    BreakTime = 1; // min
                    break;
                case 6:
                    Length = 45; // min
                    Accelerations = 8;
                    AccelerationTime = 40; // sec
                    BreakTime = 1; // min
                    break;
            }
        }
    }
}
