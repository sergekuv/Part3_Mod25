using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Part3_Mod25_HomeWork.Repositories
{
    public static class Queries
    {
        /* Получать список книг определенного жанра и вышедших между определенными годами.
        Получать количество книг определенного автора в библиотеке.
        Получать количество книг определенного жанра в библиотеке.
        */
        public static List<Book> GetBooks(AppContext db, string genreName = "", int minYear = 0, int maxYear = 2200, string authorName = "")
        {
            // Непонятно, почему этот запрос дает разные результаты в зависимости от того, заполнялась ли перед этим БД, или используется уже заполненная
            //List<Book> books = db.Books.Where(book => (book.Genre.Name == genreName || genreName == "") && (book.Year >= minYear)
            //        && (book.Year <= maxYear) && (authorName == "" || book.Authors.Where(author => author.Name == authorName).Count() > 0)).ToList();

            List<Book> books = db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).
                Where(book => (book.Genre.Name == genreName || genreName == "") && (book.Year >= minYear)
                    && (book.Year <= maxYear) && (authorName == "" || book.Authors.Where(author => author.Name == authorName).Count() > 0)).ToList();

            return books;
        }

        /* Получение списка всех книг, отсортированного в алфавитном порядке по названию.
           Получение списка всех книг, отсортированного в порядке убывания года их выхода.
            ( Признак сортировки нужно ввести; 
        */
        public static List<Book> SortedBooks(AppContext db, string paramToSort)
        {
            List<Book> books = new();
            try
            {
                books = db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).OrderBy(paramToSort).ToList();
            }
            catch // если пользователь ввел некорректный параметр сортировки, получит несортированный список
            {
                books = db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).ToList();
            }
            return books;
            //return db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).OrderBy(paramToSort).ToList();
        }

        /*
        Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.

        Надеюсь, такой вариант подойдет.. 
        */
        public static string BookStatus(AppContext db, string authorName, string bookName)
        {
            var books = db.Books.Include(b => b.Genre).Include(b => b.Authors).Include(b => b.CurrentUser).
                        Where(book => (book.Name.Contains(bookName) || String.IsNullOrEmpty(bookName)) &&
                        (book.Authors.Where(author => (author.Name.Contains(authorName) || string.IsNullOrEmpty(authorName))).Count() > 0));
            var booksInLibrary = books.Count();
            var booksTaken = books.Where(book => book.CurrentUser != null).Count();
            return $"There are {booksInLibrary} matching books in the library; {booksTaken} of them are currently in use";
        }

        // Получать количество книг на руках у пользователя.
        public static int BooksTaskenByUser(AppContext db, string userName)
        {
            return db.Books.Where(book => book.CurrentUser.Name == userName).Count();
        }

        // Получение последней вышедшей книги. - не уверен, что понимаю эту задачу. Одна из книг с самым последним годом издания? 
        public static Book LastBookByYear(AppContext db)
        {
            return db.Books.OrderBy(book => book.Year).LastOrDefault();
        }


    }


}
