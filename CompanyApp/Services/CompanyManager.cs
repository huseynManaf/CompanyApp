using System;
using System.Collections.Generic;
using CompanyApp.Interfaces;
using CompanyApp.Models;

namespace CompanyApp.Services
{
    public class CompanyManager : ICompanyManager
    {
        private readonly List<Department> _departments = new List<Department>();

        public void AddDepartment(string name, int workerLimit, double salaryLimit)
        {
            foreach (var dept in _departments)
            {
                if (dept.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Xəta: Bu şöbə artıq mövcuddur!");
                    return;
                }
            }

            Department newDept = new Department(name, workerLimit, salaryLimit);
            _departments.Add(newDept);
            Console.WriteLine($"{name} şöbəsi uğurla yaradıldı.");
        }

        public void DisplayAllDepartments()
        {
            if (_departments.Count == 0)
            {
                Console.WriteLine("Sistemdə heç bir şöbə yoxdur.");
                return;
            }

            Console.WriteLine("\n--- Şöbələr ---");
            foreach (var dept in _departments)
            {
                Console.WriteLine($"Şöbə: {dept.Name} | Limit: {dept.WorkerLimit}");
            }
        }
    }
}