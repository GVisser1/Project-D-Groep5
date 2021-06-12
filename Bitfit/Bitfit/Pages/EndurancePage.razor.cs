using Bitfit.Models;
using System.Collections.Generic;

namespace Bitfit.Pages
{
    public partial class EndurancePage
    {
        protected List<Workout> AllWorkouts { get; set; }
        public static Workout CurrentWorkout { get; set; }
        protected int Distance { get; set; }
        protected override void OnInitialized()
        {
            InitializeEnduranceTraining(CurrentWorkout.Rank);
            StateHasChanged();
        }
        public void InitializeEnduranceTraining(int rank)
        {
            switch (rank)
            {
                case 1:
                    Distance = 2; // km
                    break;
                case 2:
                    Distance = 3; // km
                    break;
                case 3:
                    Distance = 5; // km
                    break;
                case 4:
                    Distance = 7; // km
                    break;
                case 5:
                    Distance = 10; // km
                    break;
                case 6:
                    Distance = 12; // km
                    break;
            }
        }
    }
}
