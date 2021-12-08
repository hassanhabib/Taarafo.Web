// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Pages
{
    public partial class Index : ComponentBase
    {
        public SpinnerBase Spinner { get; set; }

        protected override async void OnAfterRender(bool firstRender)
        {
            Spinner.Show();
            
            for(int i = 5; i > 0; i--)
            {
                Spinner.SetValue($"Loading ends in {i} seconds");
                await Task.Delay(1000);
            }
            
            Spinner.Hide();
        }
    }
}
