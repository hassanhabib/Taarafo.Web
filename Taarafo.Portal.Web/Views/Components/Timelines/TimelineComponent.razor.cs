// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.Timelines;
using Taarafo.Portal.Web.Services.PostViews;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Components.Timelines
{
    public partial class TimelineComponent : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        public TimeLineComponentState State { get; set; }
        public List<PostView> PostViews { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase Label { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.PostViews =
                await this.PostViewService.RetrieveAllPostViewsAsync();

            this.State = TimeLineComponentState.Content;
        }
    }
}
