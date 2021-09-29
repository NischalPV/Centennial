using System;
namespace Centennial.Api.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
