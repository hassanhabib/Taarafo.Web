// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public interface ICommentService
    {
        ValueTask<Comment> AddCommentAsync(Comment post);
    }
}
