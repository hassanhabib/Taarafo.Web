// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Models.Views.Components.PostDialogs;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Components.PostDialogs
{
    public partial class PostDialog : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        public PostDialogComponentState State { get; set; }
        public DialogBase Dialog { get; set; }
        public bool IsVisible { get; set; }

        protected override void OnInitialized() => 
            this.State = PostDialogComponentState.Content;

        public void OpenDialog()
        {
            this.Dialog.Show();
            this.IsVisible = this.Dialog.IsVisible;
        }
    }
}
