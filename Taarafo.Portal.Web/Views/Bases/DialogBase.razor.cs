// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace Taarafo.Portal.Web.Views.Bases
{
    public partial class DialogBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        [Parameter]
        public string Width { get; set; }

        public bool IsVisible { get; set; }

        public void Show()
        {
            IsVisible = true;
            InvokeAsync(StateHasChanged);
        } 
        public void Hide()
        {
            IsVisible = false;
            InvokeAsync(StateHasChanged);
        }

        public void Click() => OnClick?.Invoke();
    }
}
