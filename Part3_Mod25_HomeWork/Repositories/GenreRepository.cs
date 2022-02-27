using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork.Repositories
{
    public static class GenreRepository
    {
        public static int AddGenre(AppContext db, Genre genre)
        {
            db.Genres.Add(genre);
            return db.SaveChanges();
        }
        public static Genre GetGenreById(AppContext db, int id)
        {
            // нужен ли тут try/catch, или лучше ловить исключения в вызывающем методе?
            return db.Genres.FirstOrDefault(genre => genre.Id == id);
        }
        public static Genre GetGenreByName(AppContext db, string name)
        {
            return db.Genres.FirstOrDefault(genre => genre.Name == name);
        }

        public static List<Genre> GetAllGenres(AppContext db)
        {
            return db.Genres.ToList();
        }
        public static int DeleteGenre(AppContext db, Genre genre)
        {
            db.Genres.Remove(genre);
            return db.SaveChanges();
        }
        public static int DeleteGenreById(AppContext db, int id)
        {
            db.Genres.Remove(GetGenreById(db, id));
            return db.SaveChanges();
        }

        public static int UpdateGenreName(AppContext db, int id, string name)
        {
            Genre genre = GetGenreById(db, id);
            genre.Name = name;
            db.Genres.Update(genre);
            return db.SaveChanges();
        }

    }
}
