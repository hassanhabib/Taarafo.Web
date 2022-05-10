// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Groups;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<List<Group>> GetAllGroupsAsync();

        ValueTask<Group> GetGroupByIdAsync(Guid groupId);
    }
}