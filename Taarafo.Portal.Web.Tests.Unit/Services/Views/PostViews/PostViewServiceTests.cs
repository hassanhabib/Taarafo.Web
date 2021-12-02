// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Taarafo.Portal.Web.Services.Foundations.Posts;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData ValidationExceptions()
        {
            var innerException = new Xeption();

            var postServiceValidationException =
                new PostValidationException(innerException);

            var postDependencyValidationException =
                new PostDependencyValidationException(innerException);

            return new TheoryData<Exception>
            {
                postServiceValidationException,
                postDependencyValidationException
            };
        }

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Xeption();

            var postServiceDependencyException =
                new PostDependencyException(innerException);

            var postServiceException =
                new PostServiceException(innerException);

            return new TheoryData<Exception>
            {
                postServiceDependencyException,
                postServiceException
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static dynamic CreateRandomPostViewProperties()
        {
            return new
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(),
                CreatedDate = GetRandomDate(),
                UpdatedDate = GetRandomDate(),
                Author = Guid.NewGuid()
            };
        }

        private static List<dynamic> CreateRandomPostViewCollections()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(0, randomCount).Select(item =>
                CreateRandomPostViewProperties())
                    .ToList<dynamic>();
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(
            Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
    }
}
