// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Views.Components.PostDialogs;

namespace Taarafo.Portal.Web.Views.Pages
{
    public partial class Index : ComponentBase
    {
        public PostDialog Dialog { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            this.Dialog.OpenDialog();
        }
    }
}
