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
            _departments.Add(new Department(name, workerLimit, salaryLimit));
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
                Console.WriteLine($"Şöbə: {dept.Name} | Limit: {dept.WorkerLimit} | Mövcud İşçi: {dept.Employees.Count}");
            }
        }

        // YENİ: İşçi əlavə etmə funksiyasının dərslik məntiqi ilə yazılması
        public void AddEmployee(string name, string surname, double salary, string departmentName)
        {
            Department targetDept = null;

            // 1. Şöbənin mövcud olub-olmamasını dövrlə yoxlayırıq
            foreach (var dept in _departments)
            {
                if (dept.Name.ToLower() == departmentName.ToLower())
                {
                    targetDept = dept;
                    break;
                }
            }

            if (targetDept == null)
            {
                Console.WriteLine($"Xəta: '{departmentName}' adlı şöbə tapılmadı!");
                return;
            }

            // 2. Şöbənin işçi limitini yoxlayırıq
            if (targetDept.Employees.Count >= targetDept.WorkerLimit)
            {
                Console.WriteLine($"Xəta: {targetDept.Name} şöbəsində işçi limiti dolub!");
                return;
            }

            // 3. Hər şey qaydasındadırsa, işçini yaradıb şöbəyə daxil edirik
            Employee newEmp = new Employee(name, surname, salary, targetDept.Name);
            targetDept.Employees.Add(newEmp);
            Console.WriteLine($"{name} {surname} uğurla {targetDept.Name} şöbəsinə əlavə olundu.");
        }
    }
}