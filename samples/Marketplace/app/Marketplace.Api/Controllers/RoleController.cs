// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System.Web.Http;

using Marketplace.Api.Models.Security;
using Marketplace.Domain.Models.Security;
using Marketplace.Logic.Contracts.Security;

using Wwa.Api.Controllers;
using Wwa.Api.Extensions;

namespace Marketplace.Api.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(IRoleService roleService)
        {
            RoleService = roleService;
        }

        IRoleService RoleService { get; }
        
        // GET Role
        public IHttpActionResult Get()
        {
            var list = RoleService.List();
            var mapped = list.Map<Role, RoleModel>();
            
            // Returns a 200 status with custom headers (paging)
            return Ok(mapped);
        }
    }
}
