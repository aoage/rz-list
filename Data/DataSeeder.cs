using Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class DataSeeder
    {
        public static async Task SeedDatabase(RzListDbContext context)
        {
            // Check if data already exists
            if (await context.Books.AnyAsync())
            {
                Console.WriteLine("Database already contains books. Skipping seeding.");
                return;
            }

            Console.WriteLine("Seeding database with test data...");

            var books = GetTestBooks();

            // Add random DateAdded times over the past year
            var random = new Random();
            foreach (var book in books)
            {
                book.DateAdded = DateTime.UtcNow.AddDays(-random.Next(0, 365));
            }

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();

            Console.WriteLine($"Successfully seeded {books.Count} books into the database.");
        }

        public static async Task ClearAndReseedDatabase(RzListDbContext context)
        {
            Console.WriteLine("Clearing existing books and reseeding...");

            // Remove all existing books
            context.Books.RemoveRange(context.Books);
            await context.SaveChangesAsync();

            // Add new test data
            await SeedDatabase(context);
        }

        private static List<Book> GetTestBooks()
        {
            return new List<Book>
            {
                // Classic Literature
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Publisher = "Scribner", Genre = Genre.Classic, Description = "A classic American novel set in the Jazz Age that explores themes of decadence, idealism, and the American Dream.", PublishedDate = new DateTime(1925, 4, 10), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780743273565-L.jpg", CharacterCount = 180000 },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Publisher = "J.B. Lippincott & Co.", Genre = Genre.Classic, Description = "A gripping tale of racial injustice and childhood innocence in the American South.", PublishedDate = new DateTime(1960, 7, 11), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780061120084-L.jpg", CharacterCount = 280000 },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Publisher = "T. Egerton", Genre = Genre.Classic, Description = "A romantic novel of manners set in Georgian England.", PublishedDate = new DateTime(1813, 1, 28), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141439518-L.jpg", CharacterCount = 387000 },
                new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Publisher = "Little, Brown and Company", Genre = Genre.Classic, Description = "A controversial novel about teenage rebellion and alienation.", PublishedDate = new DateTime(1951, 7, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780316769174-L.jpg", CharacterCount = 234000 },
                new Book { Title = "Wuthering Heights", Author = "Emily Brontë", Publisher = "Thomas Cautley Newby", Genre = Genre.Classic, Description = "A dark tale of passion and revenge on the Yorkshire moors.", PublishedDate = new DateTime(1847, 12, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141439556-L.jpg", CharacterCount = 340000 },
                new Book { Title = "Jane Eyre", Author = "Charlotte Brontë", Publisher = "Smith, Elder & Co.", Genre = Genre.Classic, Description = "The story of an orphaned girl who becomes a governess and finds love.", PublishedDate = new DateTime(1847, 10, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141441146-L.jpg", CharacterCount = 455000 },
                new Book { Title = "Moby Dick", Author = "Herman Melville", Publisher = "Harper & Brothers", Genre = Genre.Classic, Description = "The epic tale of Captain Ahab's obsessive quest for revenge against the white whale.", PublishedDate = new DateTime(1851, 10, 18), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780142437247-L.jpg", CharacterCount = 660000 },
                new Book { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", Publisher = "Ward, Lock & Co.", Genre = Genre.Classic, Description = "A philosophical novel about youth, beauty, and moral corruption.", PublishedDate = new DateTime(1890, 6, 20), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141439570-L.jpg", CharacterCount = 330000 },

                // Science Fiction
                new Book { Title = "1984", Author = "George Orwell", Publisher = "Secker & Warburg", Genre = Genre.ScienceFiction, Description = "A dystopian social science fiction novel about totalitarian control.", PublishedDate = new DateTime(1949, 6, 8), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780451524935-L.jpg", CharacterCount = 328000 },
                new Book { Title = "Dune", Author = "Frank Herbert", Publisher = "Chilton Books", Genre = Genre.ScienceFiction, Description = "A science fiction epic set in the distant future on the desert planet Arrakis.", PublishedDate = new DateTime(1965, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780441172719-L.jpg", CharacterCount = 688000 },
                new Book { Title = "Foundation", Author = "Isaac Asimov", Publisher = "Gnome Press", Genre = Genre.ScienceFiction, Description = "The first novel in the Foundation series about psychohistory and the fall of empires.", PublishedDate = new DateTime(1951, 5, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553293357-L.jpg", CharacterCount = 244000 },
                new Book { Title = "Neuromancer", Author = "William Gibson", Publisher = "Ace Books", Genre = Genre.ScienceFiction, Description = "A seminal cyberpunk novel that defined the genre.", PublishedDate = new DateTime(1984, 7, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780441569595-L.jpg", CharacterCount = 271000 },
                new Book { Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Publisher = "Pan Books", Genre = Genre.ScienceFiction, Description = "A comedy science fiction series about the absurdity of existence.", PublishedDate = new DateTime(1979, 10, 12), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780345391803-L.jpg", CharacterCount = 177000 },
                new Book { Title = "Ender's Game", Author = "Orson Scott Card", Publisher = "Tor Books", Genre = Genre.ScienceFiction, Description = "A military science fiction novel about child soldiers training for alien war.", PublishedDate = new DateTime(1985, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780812550702-L.jpg", CharacterCount = 324000 },
                new Book { Title = "Brave New World", Author = "Aldous Huxley", Publisher = "Chatto & Windus", Genre = Genre.ScienceFiction, Description = "A dystopian novel about a society engineered for happiness and conformity.", PublishedDate = new DateTime(1932, 8, 30), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780060850524-L.jpg", CharacterCount = 227000 },
                new Book { Title = "The Martian", Author = "Andy Weir", Publisher = "Crown Publishing", Genre = Genre.ScienceFiction, Description = "A survival story about an astronaut stranded on Mars.", PublishedDate = new DateTime(2011, 9, 27), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553418026-L.jpg", CharacterCount = 387000 },

                // Fantasy
                new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Publisher = "Allen & Unwin", Genre = Genre.Fantasy, Description = "An epic high fantasy novel about the quest to destroy the One Ring.", PublishedDate = new DateTime(1954, 7, 29), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780618640157-L.jpg", CharacterCount = 1200000 },
                new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Publisher = "Allen & Unwin", Genre = Genre.Fantasy, Description = "A children's fantasy novel and prelude to The Lord of the Rings.", PublishedDate = new DateTime(1937, 9, 21), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780547928227-L.jpg", CharacterCount = 305000 },
                new Book { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Publisher = "Bloomsbury", Genre = Genre.Fantasy, Description = "The first novel in the Harry Potter series about a boy wizard.", PublishedDate = new DateTime(1997, 6, 26), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780747532699-L.jpg", CharacterCount = 309000 },
                new Book { Title = "A Game of Thrones", Author = "George R.R. Martin", Publisher = "Bantam Spectra", Genre = Genre.Fantasy, Description = "The first book in A Song of Ice and Fire series set in Westeros.", PublishedDate = new DateTime(1996, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553103540-L.jpg", CharacterCount = 694000 },
                new Book { Title = "The Name of the Wind", Author = "Patrick Rothfuss", Publisher = "DAW Books", Genre = Genre.Fantasy, Description = "The first book in The Kingkiller Chronicle about Kvothe's legend.", PublishedDate = new DateTime(2007, 3, 27), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780756404079-L.jpg", CharacterCount = 662000 },
                new Book { Title = "The Way of Kings", Author = "Brandon Sanderson", Publisher = "Tor Books", Genre = Genre.Fantasy, Description = "The first book in The Stormlight Archive epic fantasy series.", PublishedDate = new DateTime(2010, 8, 31), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780765326355-L.jpg", CharacterCount = 1007000 },
                new Book { Title = "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", Author = "C.S. Lewis", Publisher = "Geoffrey Bles", Genre = Genre.Fantasy, Description = "Children discover a magical world through a wardrobe.", PublishedDate = new DateTime(1950, 10, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780064471046-L.jpg", CharacterCount = 183000 },
                new Book { Title = "The Dark Tower: The Gunslinger", Author = "Stephen King", Publisher = "Donald M. Grant", Genre = Genre.Fantasy, Description = "The first book in Stephen King's Dark Tower series.", PublishedDate = new DateTime(1982, 6, 10), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780452284715-L.jpg", CharacterCount = 224000 },
                new Book { Title = "Lord of Mysteries, Vol. 1: The Clown, Part I", Author = "Cuttlefish That Loves Diving", Publisher = "Yen On", Genre = Genre.Fantasy, Description = "In the storm of steam and machinery, who can achieve the extraordinary? In the fog of history and darkness, who whispers? When Zhou Mingrui wakes up bloody and dazed, he finds himself in a world of guns, factories, airships, and difference engines. But underneath the surface of all this industry, there exists a secret society revolving around potions, divination, sealed artifacts, and much more.", PublishedDate = new DateTime(2025, 7, 29), CoverImageUrl = "https://m.media-amazon.com/images/I/81BKBqWOY6L._SL1500_.jpg", CharacterCount = 580000 },

                // Mystery & Thriller
                new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Publisher = "Doubleday", Genre = Genre.Mystery, Description = "A mystery thriller combining art, history, and religious conspiracy.", PublishedDate = new DateTime(2003, 3, 18), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780385504201-L.jpg", CharacterCount = 454000 },
                new Book { Title = "The Girl with the Dragon Tattoo", Author = "Stieg Larsson", Publisher = "Norstedts Förlag", Genre = Genre.Thriller, Description = "A psychological crime thriller about a journalist and a hacker.", PublishedDate = new DateTime(2005, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780307269751-L.jpg", CharacterCount = 644000 },
                new Book { Title = "Gone Girl", Author = "Gillian Flynn", Publisher = "Crown Publishing Group", Genre = Genre.Thriller, Description = "A psychological thriller about a marriage gone terribly wrong.", PublishedDate = new DateTime(2012, 6, 5), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780307588364-L.jpg", CharacterCount = 577000 },
                new Book { Title = "The Big Sleep", Author = "Raymond Chandler", Publisher = "Knopf", Genre = Genre.Mystery, Description = "A hardboiled detective novel featuring private eye Philip Marlowe.", PublishedDate = new DateTime(1939, 2, 6), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780394758282-L.jpg", CharacterCount = 231000 },
                new Book { Title = "And Then There Were None", Author = "Agatha Christie", Publisher = "Collins Crime Club", Genre = Genre.Mystery, Description = "A mystery novel by the queen of crime about ten strangers on an island.", PublishedDate = new DateTime(1939, 11, 6), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780062073488-L.jpg", CharacterCount = 267000 },
                new Book { Title = "The Silence of the Lambs", Author = "Thomas Harris", Publisher = "St. Martin's Press", Genre = Genre.Thriller, Description = "A psychological horror thriller featuring Hannibal Lecter.", PublishedDate = new DateTime(1988, 5, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780312022822-L.jpg", CharacterCount = 352000 },
                new Book { Title = "In the Woods", Author = "Tana French", Publisher = "Viking", Genre = Genre.Mystery, Description = "An atmospheric mystery set in Ireland with psychological depth.", PublishedDate = new DateTime(2007, 5, 17), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780670038602-L.jpg", CharacterCount = 429000 },
                new Book { Title = "The Maltese Falcon", Author = "Dashiell Hammett", Publisher = "Knopf", Genre = Genre.Mystery, Description = "A classic detective novel about private investigator Sam Spade.", PublishedDate = new DateTime(1930, 2, 14), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780679722649-L.jpg", CharacterCount = 217000 },

                // Horror
                new Book { Title = "The Shining", Author = "Stephen King", Publisher = "Doubleday", Genre = Genre.Horror, Description = "A horror novel about a family isolated in a haunted hotel.", PublishedDate = new DateTime(1977, 1, 28), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780385121675-L.jpg", CharacterCount = 659000 },
                new Book { Title = "Dracula", Author = "Bram Stoker", Publisher = "Archibald Constable and Company", Genre = Genre.Horror, Description = "The classic vampire novel that defined the genre.", PublishedDate = new DateTime(1897, 5, 26), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780486411095-L.jpg", CharacterCount = 418000 },
                new Book { Title = "Frankenstein", Author = "Mary Shelley", Publisher = "Lackington, Hughes, Harding, Mavor & Jones", Genre = Genre.Horror, Description = "The modern Prometheus - a tale of scientific ambition gone wrong.", PublishedDate = new DateTime(1818, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780486282114-L.jpg", CharacterCount = 278000 },
                new Book { Title = "The Exorcist", Author = "William Peter Blatty", Publisher = "Harper & Row", Genre = Genre.Horror, Description = "A supernatural horror novel about demonic possession.", PublishedDate = new DateTime(1971, 5, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780060522971-L.jpg", CharacterCount = 340000 },
                new Book { Title = "Pet Sematary", Author = "Stephen King", Publisher = "Doubleday", Genre = Genre.Horror, Description = "A horror novel about death, resurrection, and the price of grief.", PublishedDate = new DateTime(1983, 11, 14), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780385182034-L.jpg", CharacterCount = 374000 },
                new Book { Title = "The Haunting of Hill House", Author = "Shirley Jackson", Publisher = "Viking Press", Genre = Genre.Horror, Description = "A psychological horror novel about a haunted house.", PublishedDate = new DateTime(1959, 10, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780143039983-L.jpg", CharacterCount = 246000 },

                // Young Adult
                new Book { Title = "The Hunger Games", Author = "Suzanne Collins", Publisher = "Scholastic", Genre = Genre.YoungAdult, Description = "A dystopian novel about a televised fight to the death.", PublishedDate = new DateTime(2008, 9, 14), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780439023481-L.jpg", CharacterCount = 374000 },
                new Book { Title = "Divergent", Author = "Veronica Roth", Publisher = "Katherine Tegen Books", Genre = Genre.YoungAdult, Description = "A dystopian novel set in post-apocalyptic Chicago with faction system.", PublishedDate = new DateTime(2011, 4, 25), CoverImageUrl = "https://covers.openlibrary.org/b/id/7276222-L.jpg", CharacterCount = 487000 },
                new Book { Title = "The Fault in Our Stars", Author = "John Green", Publisher = "Dutton Books", Genre = Genre.YoungAdult, Description = "A romantic tragedy about two teenagers with cancer.", PublishedDate = new DateTime(2012, 1, 10), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780525478812-L.jpg", CharacterCount = 313000 },
                new Book { Title = "Percy Jackson: The Lightning Thief", Author = "Rick Riordan", Publisher = "Miramax Books", Genre = Genre.YoungAdult, Description = "Modern mythology adventure about a boy who discovers he's a demigod.", PublishedDate = new DateTime(2005, 6, 28), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780786838653-L.jpg", CharacterCount = 377000 },
                new Book { Title = "The Maze Runner", Author = "James Dashner", Publisher = "Delacorte Press", Genre = Genre.YoungAdult, Description = "A dystopian novel about boys trapped in a changing maze.", PublishedDate = new DateTime(2009, 10, 6), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780385737944-L.jpg", CharacterCount = 374000 },
                new Book { Title = "Twilight", Author = "Stephenie Meyer", Publisher = "Little, Brown and Company", Genre = Genre.YoungAdult, Description = "A vampire romance between a human girl and a vampire.", PublishedDate = new DateTime(2005, 10, 5), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780316015844-L.jpg", CharacterCount = 498000 },

                // Non-Fiction & Biography
                new Book { Title = "Becoming", Author = "Michelle Obama", Publisher = "Crown Publishing Group", Genre = Genre.Biography, Description = "The memoir of former First Lady Michelle Obama.", PublishedDate = new DateTime(2018, 11, 13), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781524763138-L.jpg", CharacterCount = 426000 },
                new Book { Title = "Educated", Author = "Tara Westover", Publisher = "Random House", Genre = Genre.Biography, Description = "A memoir about education and family in a survivalist Mormon family.", PublishedDate = new DateTime(2018, 2, 20), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780399590504-L.jpg", CharacterCount = 334000 },
                new Book { Title = "Steve Jobs", Author = "Walter Isaacson", Publisher = "Simon & Schuster", Genre = Genre.Biography, Description = "The authorized biography of Apple co-founder Steve Jobs.", PublishedDate = new DateTime(2011, 10, 24), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781451648539-L.jpg", CharacterCount = 656000 },
                new Book { Title = "Sapiens", Author = "Yuval Noah Harari", Publisher = "Harvill Secker", Genre = Genre.NonFiction, Description = "A brief history of humankind from stone age to modern era.", PublishedDate = new DateTime(2011, 9, 4), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780062316097-L.jpg", CharacterCount = 443000 },
                new Book { Title = "The Immortal Life of Henrietta Lacks", Author = "Rebecca Skloot", Publisher = "Crown Publishers", Genre = Genre.NonFiction, Description = "Science, ethics, and the story of HeLa cells in medical research.", PublishedDate = new DateTime(2010, 2, 2), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781400052172-L.jpg", CharacterCount = 381000 },
                new Book { Title = "Guns, Germs, and Steel", Author = "Jared Diamond", Publisher = "W. W. Norton & Company", Genre = Genre.NonFiction, Description = "The fates of human societies and geographical determinism.", PublishedDate = new DateTime(1997, 3, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780393317558-L.jpg", CharacterCount = 480000 },

                // Romance
                new Book { Title = "Outlander", Author = "Diana Gabaldon", Publisher = "Delacorte Press", Genre = Genre.Romance, Description = "Time-traveling historical romance between Claire and Jamie.", PublishedDate = new DateTime(1991, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780440212560-L.jpg", CharacterCount = 627000 },
                new Book { Title = "The Notebook", Author = "Nicholas Sparks", Publisher = "Warner Books", Genre = Genre.Romance, Description = "A love story that spans decades, told through a notebook.", PublishedDate = new DateTime(1996, 10, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780446520805-L.jpg", CharacterCount = 214000 },
                new Book { Title = "Me Before You", Author = "Jojo Moyes", Publisher = "Pamela Dorman Books", Genre = Genre.Romance, Description = "An emotional contemporary romance about love and life choices.", PublishedDate = new DateTime(2012, 1, 5), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780670026609-L.jpg", CharacterCount = 340000 },
                new Book { Title = "The Time Traveler's Wife", Author = "Audrey Niffenegger", Publisher = "MacAdam/Cage", Genre = Genre.Romance, Description = "A love story complicated by uncontrollable time travel.", PublishedDate = new DateTime(2003, 9, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781931561464-L.jpg", CharacterCount = 518000 },

                // Self-Help & Business
                new Book { Title = "Atomic Habits", Author = "James Clear", Publisher = "Avery", Genre = Genre.SelfHelp, Description = "An easy and proven way to build good habits and break bad ones.", PublishedDate = new DateTime(2018, 10, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780735211292-L.jpg", CharacterCount = 320000 },
                new Book { Title = "The 7 Habits of Highly Effective People", Author = "Stephen Covey", Publisher = "Free Press", Genre = Genre.SelfHelp, Description = "Powerful lessons in personal change and effectiveness.", PublishedDate = new DateTime(1989, 8, 15), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780743269513-L.jpg", CharacterCount = 372000 },
                new Book { Title = "Think and Grow Rich", Author = "Napoleon Hill", Publisher = "The Ralston Society", Genre = Genre.Business, Description = "The classic guide to wealth and success.", PublishedDate = new DateTime(1937, 8, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781585424331-L.jpg", CharacterCount = 320000 },
                new Book { Title = "Good to Great", Author = "Jim Collins", Publisher = "HarperBusiness", Genre = Genre.Business, Description = "Why some companies make the leap and others don't.", PublishedDate = new DateTime(2001, 10, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780066620992-L.jpg", CharacterCount = 300000 },
                new Book { Title = "How to Win Friends and Influence People", Author = "Dale Carnegie", Publisher = "Simon & Schuster", Genre = Genre.SelfHelp, Description = "Timeless advice on building relationships and influencing others.", PublishedDate = new DateTime(1936, 10, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780671027032-L.jpg", CharacterCount = 288000 },

                // Philosophy & Psychology
                new Book { Title = "Man's Search for Meaning", Author = "Viktor E. Frankl", Publisher = "Beacon Press", Genre = Genre.Philosophy, Description = "A Holocaust survivor's guide to finding purpose in suffering.", PublishedDate = new DateTime(1946, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780807014295-L.jpg", CharacterCount = 165000 },
                new Book { Title = "Thinking, Fast and Slow", Author = "Daniel Kahneman", Publisher = "Farrar, Straus and Giroux", Genre = Genre.Psychology, Description = "How we make decisions and the psychology of judgment.", PublishedDate = new DateTime(2011, 10, 25), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780374275631-L.jpg", CharacterCount = 499000 },
                new Book { Title = "The Republic", Author = "Plato", Publisher = "Ancient Philosophy", Genre = Genre.Philosophy, Description = "A Socratic dialogue on justice and the ideal state.", PublishedDate = new DateTime(1, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780140455113-L.jpg", CharacterCount = 380000 },
                new Book { Title = "Meditations", Author = "Marcus Aurelius", Publisher = "Ancient Philosophy", Genre = Genre.Philosophy, Description = "Personal writings on Stoic philosophy by a Roman Emperor.", PublishedDate = new DateTime(180, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780486298238-L.jpg", CharacterCount = 120000 },

                // Children's Books
                new Book { Title = "Where the Wild Things Are", Author = "Maurice Sendak", Publisher = "Harper & Row", Genre = Genre.Children, Description = "A children's picture book about imagination and adventure.", PublishedDate = new DateTime(1963, 4, 9), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780060254926-L.jpg", CharacterCount = 338 },
                new Book { Title = "The Very Hungry Caterpillar", Author = "Eric Carle", Publisher = "World Publishing Company", Genre = Genre.Children, Description = "A classic children's book about growth and transformation.", PublishedDate = new DateTime(1969, 6, 3), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780399226908-L.jpg", CharacterCount = 224 },
                new Book { Title = "Charlotte's Web", Author = "E.B. White", Publisher = "Harper & Brothers", Genre = Genre.Children, Description = "A story of friendship between a pig named Wilbur and a spider named Charlotte.", PublishedDate = new DateTime(1952, 10, 15), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780064400558-L.jpg", CharacterCount = 187000 },
                new Book { Title = "Matilda", Author = "Roald Dahl", Publisher = "Jonathan Cape", Genre = Genre.Children, Description = "A girl with extraordinary powers overcomes adversity.", PublishedDate = new DateTime(1988, 4, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780142410370-L.jpg", CharacterCount = 225000 },

                // Adventure & Contemporary
                new Book { Title = "Into the Wild", Author = "Jon Krakauer", Publisher = "Villard", Genre = Genre.Adventure, Description = "The true story of Christopher McCandless's journey into the Alaskan wilderness.", PublishedDate = new DateTime(1996, 1, 13), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780385486804-L.jpg", CharacterCount = 203000 },
                new Book { Title = "Life of Pi", Author = "Yann Martel", Publisher = "Knopf Canada", Genre = Genre.Adventure, Description = "A boy, a tiger, and survival at sea - a story of faith and endurance.", PublishedDate = new DateTime(2001, 9, 11), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780151008117-L.jpg", CharacterCount = 460000 },
                new Book { Title = "The Kite Runner", Author = "Khaled Hosseini", Publisher = "Riverhead Books", Genre = Genre.Contemporary, Description = "A story of friendship and redemption set against Afghanistan's tumultuous history.", PublishedDate = new DateTime(2003, 5, 29), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781573222457-L.jpg", CharacterCount = 372000 },
                new Book { Title = "The Book Thief", Author = "Markus Zusak", Publisher = "Picador", Genre = Genre.Contemporary, Description = "A story narrated by Death during WWII about a girl who steals books.", PublishedDate = new DateTime(2005, 9, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780375831003-L.jpg", CharacterCount = 584000 },
                new Book { Title = "Wild", Author = "Cheryl Strayed", Publisher = "Knopf", Genre = Genre.Adventure, Description = "A memoir about hiking the Pacific Crest Trail as a journey of self-discovery.", PublishedDate = new DateTime(2012, 3, 20), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780307592736-L.jpg", CharacterCount = 315000 },

                // Poetry & Drama
                new Book { Title = "Leaves of Grass", Author = "Walt Whitman", Publisher = "Self-published", Genre = Genre.Poetry, Description = "A collection of poetry celebrating democracy, nature, and the human spirit.", PublishedDate = new DateTime(1855, 7, 4), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780486456768-L.jpg", CharacterCount = 150000 },
                new Book { Title = "The Complete Works of William Shakespeare", Author = "William Shakespeare", Publisher = "First Folio", Genre = Genre.Drama, Description = "The complete collection of Shakespeare's plays and sonnets.", PublishedDate = new DateTime(1623, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780517053614-L.jpg", CharacterCount = 884000 },
                new Book { Title = "The Waste Land", Author = "T.S. Eliot", Publisher = "Boni & Liveright", Genre = Genre.Poetry, Description = "A landmark modernist poem about spiritual desolation.", PublishedDate = new DateTime(1922, 10, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780156948777-L.jpg", CharacterCount = 3533 },

                // Crime & Suspense
                new Book { Title = "The Godfather", Author = "Mario Puzo", Publisher = "G. P. Putnam's Sons", Genre = Genre.Crime, Description = "The saga of a Mafia family and the American Dream.", PublishedDate = new DateTime(1969, 3, 10), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780451205766-L.jpg", CharacterCount = 448000 },
                new Book { Title = "In Cold Blood", Author = "Truman Capote", Publisher = "Random House", Genre = Genre.Crime, Description = "A non-fiction novel about the brutal murder of a Kansas family.", PublishedDate = new DateTime(1966, 1, 17), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780679745587-L.jpg", CharacterCount = 343000 },
                new Book { Title = "The Talented Mr. Ripley", Author = "Patricia Highsmith", Publisher = "Coward-McCann", Genre = Genre.Suspense, Description = "A psychological thriller about identity, obsession, and murder.", PublishedDate = new DateTime(1955, 12, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780393332148-L.jpg", CharacterCount = 287000 },

                // Western
                new Book { Title = "True Grit", Author = "Charles Portis", Publisher = "Simon & Schuster", Genre = Genre.Western, Description = "A young girl seeks to avenge her father's murder in the Old West.", PublishedDate = new DateTime(1968, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781590204597-L.jpg", CharacterCount = 215000 },
                new Book { Title = "Lonesome Dove", Author = "Larry McMurtry", Publisher = "Simon & Schuster", Genre = Genre.Western, Description = "An epic tale of a cattle drive from Texas to Montana.", PublishedDate = new DateTime(1985, 6, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780671683900-L.jpg", CharacterCount = 945000 },

                // Health & Science
                new Book { Title = "The Emperor of All Maladies", Author = "Siddhartha Mukherjee", Publisher = "Scribner", Genre = Genre.Health, Description = "A biography of cancer and the ongoing war against it.", PublishedDate = new DateTime(2010, 11, 16), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781439107959-L.jpg", CharacterCount = 571000 },
                new Book { Title = "A Brief History of Time", Author = "Stephen Hawking", Publisher = "Bantam Doubleday Dell", Genre = Genre.Science, Description = "An exploration of cosmology and the nature of time and space.", PublishedDate = new DateTime(1988, 4, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553380163-L.jpg", CharacterCount = 256000 },
                new Book { Title = "The Selfish Gene", Author = "Richard Dawkins", Publisher = "Oxford University Press", Genre = Genre.Science, Description = "A view of evolution from the gene's perspective.", PublishedDate = new DateTime(1976, 1, 1), CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780199291151-L.jpg", CharacterCount = 360000 }
            };
        }
    }
}
