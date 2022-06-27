// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Taarafo.Portal.Web.Models.Posts;
using Taarafo.Portal.Web.Models.Profiles;

namespace Taarafo.Portal.Web.Models.PostImpressions
{
    public class PostImpression
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public PostImpressionType Impression { get; set; }
    }
}
