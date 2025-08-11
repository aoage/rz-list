using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class UserBook
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        // Foreign Keys
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public Guid? UserListId { get; set; }

        // User-specific data
        public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;

        [Range(0, 5)]
        public decimal? Rating { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        public DateTime? DateStarted { get; set; }

        public DateTime? DateFinished { get; set; }

        [Range(0, 100)]
        public decimal? ProgressPercentage { get; set; }

        public bool IsFavorite { get; set; } = false;

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
        public virtual UserList? UserList { get; set; }

        // Computed property for easy progress display
        public string ProgressDisplay => ProgressPercentage.HasValue
            ? $"{ProgressPercentage:F1}%"
            : "0%";

        // Helper method to update progress
        public void UpdateProgress(long currentCharacterPosition, long totalCharacters)
        {
            if (totalCharacters > 0)
            {
                ProgressPercentage = Math.Round((decimal)currentCharacterPosition / totalCharacters * 100, 1);

                // Auto-update status based on progress
                if (ProgressPercentage >= 100)
                {
                    Status = ReadingStatus.Finished;
                    DateFinished = DateTime.UtcNow;
                }
                else if (ProgressPercentage > 0 && Status == ReadingStatus.WantToRead)
                {
                    Status = ReadingStatus.CurrentlyReading;
                    DateStarted = DateTime.UtcNow;
                }
            }
        }
    }

    public enum ReadingStatus
    {
        WantToRead,
        CurrentlyReading,
        Finished,
        DNF // Did Not Finish
    }
}