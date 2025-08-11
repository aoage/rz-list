using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class UserList
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        // Foreign key to User
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        // Navigation property for related UserBook entries
        public virtual ICollection<UserBook> UserBooks { get; set; } = null!;
    }
}