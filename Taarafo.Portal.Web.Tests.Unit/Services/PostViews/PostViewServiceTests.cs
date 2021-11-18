// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Moq;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Services.Foundations.Posts;
using Taarafo.Portal.Web.Services.PostViews;
using Tynamix.ObjectFiller;

namespace Taarafo.Portal.Web.Tests.Unit.Services.PostViews
{
    public partial class PostViewServiceTests
    {
        private readonly Mock<IPostService> postServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPostViewService postViewService;

        public PostViewServiceTests()
        {
            this.postServiceMock = new Mock<IPostService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.postViewService = new PostViewService(
                postService: this.postServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static dynamic CreateRandomPostProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            return new
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(),
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                Author = auditIds
            };
        }
    }
}
