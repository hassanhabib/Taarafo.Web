// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using Taarafo.Portal.Web.Models.PostViews;
using Taarafo.Portal.Web.Services.PostViews;
using Taarafo.Portal.Web.Views.Components.PostsComponents;
using Tynamix.ObjectFiller;

namespace Taarafo.Portal.Web.Tests.Unit.Views.PostsComponents
{
    public partial class PostsComponentTests : TestContext
    {
        private readonly Mock<IPostViewService> postViewServiceMock;
        private IRenderedComponent<PostsComponent> renderedPostsComponent;

        public PostsComponentTests()
        {
            this.postViewServiceMock = new Mock<IPostViewService>();
            this.Services.AddTransient(service => this.postViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static List<PostView> CreateRandomPostViews() =>
            CreatePostViewFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<PostView> CreatePostViewFiller()
        {
            var filler = new Filler<PostView>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
