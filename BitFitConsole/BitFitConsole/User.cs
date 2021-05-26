using System;
using System.Collections.Generic;
using System.Text;

namespace BitFitConsole
{
    public class User
    {
        public int Id;
        public string Fullname, Gender;
        public double Age, RestHeartRate;
        protected double MaxHeartRate, VO2Max;
        public int EnduranceGroup;
        private int[] ageThresholds = new int[] { 13, 20, 30, 40, 50, 60 };

        // Male VO2Max thresholds ordered by age thresholds
        public double[] row1maleVO2 = new double[] { 0.0, 35.0, 38.4, 45.2, 51.0, 56.0 };
        public double[] row2maleVO2 = new double[] { 0.0, 33.0, 36.5, 42.5, 46.5, 52.5 };
        public double[] row3maleVO2 = new double[] { 0.0, 31.5, 35.5, 41.0, 45.0, 49.5 };
        public double[] row4maleVO2 = new double[] { 0.0, 30.2, 33.6, 39.0, 43.8, 48.1 };
        public double[] row5maleVO2 = new double[] { 0.0, 26.1, 31.0, 35.8, 41.0, 45.4 };
        public double[] row6maleVO2 = new double[] { 0.0, 20.5, 26.1, 32.3, 36.5, 44.3 };

        // Female VO2Max thresholds ordered by age thresholds
        public double[] row1femaleVO2 = new double[] { 0.0, 25.0, 30.9, 35.0, 39.0, 42.0 };
        public double[] row2femaleVO2 = new double[] { 0.0, 23.6, 29.0, 33.0, 37.0, 41.1 };
        public double[] row3femaleVO2 = new double[] { 0.0, 22.8, 27.0, 31.5, 35.7, 40.1 };
        public double[] row4femaleVO2 = new double[] { 0.0, 21.0, 24.5, 29.0, 32.9, 37.0 };
        public double[] row5femaleVO2 = new double[] { 0.0, 20.2, 22.8, 27.0, 31.5, 35.8 };
        public double[] row6femaleVO2 = new double[] { 0.0, 17.5, 20.2, 24.5, 30.3, 31.5 };

        public User(string fullName, string gender, double age, double restHeartRate)
        {
            Fullname = fullName;
            Gender = gender;
            Age = age;
            RestHeartRate = restHeartRate; // Get from Fitbit
            MaxHeartRate = 220 - Age;
            VO2Max = Math.Round(((MaxHeartRate / RestHeartRate) * 15), 2);
            EnduranceGroup = CheckEndurance(CheckAgeThreshold());
        }

        public static void AddUser()
        {
            Console.WriteLine("Please enter the following information: \n");
            Console.WriteLine("Full name:"); string fullName = Console.ReadLine();
            Console.WriteLine("\nGender(M/F):"); string gender = Console.ReadLine();
            Console.WriteLine("\nAge:"); double age = Double.Parse(Console.ReadLine());
            Console.WriteLine("\nResting heart rate:"); double restHeartRate = Double.Parse(Console.ReadLine());

            Bitfit.Users.Add(new User(fullName, gender, age, restHeartRate));
            Console.WriteLine("The user was successfully created!\n" +
                "\n------------------------------------\n");
            Bitfit.ReturnToMenu();
        }

        public static void ViewUser() {
            Console.WriteLine("\n------------------------------------\n" +
                $"\nCurrently viewing the user {Bitfit.currentUser.Fullname}\n" +
                $"Gender: {Bitfit.currentUser.Gender}\n" +
                $"Age: {Bitfit.currentUser.Age}\n" +
                $"Resting heart rate: {Bitfit.currentUser.RestHeartRate} bpm\n" +
                $"VO2max: {Bitfit.currentUser.VO2Max} ml/kg/min\n" +
                $"Endurance group: {Bitfit.currentUser.EnduranceGroup}\n" +
                $"\n------------------------------------\n");
            Bitfit.ReturnToMenu();
        }

        private int CheckAgeThreshold()
        {
            int row = ageThresholds.Length;
            for (int i = 0; i < ageThresholds.Length; i++)
            {
                if (Age > ageThresholds[ageThresholds.Length - 1 - i])
                    break;
                row--;
            }
            return row;
        }

        private int CheckEndurance(int row)
        {
            int length = row1maleVO2.Length;
            switch (row)
            {
                case (1):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row1maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row1femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
                case (2):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row2maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row2femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
                case (3):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row3maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row3femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
                case (4):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row4maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row4femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
                case (5):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row5maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row5femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
                case (6):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row6maleVO2[length - 1 - i] && Gender == "M")
                            return length - i;
                        else if (VO2Max >= row6femaleVO2[length - 1 - i] && Gender == "F")
                            return length - i;
                    }
                    break;
            }
            return -1;
        }
    }
}
