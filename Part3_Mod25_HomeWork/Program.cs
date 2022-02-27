using System;
using System.Collections.Generic;
using Part3_Mod25_HomeWork.Repositories;


namespace Part3_Mod25_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                using (AppContext db = new()) // Цитата из https://docs.microsoft.com/ru-ru/ef/core/dbcontext-configuration/ 
                                              //"Экземпляр DbContext предназначен для выполнения DbContextединицы работы. 
                                              // Это означает, что время существования экземпляра DbContext обычно невелико."
                                              // Вопрос: правильно ли создавать контекст для каждой операции, или лучше делать так, 
                                              // как написано в этом коде - один контекст для заполнения и всех операций тестирования? 
                {
                    FillDb(db); // Заполняем таблицы данными
                    RunQueries(db); // Выполняем запросы
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("-- end --");
        }

        #region FillDb
        private static void FillDb(AppContext db)  // Заполняем таблицы данными
        {
            try
            {
                FillGenres(db);
                FillAuthors(db);
                FillUsers(db);
                FillBooks(db);
                TakeBooks(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while filling DB\n\n" + ex.Message);
            }
        }
        private static void FillGenres(AppContext db)
        {
            int i = GenreRepository.AddGenre(db, new Genre { Name = "Fantasy" });
            i += GenreRepository.AddGenre(db, new Genre { Name = "NonFiction" });
            i += GenreRepository.AddGenre(db, new Genre { Name = "Thriller" });
            Console.WriteLine("Added " + i + " genres of 3");
        }
        private static void FillAuthors(AppContext db)
        {
            int i = AuthorRepository.AddAuthor(db, new Author { Name = "Allen" });
            i += AuthorRepository.AddAuthor(db, new Author { Name = "Backer" });
            i += AuthorRepository.AddAuthor(db, new Author { Name = "Collins" });
            Console.WriteLine("Added " + i + " authors of 3");
        }
        private static void FillUsers(AppContext db)
        {
            int i = UserRepository.AddUser(db, new User { Name = "Zak", Email = "zak@mail.mm" });
            i += UserRepository.AddUser(db, new User { Name = "Willson", Email = "will@mail.mm" });
            i += UserRepository.AddUser(db, new User { Name = "Yukki", Email = "yukki@mail.mm" });
            Console.WriteLine("Added " + i + " users of 3");
        }
        private static void FillBooks(AppContext db)
        {
            List<Author> authors = AuthorRepository.GetAllAuthors(db);
            List<Genre> genres = GenreRepository.GetAllGenres(db);
            int i = 0;
            foreach(Author a in authors)
            {
                List<Author> authorsList = new();
                authorsList.Add(a);
                foreach(Genre g in genres)
                {
                    i += BookRepository.AddBook(db, new Book { Name = "Book1", Year = 1798 + i, Genre = g, Authors = authorsList});
                    i += BookRepository.AddBook(db, new Book { Name = "Book2", Year = 1798 + i, Genre = g, Authors = authorsList });
                }
            }
            i += BookRepository.AddBook(db, new Book { Name = "Special", Year = 2000, Genre = genres[0], Authors = authors });
            Console.WriteLine("Affected " + i + " rows (in different tables) when adding " + BookRepository.GetAllBooks(db).Count + " books");

        }
        private static void TakeBooks(AppContext db)
        {
            List<User> users = UserRepository.GetAllUsers(db);
            List<Book> books = BookRepository.GetAllBooks(db);
            for (int i = 0; i <= books.Count / 2; i++)
            {
                books[i].CurrentUser = users[i % 2];
            }
            int j = db.SaveChanges();
            Console.WriteLine("TekeBooks process: affected " + j + " rows");
        }
        #endregion
        private static void RunQueries(AppContext db) // Списки книг, удовлетворяющих заданным критериям
        {
            try
            {
                // Списки книг, удовлетворяющих заданным критериям
                List<Book> books = new();
                Console.WriteLine("-- All Books --");
                books = Queries.GetBooks(db);
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }

                Console.WriteLine("\n--Fantasy books written after 1803 by Backer--");
                books = Queries.GetBooks(db, genreName: "Fantasy", minYear: 1804, authorName: "Backer" );
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }

                Console.Write("\nEnter parameter to sort books like this: Name or Year desc ");
                string paramToSort = Console.ReadLine();
                Console.WriteLine($"-- All books sorted by {paramToSort} --");
                books = Queries.SortedBooks(db, paramToSort);
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }

                Console.Write("\nEnter book name to search in the library (or press Enter): ");
                string bookToSearch= Console.ReadLine();
                Console.Write("\nEnter author name to search in the library (or press Enter): ");
                string authorToSearch = Console.ReadLine();
                Console.WriteLine(Queries.BookStatus(db, authorName: authorToSearch, bookName: bookToSearch));

                Console.WriteLine("\nEnter user name to know how many books he currently has on hands: ");
                string userName = Console.ReadLine();
                Console.WriteLine($"{userName} has {Queries.BooksTaskenByUser(db, userName)} book at the moment");

                Console.WriteLine("\nA book with the latest Year: ");
                Console.WriteLine(Queries.LastBookByYear(db).ToString());
                
                for (int i = 0; i < 2; i++) // Попробуем дважды удалить книгу с Id = 5
                {
                    Console.WriteLine("\nGetBookById for id = 5 ");
                    Book book5 = BookRepository.GetBookById(db, 5);
                    if (book5 != null)
                    {
                        Console.WriteLine(book5);
                        BookRepository.DeleteBookById(db, 5);
                        Console.WriteLine($"Deleting book with id 5 affected {db.SaveChanges()} rows");
                    }
                    else
                    {
                        Console.WriteLine("no book with id 5 found");
                    }
                }

                Console.WriteLine("\nGetBookById for id = 6 ");
                Book book6 = BookRepository.GetBookById(db, 6);
                if (book6 != null)
                {
                    Console.WriteLine(book6);
                    BookRepository.UpdateBookYear(db, id:6, year:1900);
                    int affected = db.SaveChanges();
                    Console.WriteLine($"Updating book with id 6 affected {affected} rows\nNow book with Id6 looks like:");
                    Book newBook6 = BookRepository.GetBookById(db, 6);
                    Console.WriteLine(book6);
                }
                else
                {
                    Console.WriteLine("no book with id 6 found");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while running queries\n\n" + ex.Message);
            }
        }
    }
}
