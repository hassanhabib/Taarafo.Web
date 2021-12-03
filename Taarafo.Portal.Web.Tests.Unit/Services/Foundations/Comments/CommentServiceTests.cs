// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using Taarafo.Portal.Web.Brokers.API;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Services.Foundations.Comments;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Comments
{
    public partial class CommentServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICommentService commentService;

        public CommentServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.commentService = new CommentService(
                apiBroker: apiBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private static Comment CreateRandomComment() =>
            CreateCommentFiller(date: GetRandomDateTimeOffset()).Create();

        private static Comment CreateRandomComment(DateTimeOffset date) =>
             CreateCommentFiller(date).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Comment> CreateCommentFiller(DateTimeOffset date)
        {
            var filler = new Filler<Comment>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message &&
                actualException.InnerException.Message == expectedException.InnerException.Message &&
                (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
    }
}
