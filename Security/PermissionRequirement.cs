using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public UserPermissions Permission { get; }
        public PermissionRequirement(UserPermissions permission) => Permission = permission;
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<User> _userManager;
        public PermissionHandler(UserManager<User> userManager) => _userManager = userManager;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null && user.HasPermission(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}