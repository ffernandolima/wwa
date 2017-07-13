// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using AutoMapper;

using Marketplace.Models.Security;
using Marketplace.Domain.Models.Security;

namespace Marketplace.Api.Core.Mapping.Security
{
    public class LoginMap : Profile
    {
        public LoginMap()
        {
            CreateMap<LoginRequest, LoginModel>()
                .ReverseMap();
        }
    }
}
