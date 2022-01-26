// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Taarafo.Portal.Web.Models.Comments;
using Taarafo.Portal.Web.Models.CommentViews;
using Xunit;

namespace Taarafo.Portal.Web.Tests.Unit.Services.Views.CommentViews
{
    public partial class CommentViewServiceTest
    {
        [Fact] 
        public async Task ShouldAddCommentViewAsync()
        {
            // given 
            DateTimeOffset randomDateTime = GetRandomDate();
            Guid randomPostId = GetRandomGuid();

            dynamic randomCommentViewProperties =
                CreateRandomCommentViewProperties(
                    auditDates: randomDateTime,
                    auditPostId: randomPostId);

            var randomCommentView = new CommentView
            {
                Id = randomCommentViewProperties.Id,
                Content = randomCommentViewProperties.Content,
                CreatedDate = randomCommentViewProperties.CreatedDate,
                UpdatedDate = randomCommentViewProperties.UpdatedDate, 
                PostId = randomCommentViewProperties.PostId
            };

            CommentView inputCommentView = randomCommentView;
            CommentView expectedCommentView = inputCommentView;

            var randomComment = new Comment()
            {
                Id = randomCommentViewProperties.Id,
                Content = randomCommentViewProperties.Content,
                CreatedDate = randomCommentViewProperties.CreatedDate,
                UpdatedDate = randomCommentViewProperties.UpdatedDate,
                PostId = randomCommentViewProperties.PostId
            };

            Comment inputComment = randomComment;
            Comment expectedComment = inputComment;

            this.commentServiceMock.Setup(service =>
                service.AddCommentAsync(It.Is(
                    SameCommentAs(inputComment))))
                    .ReturnsAsync(expectedComment);

            // when 
            CommentView actualCommentView =
                await this.commentViewService
                    .AddCommentViewAsync(inputCommentView);

            // then
            actualCommentView.Should().BeEquivalentTo(expectedCommentView);

            this.commentServiceMock.Verify(service => 
                service.AddCommentAsync(It.Is(
                    SameCommentAs(inputComment))), 
                Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.commentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
