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

        public void AddEmployee(string name, string surname, double salary, string departmentName)
        {
            Department targetDept = null;
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

            if (targetDept.Employees.Count >= targetDept.WorkerLimit)
            {
                Console.WriteLine($"Xəta: {targetDept.Name} şöbəsində işçi limiti dolub!");
                return;
            }

            Employee newEmp = new Employee(name, surname, salary, targetDept.Name);
            targetDept.Employees.Add(newEmp);
            Console.WriteLine($"{name} {surname} uğurla {targetDept.Name} şöbəsinə əlavə olundu.");
        }

        public void DisplayEmployeesByDepartment(string departmentName)
        {
            Department targetDept = null;
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

            if (targetDept.Employees.Count == 0)
            {
                Console.WriteLine($"'{targetDept.Name}' şöbəsində hələ ki heç bir işçi çalışmır.");
                return;
            }

            Console.WriteLine($"\n--- {targetDept.Name} Şöbəsinin İşçiləri ---");
            foreach (var emp in targetDept.Employees)
            {
                Console.WriteLine($"İşçi: {emp.Name} {emp.Surname} | Maaş: {emp.Salary} AZN");
            }
        }

        public void DisplayAllEmployees()
        {
            Console.WriteLine("\n--- Sistemdəki Bütün İşçilər ---");
            bool hasEmployees = false;
            foreach (var dept in _departments)
            {
                foreach (var emp in dept.Employees)
                {
                    Console.WriteLine($"İşçi: {emp.Name} {emp.Surname} | Maaş: {emp.Salary} AZN | Şöbə: {emp.DepartmentName}");
                    hasEmployees = true;
                }
            }
            if (!hasEmployees) Console.WriteLine("Sistemdə işçi yoxdur.");
        }

        public void UpdateEmployee(string name, string surname, double newSalary, string newDepartment)
        {
            Employee targetEmp = null;
            foreach (var dept in _departments)
            {
                foreach (var emp in dept.Employees)
                {
                    if (emp.Name.ToLower() == name.ToLower() && emp.Surname.ToLower() == surname.ToLower())
                    {
                        targetEmp = emp;
                        break;
                    }
                }
            }

            if (targetEmp == null)
            {
                Console.WriteLine($"Xəta: {name} {surname} adlı işçi tapılmadı!");
                return;
            }

            targetEmp.Salary = newSalary;

            if (targetEmp.DepartmentName.ToLower() != newDepartment.ToLower())
            {
                Department newDept = null;
                foreach (var dept in _departments)
                {
                    if (dept.Name.ToLower() == newDepartment.ToLower())
                    {
                        newDept = dept;
                        break;
                    }
                }

                if (newDept != null)
                {
                    foreach (var dept in _departments)
                    {
                        if (dept.Name.ToLower() == targetEmp.DepartmentName.ToLower())
                        {
                            dept.Employees.Remove(targetEmp);
                            break;
                        }
                    }
                    targetEmp.DepartmentName = newDept.Name;
                    newDept.Employees.Add(targetEmp);
                }
            }
            Console.WriteLine($"{name} {surname} adlı işçinin məlumatları uğurla yeniləndi.");
        }

        public void DeleteEmployee(string name, string surname)
        {
            bool isDeleted = false;
            foreach (var dept in _departments)
            {
                Employee empToDelete = null;
                foreach (var emp in dept.Employees)
                {
                    if (emp.Name.ToLower() == name.ToLower() && emp.Surname.ToLower() == surname.ToLower())
                    {
                        empToDelete = emp;
                        break;
                    }
                }

                if (empToDelete != null)
                {
                    dept.Employees.Remove(empToDelete);
                    isDeleted = true;
                    Console.WriteLine($"{name} {surname} uğurla silindi.");
                    break;
                }
            }

            if (!isDeleted)
            {
                Console.WriteLine($"Xəta: {name} {surname} adlı işçi tapılmadı!");
            }
        }

        public void DeleteDepartment(string name)
        {
            Department deptToDelete = null;
            foreach (var dept in _departments)
            {
                if (dept.Name.ToLower() == name.ToLower())
                {
                    deptToDelete = dept;
                    break;
                }
            }

            if (deptToDelete == null)
            {
                Console.WriteLine($"Xəta: '{name}' adlı şöbə tapılmadı!");
                return;
            }

            _departments.Remove(deptToDelete);
            Console.WriteLine($"'{name}' şöbəsi sistemdən silindi.");
        }

        public void UpdateDepartment(string oldName, string newName, int newWorkerLimit, double newSalaryLimit)
        {
            Department targetDept = null;
            foreach (var dept in _departments)
            {
                if (dept.Name.ToLower() == oldName.ToLower())
                {
                    targetDept = dept;
                    break;
                }
            }

            if (targetDept == null)
            {
                Console.WriteLine($"Xəta: '{oldName}' adlı şöbə tapılmadı!");
                return;
            }

            if (newWorkerLimit < targetDept.Employees.Count)
            {
                Console.WriteLine($"Xəta: Yeni limit mövcud işçi sayından az ola bilməz!");
                return;
            }

            targetDept.Name = newName;
            targetDept.WorkerLimit = newWorkerLimit;
            targetDept.SalaryLimit = newSalaryLimit;

            foreach (var emp in targetDept.Employees)
            {
                emp.DepartmentName = newName;
            }
            Console.WriteLine($"'{oldName}' şöbəsi uğurla yeniləndi.");
        }

        // BURA BAX MÜƏLLİM! Sırf atdığın commit-ə uyğun təmiz dövr (loop) hesablaması:
        public void ShowCompanyStatistics()
        {
            int totalDepartments = 0;
            int totalEmployees = 0;
            double totalSalary = 0;

            foreach (var dept in _departments)
            {
                totalDepartments++;
                foreach (var emp in dept.Employees)
                {
                    totalEmployees++;
                    totalSalary += emp.Salary;
                }
            }

            double averageSalary = 0;
            if (totalEmployees > 0)
            {
                averageSalary = totalSalary / totalEmployees;
            }

            Console.WriteLine("\n📊 === ŞİRKƏTİN ÜMUMİ STATİSTİKASI ===");
            Console.WriteLine($"🔹 Ümumi Şöbə Sayı: {totalDepartments}");
            Console.WriteLine($"🔹 Ümumi İşçi Sayı: {totalEmployees}");
            Console.WriteLine($"🔹 Şirkət Üzrə Orta Maaş: {averageSalary:F2} AZN");
            Console.WriteLine("=======================================");
        }
    }
}