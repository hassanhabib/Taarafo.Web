// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.CommentViews;

namespace Taarafo.Portal.Web.Services.Views.CommentViews
{
    public interface ICommentViewService
    {
        ValueTask<CommentView> AddCommentViewAsync(CommentView commentView);
    }
}
