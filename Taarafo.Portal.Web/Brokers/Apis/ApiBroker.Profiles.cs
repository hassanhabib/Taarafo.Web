// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Taarafo.Portal.Web.Models.Profiles;

namespace Taarafo.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string ProfilesRelativeUrl = "api/profiles";

        public async ValueTask<List<Profile>> GetAllProfilesAsync() =>
            await this.GetAsync<List<Profile>>(ProfilesRelativeUrl);
    }
}
