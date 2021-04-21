using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentMange.Models
{
    public class User
    {   [Key]
        public string Myname { get; set; }
        public int Passsword { get; set; }
        public class UserDBContent : DbContext
        {
            public DbSet<User> users { get; set; }
        }
    }
}