﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.ProjectManagement.Models
{
    class Project
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Worker Leader { get; set; }
        public string Description { get; set; }
        public  List<Worker> Developers { get; set; }

       // public int DurationInDays => (EndDate - StartDate).Days;
       // public decimal MonthlyCost => Developers.Sum(d => d.Salary);
    }
}
