// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.PostImpressions;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PostImpressionsRelativeUrl = "api/postimpressions";

        public async ValueTask<PostImpression> PostPostImpressionAsync(PostImpression postImpression) =>
            await this.PostAsync(PostImpressionsRelativeUrl, postImpression);
    }
}
