namespace RzList.Models
{
    [Flags]
    public enum UserPermissions : long
    {
        None = 0,
        
        // Basic user permissions
        ReadOwnBooks = 1 << 0,          // 1
        WriteOwnBooks = 1 << 1,         // 2
        DeleteOwnBooks = 1 << 2,        // 4
        
        // Book catalog permissions
        ReadBookCatalog = 1 << 3,       // 8
        AddBooksToCatalog = 1 << 4,     // 16
        EditBookCatalog = 1 << 5,       // 32
        DeleteFromCatalog = 1 << 6,     // 64
        
        // User management permissions
        ReadUsers = 1 << 7,             // 128
        EditUsers = 1 << 8,             // 256
        DeleteUsers = 1 << 9,           // 512
        ManageUserPermissions = 1 << 10, // 1024
        
        // System permissions
        ViewSystemLogs = 1 << 11,       // 2048
        ManageSystem = 1 << 12,         // 4096
        
        // Convenience groupings
        BasicUser = ReadOwnBooks | WriteOwnBooks | DeleteOwnBooks | ReadBookCatalog,
        BookModerator = BasicUser | AddBooksToCatalog | EditBookCatalog,
        UserModerator = BasicUser | ReadUsers | EditUsers,
        Administrator = ~None  // All bits set (all permissions)
    }
}