﻿using InfoTrack.Refactoring.Application.Contracts;
using System.Security.Claims;

namespace InfoTrack.Refactoring.Api.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            //UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
    }
}
