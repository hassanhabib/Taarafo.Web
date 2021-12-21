// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Comment> PostCommentAsync(Comment comment);
        ValueTask<List<Comment>> GetAllCommentsAsync();
        ValueTask<Comment> GetCommentByIdAsync(Guid commentId);
        ValueTask<Comment> PutCommentAsync(Comment comment);
        ValueTask<Comment> DeleteCommentByIdAsync(Guid commentId);
    }
}
