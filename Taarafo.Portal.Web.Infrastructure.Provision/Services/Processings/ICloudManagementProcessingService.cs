// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace Taarafo.Portal.Web.Infrastructure.Provision.Services.Processings
{
    public interface ICloudManagementProcessingService
    {
        ValueTask ProcessAsync();
    }
}
