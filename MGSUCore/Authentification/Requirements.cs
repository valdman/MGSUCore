using System;
using MGSUCore.Authentification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Common.Entities;

namespace MGSUBackend.Authentification
{
    
    public class IsInRole : IAuthorizationRequirement
    {
        public UserRole AccountRole {get; private set;}
        public IsInRole(UserRole accountRole)
        {
            AccountRole = accountRole;
        }
    }

    public class IsAuthentificated : IAuthorizationRequirement
    {

    }
}