using Bitfit.Models;
using Bitfit.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Bitfit
{
    public partial class DbFunctions
    {
        public static SQLiteConnection conn;
        public static SQLiteCommand cmd;
        public SQLiteDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public DbFunctions()
        {
            string database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database/BitfitDb.db");
            conn = new SQLiteConnection("data source=" + database + ";Version=3");
        }
        public static DataRowCollection Select(string query)
        {
            //deze functie haalt data op uit de database en returnd het resultaat
            string database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database/BitfitDb.db");
            conn = new SQLiteConnection("data source=" + database + ";Version=3");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            return dt.Rows;
        }
        //query naar de database word uitgevoerd
        public static void ExcQuery(string Query)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<User> GetUsers()
        {
            var AllUsers = new List<User>();
            foreach (DataRow user in DbFunctions.Select("SELECT * FROM Users"))
            {
                int tempScheduleId = 0;
                if (Int32.Parse(user["ScheduleId"].ToString()) > 0) tempScheduleId = Int32.Parse(user["ScheduleId"].ToString());
                AllUsers.Add(new User
                {
                    Id = Int32.Parse(user["Id"].ToString()),
                    FullName = user["FullName"].ToString(),
                    Gender = user["Gender"].ToString(),
                    Age = Int32.Parse(user["Age"].ToString()),
                    RestHeartRate = Int32.Parse(user["RestHeartRate"].ToString()),
                    Rank = Int32.Parse(user["Rank"].ToString()),
                    ScheduleId = tempScheduleId
                });
            }
            return AllUsers;
        }
        public static List<Schedule> GetSchedules()
        {
            var AllSchedules = new List<Schedule>();
            foreach (DataRow schedule in Select("SELECT * FROM Schedules"))
            {
                int tempWorkout1Id = 0, tempWorkout2Id = 0, tempWorkout3Id = 0;
                if (Int32.Parse(schedule["Workout1Id"].ToString()) > 0) tempWorkout1Id = Int32.Parse(schedule["Workout1Id"].ToString());
                if (Int32.Parse(schedule["Workout2Id"].ToString()) > 0) tempWorkout2Id = Int32.Parse(schedule["Workout2Id"].ToString());
                if (Int32.Parse(schedule["Workout3Id"].ToString()) > 0) tempWorkout3Id = Int32.Parse(schedule["Workout3Id"].ToString());
                AllSchedules.Add(new Schedule
                {
                    Id = Int32.Parse(schedule["Id"].ToString()),
                    Rank = Int32.Parse(schedule["Rank"].ToString()),
                    Workout1Id = tempWorkout1Id,
                    Workout2Id = tempWorkout2Id,
                    Workout3Id = tempWorkout3Id
                });
            }
            return AllSchedules;
        }
        public static List<Workout> GetWorkouts()
        {
            var AllWorkouts = new List<Workout>();
            foreach (DataRow workout in Select("SELECT * FROM Workouts"))
            {
                AllWorkouts.Add(new Workout
                {
                    Id = Int32.Parse(workout["Id"].ToString()),
                    Type = workout["Type"].ToString(),
                    Rank = Int32.Parse(workout["Rank"].ToString()),
                    Description = workout["Description"].ToString(),
                    Href = workout["Href"].ToString()
                });
            }
            return AllWorkouts;
        }
        public static List<Workout> GetAvailableWorkouts(int rank)
        {
            var AvailableWorkouts = new List<Workout>();
            foreach (var workout in GetWorkouts())
            {
                if (rank == workout.Rank)
                {
                    AvailableWorkouts.Add(workout);
                }
            }
            return AvailableWorkouts;
        }
        public static List<Workout> GetCurrentWorkouts()
        {
            var ScheduleWorkouts = new List<Workout>();
            var AllWorkouts = GetWorkouts();
            foreach (var workout in AllWorkouts)
            {
                if(workout.Id == SchedulePage.CurrentSchedule.Workout1Id) {
                    ScheduleWorkouts.Add(workout); break;
                }
            }
            foreach (var workout in AllWorkouts)
            {
                if(workout.Id == SchedulePage.CurrentSchedule.Workout2Id) {
                    ScheduleWorkouts.Add(workout); break;
                }
            }
            foreach (var workout in AllWorkouts)
            {
                if(workout.Id == SchedulePage.CurrentSchedule.Workout3Id) {
                    ScheduleWorkouts.Add(workout); break;
                }
            }
            return ScheduleWorkouts;
        }
    }
}
