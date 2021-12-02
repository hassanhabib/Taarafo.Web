// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.PostViews;

namespace Taarafo.Portal.Web.Services.Views.PostViews
{
    public interface IPostViewService
    {
        ValueTask<List<PostView>> RetrieveAllPostViewsAsync();
        ValueTask<PostView> RemovePostViewByIdAsync(Guid postViewId);
    }
}
