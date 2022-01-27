// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.CommentViews;
using Taarafo.Portal.Web.Services.Foundations.Comments;

namespace Taarafo.Portal.Web.Services.Views.CommentViews
{
    public partial class CommentViewService : ICommentViewService
    {
        private readonly ICommentService commentService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public CommentViewService(
            ICommentService commentService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.commentService = commentService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<CommentView> AddCommentViewAsync(CommentView commentView) =>
            TryCatch(async () =>
            {
                ValidateCommentView(commentView);

                Comment comment = MapToComment(commentView);
                await commentService.AddCommentAsync(comment);

                return commentView;
            });

        private static Comment MapToComment(CommentView commentView)
        {
            return new Comment
            {
                Id = commentView.Id,
                Content = commentView.Content,
                CreatedDate = commentView.CreatedDate,
                UpdatedDate = commentView.UpdatedDate,
                PostId = commentView.PostId
            };
        }
    }
}
