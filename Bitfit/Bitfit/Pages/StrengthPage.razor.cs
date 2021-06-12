using Bitfit.Models;
using System.Collections.Generic;

namespace Bitfit.Pages
{
    public partial class StrengthPage
    {
        protected List<Workout> AllWorkouts { get; set; }
        public static Workout CurrentWorkout { get; set; }
        protected int NoExercises { get; set; } // Number of...
        protected int AmountPerExercise { get; set; }
        protected List<string> Exercises { get; set; }
        protected override void OnInitialized()
        {
            InitializeStrengthTraining(CurrentWorkout.Rank);
            Exercises = GetExercises();
            StateHasChanged();
        }
        public void InitializeStrengthTraining(int rank)
        {
            switch (rank)
            {
                case 1:
                    NoExercises = 4; AmountPerExercise = 5;
                    break;
                case 2:
                    NoExercises = 5; AmountPerExercise = 8;
                    break;
                case 3:
                    NoExercises = 7; AmountPerExercise = 12;
                    break;
                case 4:
                    NoExercises = 8; AmountPerExercise = 16;
                    break;
                case 5:
                    NoExercises = 9; AmountPerExercise = 20;
                    break;
                case 6:
                    NoExercises = 10; AmountPerExercise = 25;
                    break;
            }
        }
        public List<string> CreateExercises()
        {
            var exercises = new List<string>()
            {
                "Push-Ups", "Inchworms", "Lunges", "Flutter Kicks", //Rank 1
                "Burpees", //Rank 2
                "Sumo Squat", "Side Leg Circles (Both Sides)", // Rank 3
                "Swimmer And Superman", // Rank 4
                "Pike Push-Ups", // Rank 5
                "Glute Kick Back (Both Sides)" // Rank 6
            };
            return exercises;
        } 
        public List<string> GetExercises()
        {
            var allExercises = CreateExercises();
            var exercises = new List<string>();
            for (int i = 0; i < NoExercises; i++)
            {
                exercises.Add(allExercises[i]);
            }
            return exercises;
        }
    }
}
