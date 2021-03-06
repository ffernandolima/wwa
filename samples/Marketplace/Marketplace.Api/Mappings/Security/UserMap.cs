// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using AutoMapper;

using Marketplace.Client.Models.Security;
using Marketplace.Domain.Models.Security;

namespace Marketplace.Api.Mappings.Security
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserModel>()
                .ReverseMap();
        }
    }
}
