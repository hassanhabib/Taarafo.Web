// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Taarafo.Portal.Web.Brokers.DateTimes;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;
using Taarafo.Portal.Web.Services.Foundations.Authors;
using Taarafo.Portal.Web.Services.Foundations.Posts;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        private readonly Mock<IPostService> postServiceMock;
        private readonly Mock<IAuthorService> authorServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IPostViewService postViewService;

        public PostViewServiceTests()
        {
            this.postServiceMock = new Mock<IPostService>();
            this.authorServiceMock = new Mock<IAuthorService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<Post>(post => post.Id);
            this.compareLogic = new CompareLogic(compareConfig);

            this.postViewService = new PostViewService(
                postService: this.postServiceMock.Object,
                authorService: this.authorServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
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

        private static string GetRandomName() =>
            new RealNames().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static dynamic CreateRandomPostViewProperties(
            DateTimeOffset auditDates,
            string auditAuthor)
        {
            return new
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(),
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                Author = auditAuthor
            };
        }

        private Expression<Func<Post, bool>> SamePostAs(Post expectedPost)
        {
            return actualPost => this.compareLogic.Compare(expectedPost, actualPost)
                .AreEqual;
        }

        private static dynamic CreateRandomPostViewProperties()
        {
            return new
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(),
                CreatedDate = GetRandomDate(),
                UpdatedDate = GetRandomDate(),
                Author = GetRandomString()
            };
        }

        private static List<dynamic> CreateRandomPostViewCollections()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(0, randomCount).Select(item =>
            {
                return new
                {
                    Id = Guid.NewGuid(),
                    Content = GetRandomString(),
                    CreatedDate = GetRandomDate(),
                    UpdatedDate = GetRandomDate(),
                    Author = GetRandomName()
                };
            }).ToList<dynamic>();
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
