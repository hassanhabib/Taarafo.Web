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
using Taarafo.Portal.Web.Models.Groups;
using Taarafo.Portal.Web.Services.Foundations.Groups;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Foundations.Groups
{
    public partial class GroupServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGroupService groupService;

        public GroupServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.groupService = new GroupService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
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

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static Group CreateRandomGroup() =>
            CreateGroupFiller().Create();

        private static List<Group> CreateRandomGroups() =>
            CreateGroupFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message &&
                actualException.InnerException.Message == expectedException.InnerException.Message &&
                (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Filler<Group> CreateGroupFiller()
        {
            var filler = new Filler<Group>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
