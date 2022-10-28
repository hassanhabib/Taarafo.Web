// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.PostImpressions;

namespace Taarafo.Portal.Web.Services.Foundations.PostImpressions
{
    public partial class PostImpressionService : IPostImpressionService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PostImpressionService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<PostImpression> AddPostImpressionAsync(PostImpression postImpression) =>
        TryCatch(async () =>
        {
            ValidatePostImpressionOnAdd(postImpression);

            return await apiBroker.PostPostImpressionAsync(postImpression);
        });
    }
}
