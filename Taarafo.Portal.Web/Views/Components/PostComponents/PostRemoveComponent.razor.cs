// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.PostComponents;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Components.PostComponents
{
    public partial class PostRemoveComponent : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        [Parameter]
        public PostView PostView { get; set; }

        public PostRemoveComponentState State { get; set; }
        public ButtonBase Button { get; set; }
        public string ErrorMessage { get; set; }
        public ImageBase ErrorImage { get; set; }

        protected override void OnInitialized() =>
            State = PostRemoveComponentState.Content;

        public async ValueTask RemovePostAsync()
        {
            Button.Disable();

            await this.PostViewService
                .RemovePostViewByIdAsync(PostView.Id);
        }
    }
}
