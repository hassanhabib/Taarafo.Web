// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Services.Foundations.Comments
{
    public interface ICommentService
    {
        ValueTask<Comment> AddCommentAsync(Comment comment);
        ValueTask<List<Comment>> RetrieveAllCommentsAsync();
        ValueTask<Comment> RetrieveCommentByIdAsync(Guid commentId);
        ValueTask<Comment> ModifyCommentAsync(Comment comment);
    }
}
