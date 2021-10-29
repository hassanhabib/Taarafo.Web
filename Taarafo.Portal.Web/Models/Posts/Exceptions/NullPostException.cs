// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Taarafo.Portal.Web.Models.Posts.Exceptions
{
    public class NullPostException : Xeption
    {
        public NullPostException()
            : base(message:"Post is null")
        {}
    }
}
