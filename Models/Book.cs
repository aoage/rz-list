using System.ComponentModel.DataAnnotations;

namespace RzList.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Author { get; set; } = string.Empty;
        
        [StringLength(13)]
        public string? ISBN { get; set; }

        public DateTime? PublishedDate { get; set; }
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        public long? CharacterCount { get; set; }
        
        [StringLength(500)]
        public string? Genre { get; set; }
        
        [StringLength(255)]
        public string? Publisher { get; set; }
        
        [StringLength(500)]
        public string? CoverImageUrl { get; set; }
        
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}