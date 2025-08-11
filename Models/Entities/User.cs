using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; } = Guid.CreateVersion7();

        public DateTime DateCreated { get; set; }

        public UserPermissions Permissions { get; set; } = UserPermissions.BasicUser;

        // Helper methods for permission checking
        public bool HasPermission(UserPermissions permission)
        {
            return (Permissions & permission) == permission;
        }

        public bool HasAnyPermission(UserPermissions permissions)
        {
            return (Permissions & permissions) != UserPermissions.None;
        }

        public void GrantPermission(UserPermissions permission)
        {
            Permissions |= permission;
        }

        public void RevokePermission(UserPermissions permission)
        {
            Permissions &= ~permission;
        }

        public bool IsAdmin => HasPermission(UserPermissions.Administrator);

        // Role-based convenience properties
        public bool IsBookModerator => HasPermission(UserPermissions.BookModerator);
        public bool IsUserModerator => HasPermission(UserPermissions.UserModerator);
    }
}