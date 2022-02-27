using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork
{
    public class BookRepository
    {
        public static Book GetBookById(AppContext db, int id)
        {
            // нужен ли тут try/catch, или лучше ловить исключения в вызывающем методе?
            // Чтобы получить поля, хранящиеся в других таблицах, нужно добавить .Include(book => book.Genre) и т.п.
            return db.Books.FirstOrDefault(book => book.Id == id);
        }
        public static List<Book> GetAllBooks(AppContext db)
        {
            return db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).ToList();
        }
        public static int AddBook(AppContext db, Book book)
        {
            db.Books.Add(book);
            return db.SaveChanges();
        }
        public static int DeleteBook(AppContext db, Book book)
        {
            db.Books.Remove(book);
            return db.SaveChanges();
        }
        public static int DeleteBookById(AppContext db, int id)
        {
            db.Books.Remove(GetBookById(db, id));
            return db.SaveChanges();
        }

        public static int UpdateBookYear(AppContext db, int id, int year)
        {
            Book book = GetBookById(db, id);
            book.Year = year;
            db.Books.Update(book);
            return db.SaveChanges();
        }
        public static int AddBookAuthor(AppContext db, int id, Author author)
        {
            Book book = GetBookById(db, id);
            book.Authors.Add(author);
            db.Books.Update(book);
            return db.SaveChanges();
        }

        public static int TakeBook (AppContext db,int id, User user)
        {
            Book book = GetBookById(db, id);
            book.CurrentUser = user;
            db.Books.Update(book);
            return db.SaveChanges();
        }
        public static int ReturnBook(AppContext db, int id)
        {
            Book book = GetBookById(db, id);
            book.CurrentUser = null;
            db.Books.Update(book);
            return db.SaveChanges();
        }

    }
}
