using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentMange.Models
{
    public class Student
    {
        
        public string Name { get; set; }
        public int Id { get; set; }
        
        public string Myclass { get; set; }
       
        public int Mathscore { get; set; }
        public int Englishscore { get; set; }
        public int Chinesescore { get; set; }
        public int Computerscore { get; set; }
        public class StudentDBContent : DbContext
        {
            public DbSet<Student> Students { get; set; }
        }
    }
}