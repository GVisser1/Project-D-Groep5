using BitFitWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitFitWebApp.Data
{
    public class UserData
    {
        private static User user1 = new User();
        private static User user2 = new User();
        private static User user3 = new User();
        public static void CreateUsers()
        {
            user1.Fullname = "Jan Klaas"; user2.Fullname = "Jaap de Vent"; user3.Fullname = "Carla de Vent";
            user1.Gender = "M"; user2.Gender = "M"; user3.Gender = "F";
            user1.Age = 23; user2.Age = 45; user3.Age = 38;
            user1.RestHeartRate = 66; user2.RestHeartRate = 72; user3.RestHeartRate = 54;
            user1.MaxHeartRate = 220 - user1.Age; user2.MaxHeartRate = 220 - user2.Age; user3.MaxHeartRate = 220 - user3.Age;
            user1.VO2Max = Math.Round(((user1.MaxHeartRate / user1.RestHeartRate) * 15), 2);
            user1.EnduranceGroup = user1.CheckEndurance(user1.CheckAgeThreshold());
            user2.VO2Max = Math.Round(((user2.MaxHeartRate / user2.RestHeartRate) * 15), 2);
            user2.EnduranceGroup = user2.CheckEndurance(user2.CheckAgeThreshold());
            user3.VO2Max = Math.Round(((user3.MaxHeartRate / user3.RestHeartRate) * 15), 2);
            user3.EnduranceGroup = user3.CheckEndurance(user3.CheckAgeThreshold());
            Pages.Index.Users.Add(user1); Pages.Index.Users.Add(user2); Pages.Index.Users.Add(user3);
        }
    }
}
