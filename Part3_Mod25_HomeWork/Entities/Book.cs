using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod25_HomeWork
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        [Required, Range(1800, 2200)]
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public User CurrentUser { get; set; }   //для решения задачи, поставленной в модуле, этого хватит

        public override string ToString()
        {
            string authors = "";
            if (Authors != null)
            {
                foreach (Author author in Authors)
                {
                    authors += author.Name + " ";
                }
            }
            
            //return $"name: {Name}; author(s): {authors}; year: {Year}; genre: {Genre.Name}; current user: {(CurrentUser != null ? (CurrentUser.Name ?? "No Name") : "available now")}";
            return $"name: {Name}; author(s): {authors}; year: {Year}; genre: {(Genre != null ? Genre.Name ?? "n/a" : "n/a")}; current user: {(CurrentUser != null ? (CurrentUser.Name ?? "No Name") : "available now")}";

        }
    }
}
