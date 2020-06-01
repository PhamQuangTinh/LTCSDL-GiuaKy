using Microsoft.AspNetCore.Authorization;
using System;

namespace LTCSDL.DAL.Models
{
    public static class Policies
    {
        public const string Admin = "ADMIN";
        public const string User = "USER";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}