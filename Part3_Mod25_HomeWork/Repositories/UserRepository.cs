using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork.Repositories
{
    /// <summary>
    /// Вопрос: как оптимальнее организовать работу с БД?
    /// Правильно ли использовать using (AppContext) в каждом методе, или нужно просто проверить, что он не null, 
    /// или вообще не проверять - искллючение все равно будет выброшено? 
    /// </summary>
    public static class UserRepository
    {
        public static User GetUserById(AppContext db, int id)
        {
            // нужен ли тут try/catch, или лучше ловить исключения в вызывающем методе?
            return db.Users.FirstOrDefault(user => user.Id == id);
        }
        public static List<User> GetAllUsers(AppContext db)
        {
            return db.Users.ToList();
        }
        public static int AddUser(AppContext db, User user)
        {
            db.Users.Add(user);
            return db.SaveChanges();
        }
        public static int DeleteUser(AppContext db, User user)
        {
            db.Users.Remove(user);
            return db.SaveChanges();
        }
        public static  int DeleteUserById(AppContext db, int id)
        {
            db.Users.Remove(GetUserById(db, id));
            return db.SaveChanges();
        }

        public static int UpdateUserName(AppContext db, int id, string name)
        {
            User user = GetUserById(db, id);
            user.Name = name;
            db.Users.Update(user);
            return db.SaveChanges();
        }

    }
}
