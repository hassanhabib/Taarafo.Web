// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Models.Views.Components.PostsComponents;
using Taarafo.Portal.Web.Services.PostViews;
using Taarafo.Portal.Web.Views.Bases;

namespace Taarafo.Portal.Web.Views.Components.PostsComponents
{
    public partial class PostsComponent : ComponentBase
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        public PostsComponentState State { get; set; }
        public List<PostView> PostViews { get; set; }
        public GridBase<PostView> Grid { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase ErrorLabel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.PostViews =
                await this.PostViewService.RetrieveAllPostViewsAsync();

            this.State = PostsComponentState.Content;
        }
    }
}
