using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EasyTree
    {
        
        public int id { get; set; }
        public int pid { get; set; }
        public string text { get; set; }
       
        public string state { get; set; }

        public bool ischecked{ get; set; }
        public string attributes { get; set; }

        public List<EasyTree> children { get; set; }





        public class TreeContext : DbContext
        {
            public DbSet<EasyTree> EasyTrees { get; set; }
        }
    }
}
