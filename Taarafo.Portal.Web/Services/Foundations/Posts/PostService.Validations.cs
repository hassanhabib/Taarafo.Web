﻿// ---------------------------------------------------------------
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

            Validate(
                (Rule: IsInvalid(post.Id), Parameter: nameof(post.Id)),
                (Rule: IsInvalid(post.Content), Parameter: nameof(post.Content)),
                (Rule: IsInvalid(post.CreatedDate), Parameter: nameof(post.CreatedDate)),
                (Rule: IsInvalid(post.UpdatedDate), Parameter: nameof(post.UpdatedDate)),
                (Rule: IsInvalid(post.Author), Parameter: nameof(post.Author))
            );
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

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
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
