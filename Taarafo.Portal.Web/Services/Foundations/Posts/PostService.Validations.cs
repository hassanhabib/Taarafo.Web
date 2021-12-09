// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Posts.Exceptions;

namespace Taarafo.Portal.Web.Services.Foundations.Posts
{
    public partial class PostService
    {
        public static void ValidatePostOnAdd(Post post)
        {
            ValidatePost(post);
        }

        public static void ValidatePostId(Guid postId)
        {
            Validate((Rule: IsInvalid(postId), Parameter: nameof(Post.Id)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPostException = new InvalidPostException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPostException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPostException.ThrowIfContainsErrors();
        }

        private static void ValidatePost(Post post)
        {
            if (post is null)
            {
                throw new NullPostException();
            }
        }
    }
}
