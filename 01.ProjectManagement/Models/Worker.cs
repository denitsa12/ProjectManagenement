using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.ProjectManagement.Models
{
    public enum Role
    {
        Leader,
        Developer,
        //QA,
        //DevOps,
        //SysAdmin,
        //BusinessAnalyst
    }
    internal class Worker
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        //public string Position { get; set; }   
        public Role Role { get; set; }
    }
}
