// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Brokers.Apis;
using Taarafo.Portal.Web.Brokers.Loggings;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.Posts;

namespace Taarafo.Portal.Web.Services.Foundations.PostImpressions
{
    public class PostImpressionService : IPostImpressionService
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

        public async ValueTask<PostImpression> AddPostImpressionAsync(PostImpression postImpression)
        {
            return await apiBroker.PostPostImpressionAsync(postImpression);
        }
    }
}
