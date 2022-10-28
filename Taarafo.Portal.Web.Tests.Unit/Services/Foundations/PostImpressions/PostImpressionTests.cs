// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using Moq;
using RESTFulSense.Exceptions;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Services.Foundations.PostImpressions;
using Taarafo.Portal.Web.Services.Foundations.Posts;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.PostImpressions
{
    public partial class PostImpressionTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPostImpressionService postImpressionService;

        public PostImpressionTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.postImpressionService = new PostImpressionService(
                apiBroker: apiBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyExceptions()
        {
            string exceptionMessage = GetRandomMessage();
            var responseMessage = new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseUnAuthorizedException =
                new HttpResponseUnauthorizedException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpResponseUrlNotFoundException,
                httpResponseUnAuthorizedException
            };
        }

        private static PostImpression CreateRandomPostImpression() =>
            CreatePostImpressionFiller().Create();

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message &&
                actualException.InnerException.Message == expectedException.InnerException.Message &&
                (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<string>>>();

            return filler.Create();
        }

        private static Filler<PostImpression> CreatePostImpressionFiller()
        {
            var filler = new Filler<PostImpression>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}
