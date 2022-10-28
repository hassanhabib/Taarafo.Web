// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;

namespace Taarafo.Portal.Web.Services.Foundations.PostImpressions
{
    public partial class PostImpressionService
    {
        public static void ValidatePostImpressionOnAdd(PostImpression postImpression)
        {
            ValidatePostImpression(postImpression);
        }

        private static void ValidatePostImpression(PostImpression postImpression)
        {
            if (postImpression is null)
                throw new NullPostImpressionException();
        }
    }
}
