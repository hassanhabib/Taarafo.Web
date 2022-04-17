// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Groups;

namespace Taarafo.Portal.Web.Services.Foundations.Groups
{
    public interface IGroupService
    {
        ValueTask<List<Group>> RetrieveAllGroupsAsync();
    }
}