// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System.Linq;
using System.Security.Principal;

using Marketplace.Domain.Security;
using Marketplace.Domain.Models.Security;
using Marketplace.Logic.Contracts.Security;

using Wwa.Api.Filters;

namespace Marketplace.Api.Core.Filters
{
    internal class PermissionAttribute : PermissionBaseAttribute
    {
        object sync = new object();

        public PermissionAttribute(SecurityContext security, IUserService userService, IFeatureService featureService)
        {
            Security = security;
            UserService = userService;
            FeatureService = featureService;
        }

        IUserService UserService { get; }
        IFeatureService FeatureService { get; }
        SecurityContext Security { get; }
        
        protected override bool Evaluate(IIdentity identity, string route, string method)
        {
            var path = $"{route}/{method}";
            var identityId = identity?.Name;

            var allowAccess = false;

            Security.Feature = FeatureService.Get(path);

            // Check feature premission
            if (identity.IsAuthenticated)
            {
                User user = UserService.Get(i => i.IdentityId == identityId);

                // Admin has full access
                if (!user.Role.IsAdmin)
                {
                    var permissions = user.Role.Permissions;
                    allowAccess = permissions.Any(i => i.Path.ToLower() == path.ToLower());
                }

                Security.User = user;
            }

            return allowAccess;
        }
    }
}
