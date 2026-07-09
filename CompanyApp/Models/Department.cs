using System;
using System.Collections.Generic;

namespace CompanyApp.Models
{
    public class Department
    {
        public string Name { get; set; }
        public int WorkerLimit { get; set; }
        public double SalaryLimit { get; set; }

        // Hər şöbənin özünə aid işçilərinin siyahısı (Array/Collection məntiqi)
        public List<Employee> Employees { get; set; }

        public Department(string name, int workerLimit, double salaryLimit)
        {
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
            Employees = new List<Employee>(); // Siyahını işə salırıq
        }
    }
}