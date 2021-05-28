using Bitfit.Models;
using Bitfit.Services;
using Microsoft.AspNetCore.Components;
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
    }
}
