using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork.Repositories
{
    public static class AuthorRepository
    {
        public static Author GetAuthorById(AppContext db, int id)
        {
            // нужен ли тут try/catch, или лучше ловить исключения в вызывающем методе?
            return db.Authors.FirstOrDefault(author => author.Id == id);
        }
        public static Author GetAuthorByName(AppContext db, string name)
        {
            return db.Authors.FirstOrDefault(author => author.Name == name);
        }

        public static List<Author> GetAllAuthors(AppContext db)
        {
            return db.Authors.ToList();
        }
        public static int AddAuthor(AppContext db, Author author)
        {
            db.Authors.Add(author);
            return db.SaveChanges();
        }
        public static int DeleteAuthor(AppContext db, Author author)
        {
            db.Authors.Remove(author);
            return db.SaveChanges();
        }
        public static int DeleteAuthorById(AppContext db, int id)
        {
            db.Authors.Remove(GetAuthorById(db, id));
            return db.SaveChanges();
        }

        public static int UpdateAuthorName(AppContext db, int id, string name)
        {
            Author author = GetAuthorById(db, id);
            author.Name = name;
            db.Authors.Update(author);
            return db.SaveChanges();
        }

    }
}
