using System;
using System.Linq;
using System.Collections.Generic;

namespace Part3_Mod25
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppContext db = new())
            {
                Company company1 = new Company { Name = "SF" };
                Company company2 = new Company { Name = "VK", Users = new() };

                Company company3 = new Company { Name = "FB" };
                db.Companies.Add(company3);
                db.SaveChanges();

                User user1 = new User { Name = "A", Role = "Admin" };
                var user2 = new User { Name = "Bob", Role = "Admin" };
                var user3 = new User { Name = "Clark", Role = "User" };

                user1.Company = company1;
                company2.Users.Add(user2);
                user3.CompanyId = company3.Id;

                db.Companies.AddRange(company1, company2);
                db.Users.AddRange(user1, user2, user3);
                db.SaveChanges();
            }
            

            Console.WriteLine("-- end --");
        }
    }
}
