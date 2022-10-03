// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

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
    }
}
