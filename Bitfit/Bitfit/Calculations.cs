using Bitfit.Models;
using System;

namespace Bitfit
{
    public class Calculations
    {
        public static int CalcMaxHeartRate(User CurrentUser)
        {
            return 220 - CurrentUser.Age;
        }
        public static double CalcVo2Max(User CurrentUser)
        {
            return Math.Round((((double)CalcMaxHeartRate(CurrentUser) / CurrentUser.RestHeartRate) * 15), 2);
        }
        public static int CalcEnduranceRank(User CurrentUser)
        {
            return CheckEndurance(CheckAgeThreshold(CurrentUser),CurrentUser);
        }
        public static int CheckAgeThreshold(User CurrentUser)
        {
            int[] ageThresholds = new int[] { 13, 20, 30, 40, 50, 60 };
            int row = ageThresholds.Length;
            for (int i = 0; i < ageThresholds.Length; i++)
            {
                if (CurrentUser.Age > ageThresholds[ageThresholds.Length - 1 - i])
                    break;
                row--;
            }
            return row;
        }

        public static int CheckEndurance(int row, User CurrentUser)
        {
            #region VO2Max Data
            // Male VO2Max thresholds ordered by age thresholds
            double[] row1maleVO2 = new double[] { 0.0, 35.0, 38.4, 45.2, 51.0, 56.0 };
            double[] row2maleVO2 = new double[] { 0.0, 33.0, 36.5, 42.5, 46.5, 52.5 };
            double[] row3maleVO2 = new double[] { 0.0, 31.5, 35.5, 41.0, 45.0, 49.5 };
            double[] row4maleVO2 = new double[] { 0.0, 30.2, 33.6, 39.0, 43.8, 48.1 };
            double[] row5maleVO2 = new double[] { 0.0, 26.1, 31.0, 35.8, 41.0, 45.4 };
            double[] row6maleVO2 = new double[] { 0.0, 20.5, 26.1, 32.3, 36.5, 44.3 };

            // Female VO2Max thresholds ordered by age thresholds
            double[] row1femaleVO2 = new double[] { 0.0, 25.0, 30.9, 35.0, 39.0, 42.0 };
            double[] row2femaleVO2 = new double[] { 0.0, 23.6, 29.0, 33.0, 37.0, 41.1 };
            double[] row3femaleVO2 = new double[] { 0.0, 22.8, 27.0, 31.5, 35.7, 40.1 };
            double[] row4femaleVO2 = new double[] { 0.0, 21.0, 24.5, 29.0, 32.9, 37.0 };
            double[] row5femaleVO2 = new double[] { 0.0, 20.2, 22.8, 27.0, 31.5, 35.8 };
            double[] row6femaleVO2 = new double[] { 0.0, 17.5, 20.2, 24.5, 30.3, 31.5 };
            #endregion
            double VO2Max = CalcVo2Max(CurrentUser);
            int length = row1maleVO2.Length;
            switch (row)
            {
                case (1):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row1maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row1femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
                case (2):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row2maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row2femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
                case (3):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row3maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row3femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
                case (4):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row4maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row4femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
                case (5):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row5maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row5femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
                case (6):
                    for (int i = 0; i < length; i++)
                    {
                        if (VO2Max >= row6maleVO2[length - 1 - i] && CurrentUser.Gender == "M")
                            return length - i;
                        else if (VO2Max >= row6femaleVO2[length - 1 - i] && CurrentUser.Gender == "F")
                            return length - i;
                    }
                    break;
            }
            return -1;
        }
    }
}
