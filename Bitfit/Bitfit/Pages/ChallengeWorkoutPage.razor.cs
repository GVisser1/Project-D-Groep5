using Bitfit.Models;
using System.Collections.Generic;

namespace Bitfit.Pages
{
    public partial class ChallengeWorkoutPage
    {
        public static Workout CurrentWorkout1 { get; set; }
        public static Workout CurrentWorkout2 { get; set; }
        public static Workout CurrentWorkout3 { get; set; }
        public static Schedule CurrentSchedule { get; set; }
        public static List<Workout> AllWorkouts { get; set; }
        public static string[] Workout1Array { get; set; }
        public static string[] Workout2Array { get; set; }
        public static string[] Workout3Array { get; set; }
        protected override void OnInitialized()
        {
            CurrentSchedule = SchedulePage.CurrentSchedule;

            AllWorkouts = DbFunctions.GetWorkouts();

            if (UserPage.SignedIn)
            {
                foreach (var workout in AllWorkouts)
                {
                    if (workout.Id == CurrentSchedule.Workout1Id)
                    {
                        CurrentWorkout1 = workout;
                    }
                    else if (workout.Id == CurrentSchedule.Workout2Id)
                    {
                        CurrentWorkout2 = workout;
                    }
                    else if (workout.Id == CurrentSchedule.Workout3Id)
                    {
                        CurrentWorkout3 = workout;
                    }
                }
                // Splits the description
                Workout1Array = CurrentWorkout1.Description.Split(';');
                Workout2Array = CurrentWorkout2.Description.Split(';');
                Workout3Array = CurrentWorkout3.Description.Split(';');
            }
            StateHasChanged();
        }
    }
}
