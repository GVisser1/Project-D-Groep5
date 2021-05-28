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
    public partial class SchedulePage
    {
        [Inject]
        private DatabaseService DatabaseService { get; set; }
        protected List<Schedule> AllSchedules { get; set; }
        protected Schedule CurrentSchedule;
        protected bool AddingSchedule { get; set; }
        protected override void OnInitialized()
        {
            AllSchedules = DatabaseService.DB.Schedules.ToList();
            StateHasChanged();
        }
        public void OnNewSchedule()
        {
            AddingSchedule = true;
            CurrentSchedule = new Schedule();
        }
        public async Task AddSchedule(EditContext editContext)
        {
            DatabaseService.DB.Schedules.Add(CurrentSchedule);
            await DatabaseService.DB.SaveChangesAsync();
            AllSchedules = DatabaseService.DB.Schedules.ToList();
            StateHasChanged();
            AddingSchedule = false;
        }
    }
}
