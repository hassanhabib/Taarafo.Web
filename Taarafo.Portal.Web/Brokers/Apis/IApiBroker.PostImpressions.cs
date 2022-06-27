// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.PostImpressions;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<PostImpression> PostImpressionAsync(PostImpression postImpression);
    }
}
