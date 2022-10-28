// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Taarafo.Portal.Web.Models.PostImpressions;
using Taarafo.Portal.Web.Models.PostImpressions.Exceptions;

namespace Taarafo.Portal.Web.Services.Foundations.PostImpressions
{
    public partial class PostImpressionService
    {
        public static void ValidatePostImpressionOnAdd(PostImpression postImpression)
        {
            ValidatePostImpression(postImpression);

            Validate(
                (Rule: IsInvalid(postImpression.PostId), Parameter: nameof(PostImpression.PostId)),
                (Rule: IsInvalid(postImpression.Post), Parameter: nameof(PostImpression.Post)),
                (Rule: IsInvalid(postImpression.ProfileId), Parameter: nameof(PostImpression.ProfileId)),
                (Rule: IsInvalid(postImpression.Profile), Parameter: nameof(PostImpression.Profile)),
                (Rule: IsInvalid(postImpression.CreatedDate), Parameter: nameof(PostImpression.CreatedDate)),
                (Rule: IsInvalid(postImpression.UpdatedDate), Parameter: nameof(PostImpression.UpdatedDate)));
        }

        private static void ValidatePostImpression(PostImpression postImpression)
        {
            if (postImpression is null)
                throw new NullPostImpressionException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Object is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPostImpressionException = new InvalidPostImpressionException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPostImpressionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPostImpressionException.ThrowIfContainsErrors();
        }
    }
}
