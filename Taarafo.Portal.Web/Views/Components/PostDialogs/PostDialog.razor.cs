// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.PostViews.Exceptions;
using Taarafo.Portal.Web.Models.Views.Components.PostDialogs;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Taarafo.Portal.Web.Views.Bases;
using Xeptions;

namespace Taarafo.Portal.Web.Views.Components.PostDialogs
{
    public partial class PostDialog : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        public PostDialogComponentState State { get; set; }
        public DialogBase Dialog { get; set; }
        public TextAreaBase TextArea { get; set; }
        public bool IsVisible { get; set; }
        public PostView PostView { get; set; }
        public SpinnerBase Spinner { get; set; }
        public Exception Exception { get; set; }
        public ValidationSummaryBase ContentValidationSummary { get; set; }

        protected override void OnInitialized()
        {
            this.PostView = new PostView();
            this.State = PostDialogComponentState.Content;
        }

        public void OpenDialog()
        {
            this.Dialog.Show();
            this.IsVisible = this.Dialog.IsVisible;
        }

        public async ValueTask PostViewAsync()
        {
            try
            {
                this.TextArea.Disable();
                this.Dialog.DisableButton();
                this.Spinner.Show();

                await this.PostViewService.AddPostViewAsync(
                    this.PostView);

                CloseDialog();
            }
            catch (PostViewValidationException postViewValidationException)
            {
                RenderValidationError(postViewValidationException);
            }
            catch (PostViewDependencyValidationException postViewDependencyValidationException)
            {
                RenderValidationError(postViewDependencyValidationException);
            }
        }

        private void RenderValidationError(Xeption exception)
        {
            this.Exception = exception.InnerException;
            this.TextArea.Enable();
            this.Dialog.EnableButton();
            this.Spinner.Hide();
        }

        public void CloseDialog()
        {
            this.Dialog.Hide();
            this.IsVisible = this.Dialog.IsVisible;
        }
    }
}
