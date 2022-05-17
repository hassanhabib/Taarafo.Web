// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Taarafo.Portal.Web.Models.Groups;
using Taarafo.Portal.Web.Models.Groups.Exceptions;

namespace Taarafo.Portal.Web.Services.Foundations.Groups
{
    public partial class GroupService
    {
        public static void ValidateGroupId(Guid groupId) =>
            Validate((Rule: IsInvalid(groupId), Parameter: nameof(Group.Id)));

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGroupException = new InvalidGroupException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGroupException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGroupException.ThrowIfContainsErrors();
        }
    }
}
