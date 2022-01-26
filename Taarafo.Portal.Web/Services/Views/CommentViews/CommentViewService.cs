// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Models.CommentViews;
using Taarafo.Portal.Web.Services.Foundations.Comments;

namespace Taarafo.Portal.Web.Services.Views.CommentViews
{
    public class CommentViewService : ICommentViewService
    {
        private readonly ICommentService commentService;
        private readonly IDateTimeBroker dateTimeBroker;

        public CommentViewService(
            ICommentService commentService,
            IDateTimeBroker dateTimeBroker)
        {
            this.commentService = commentService;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<CommentView> AddCommentViewAsync(CommentView commentView)
        {
            throw new NotImplementedException();
        }
    }
}
