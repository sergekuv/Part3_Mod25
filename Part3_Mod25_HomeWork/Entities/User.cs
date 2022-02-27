using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork
{
    public class User
    {
       // [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        //public User(string name, string eMail)
        //{
        //    Name = name;
        //    Email = Email;
        //}
        public override string ToString() => Name + " " + Email;
    }
}
