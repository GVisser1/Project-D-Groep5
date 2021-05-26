using System;
using System.Collections.Generic;
using BitFitWebApp.Data;
using System.Linq;
using System.Threading.Tasks;
using BitFitWebApp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BitFitWebApp.Pages
{
    public partial class Index
    {
        protected User CurrentUser;
        public static List<User> Users = new List<User>();
        protected bool AddingUser { get; set; }
        protected bool SignedIn { get; set; }
        public string LastSubmitResult;

        public void SelectUser(User user)
        {
            CurrentUser = user;
            SignedIn = true;
        }
        public void SignOut()
        {
            CurrentUser = null;
            SignedIn = false;
        }
        public void OnNewUser()
        {
            AddingUser = true;
            CurrentUser = new User();
        }
        public void AddUser(EditContext editContext)
        {
            CurrentUser.MaxHeartRate = 220 - CurrentUser.Age;
            CurrentUser.VO2Max = Math.Round(((CurrentUser.MaxHeartRate / CurrentUser.RestHeartRate) * 15), 2);
            CurrentUser.EnduranceGroup = CurrentUser.CheckEndurance(CurrentUser.CheckAgeThreshold());
            Users.Add(CurrentUser);
            AddingUser = false;
        }
    }
}
