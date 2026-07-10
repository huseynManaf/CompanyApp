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
    }
    public void UpdateEmployee(string name, string surname, double newSalary, string newDepartment)
{
    Employee targetEmp = null;

    // Bütün şöbələri gəzib həmin işçini tapırıq
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

    // Maaşı yeniləyirik
    targetEmp.Salary = newSalary;

    // Əgər şöbəsi dəyişirsə, onu köhnə şöbədən silib yenisinə keçiririk
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
            // Köhnə şöbədən sil
            foreach (var dept in _departments)
            {
                if (dept.Name.ToLower() == targetEmp.DepartmentName.ToLower())
                {
                    dept.Employees.Remove(targetEmp);
                    break;
                }
            }

            // Yeni şöbəyə əlavə et
            targetEmp.DepartmentName = newDept.Name;
            newDept.Employees.Add(targetEmp);
        }
    }

    Console.WriteLine($"{name} {surname} adlı işçinin məlumatları uğurla yeniləndi.");
}
public void DeleteEmployee(string name, string surname)
{
    bool isDeleted = false;

    // Şöbələri gəzib həmin işçini tapırıq və siyahıdan silirik
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
            Console.WriteLine($"{name} {surname} sistemdən və {dept.Name} şöbəsindən uğurla silindi.");
            break;
        }
    }

    if (!isDeleted)
    {
        Console.WriteLine($"Xəta: {name} {surname} adlı işçi tapılmadı!");
    }
}