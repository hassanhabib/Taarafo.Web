// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.PostViews;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldAddPostViewAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDate();
            string randomAuthor = GetRandomName();

            dynamic randomPostViewProperties =
                CreateRandomPostViewProperties(
                    auditDates: randomDateTime,
                    auditAuthor: randomAuthor);

            var randomPostView = new PostView
            {
                Content = randomPostViewProperties.Content
            };

            PostView inputPostView = randomPostView;
            PostView expectedPostView = inputPostView;

            var randomPost = new Post
            {
                Id = randomPostViewProperties.Id,
                Content = randomPostViewProperties.Content,
                CreatedDate = randomDateTime,
                UpdatedDate = randomDateTime,
                Author = randomPostViewProperties.Author
            };

            Post expectedInputPost = randomPost;
            Post returnedPost = expectedInputPost;

            this.authorServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInAuthor())
                    .Returns(randomAuthor);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            this.postServiceMock.Setup(service =>
                service.AddPostAsync(It.Is(
                    SamePostAs(expectedInputPost))))
                        .ReturnsAsync(returnedPost);

            // when
            PostView actualPostView =
                await this.postViewService
                    .AddPostViewAsync(inputPostView);

            // then
            actualPostView.Should().BeEquivalentTo(expectedPostView);

            this.authorServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInAuthor(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.postServiceMock.Verify(service =>
                service.AddPostAsync(It.Is(
                    SamePostAs(expectedInputPost))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.authorServiceMock.VerifyNoOtherCalls();
            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
