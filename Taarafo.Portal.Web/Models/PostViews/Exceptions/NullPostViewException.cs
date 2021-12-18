// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Taarafo.Portal.Web.Models.PostViews.Exceptions
{
    public class NullPostViewException : Xeption
    {
        public NullPostViewException()
            : base("Null post error occured.") { }
    }
}
