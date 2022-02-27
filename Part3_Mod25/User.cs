using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }  // Внешний ключ
        public Company Company { get; set; } //Навигационное свойство
        public UserCredential UserCredential { get; set; }

    }
}
