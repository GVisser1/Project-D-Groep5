using System;
using System.Collections.Generic;
using System.Text;

namespace BitFitConsole
{
    class Schedule
    {
        // public int Restdays // Amount of restdays
        // public int NumWorkouts // Amount of workouts
        // public int WeeklyTime; // Time available in a week
        // public int Length; // In weeks
        // public int Difficulty; // Depends on endurance group
        public Schedule()
        {
            
        }

        public void SetupSchedule()
        {
            switch (Bitfit.currentUser.EnduranceGroup)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }

        public void CreateSchedule()
        {

        }

        public void ViewSchedule()
        {

        }

        public void RemoveSchedule()
        {

        }
    }
}
