// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Taarafo.Portal.Web.Views.Bases
{
    public partial class SpinnerBase : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public string Value { get; set; }

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

        public void SetValue(string value) 
        {
            Value = value;
            InvokeAsync(StateHasChanged);
        }
    }
}
