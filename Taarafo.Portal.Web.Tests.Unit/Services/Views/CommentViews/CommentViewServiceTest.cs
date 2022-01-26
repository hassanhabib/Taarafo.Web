// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Services.Foundations.Comments;
using Taarafo.Portal.Web.Services.Views.CommentViews;
using Tynamix.ObjectFiller;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.CommentViews
{
    public partial class CommentViewServiceTest
    {
        private readonly Mock<ICommentService> commentServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICommentViewService commentViewService;
        private readonly ICompareLogic compareLogic;

        public CommentViewServiceTest()
        {
            this.commentServiceMock = new Mock<ICommentService>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<Comment>(comment => comment.Id);
            this.compareLogic = new CompareLogic(compareConfig);

            this.commentViewService = new CommentViewService(
                commentService: this.commentServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static string GetRandomString() =>
           new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Guid GetRandomGuid() =>
            Guid.NewGuid();

        private dynamic CreateRandomCommentViewProperties(DateTimeOffset auditDates, Guid auditPostId)
        {
            return new
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(),
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                PostId = auditPostId
            };
        }

        private Expression<Func<Comment, bool>> SameCommentAs(Comment expectedComment)
        {
            return actualComment => this.compareLogic.Compare(expectedComment, actualComment)
                .AreEqual;
        }
    }
}
