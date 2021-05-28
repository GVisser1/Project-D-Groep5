using Bitfit.Models;
using Bitfit.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitfit.Pages
{
    public partial class WorkoutPage
    {
        [Inject]
        private DatabaseService DatabaseService { get; set; }
        protected List<Workout> AllWorkouts { get; set; }
        protected Workout CurrentWorkout;
        protected bool AddingWorkout { get; set; }
        protected override void OnInitialized()
        {
            AllWorkouts = DatabaseService.DB.Workouts.ToList();
            StateHasChanged();
        }
        public void OnNewWorkout()
        {
            AddingWorkout = true;
            CurrentWorkout = new Workout();
        }
        public async Task AddWorkout(EditContext editContext)
        {
            DatabaseService.DB.Workouts.Add(CurrentWorkout);
            await DatabaseService.DB.SaveChangesAsync();
            AllWorkouts = DatabaseService.DB.Workouts.ToList();
            StateHasChanged();
            AddingWorkout = false;
        }
    }
}
