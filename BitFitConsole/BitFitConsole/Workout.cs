using System;
using System.Collections.Generic;
using System.Text;

namespace BitFitConsole
{
    class Workout
    {
        public string Type; // HIIT, Endurance, Strength
        public int Length; // in minutes
        public Workout(string type, int length)
        {
            Type = type;
            Length = length;
        }

        public void SetupWorkout()
        {
            switch (Type)
            {
                case "HIIT":
                    CreateStrengthWorkout();
                    break;
                case "Endurance":
                    CreateEnduranceWorkout();
                    break;
                case "Strength":
                    CreateStrengthWorkout();
                    break;
            }
        }

        public void CreateStrengthWorkout()
        {

        }

        public void CreateEnduranceWorkout()
        {

        }

        public void CreateHIITWorkout()
        {

        }
    }
}
