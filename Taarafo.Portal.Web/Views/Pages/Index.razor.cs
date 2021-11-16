// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Pages
{
    public partial class Index : ComponentBase
    {
        public DialogBase Dialog { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            Dialog.Show();
        }
        
        public void CloseDialog()
        {
            Dialog.Hide();
        }
    }
}
