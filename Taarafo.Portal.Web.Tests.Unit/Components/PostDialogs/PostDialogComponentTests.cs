// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using Taarafo.Portal.Web.Services.Views.PostViews;
using Taarafo.Portal.Web.Views.Components.PostDialogs;
using Tynamix.ObjectFiller;

namespace Taarafo.Portal.Web.Tests.Unit.Components.PostDialogs
{
    public partial class PostDialogComponentTests : TestContext
    {
        private readonly Mock<IPostViewService> postViewServiceMock;
        private IRenderedComponent<PostDialog> postDialogRenderedComponent;

        public PostDialogComponentTests()
        {
            this.postViewServiceMock = new Mock<IPostViewService>();
            this.Services.AddTransient(service => this.postViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static string GetRandomContent() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static string GetRandomErrorMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static string[] GetRandomErrorMessages()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(start: 0, count: randomCount)
                .Select(item => GetRandomErrorMessage())
                    .ToArray();
        }
        
        private static int GetRandomNumber() =>
            new IntRange(min: 2, 10).GetValue();
    }
}
