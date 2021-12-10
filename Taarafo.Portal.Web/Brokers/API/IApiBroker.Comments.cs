// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Comments;

namespace Taarafo.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        ValueTask<Comment> PostCommentAsync(Comment comment);
        ValueTask<List<Comment>> GetAllCommentsAsync();
    }
}
