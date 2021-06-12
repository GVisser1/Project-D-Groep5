using Bitfit.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitfit.Pages
{
    public partial class ChallengePage
    {
        protected List<Challenge> AllChallenges { get; set; }
        protected override void OnInitialized()
        {
            AllChallenges = DbFunctions.GetChallenges();
            StateHasChanged();
        }
    }
}