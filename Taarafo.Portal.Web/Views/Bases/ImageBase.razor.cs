// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Taarafo.Portal.Web.Views.Bases
{
    public partial class ImageBase : ComponentBase
    {
        [Parameter]
        public string Url { get; set; }

        [Parameter]
        public string Width { get; set; }
    }
}
