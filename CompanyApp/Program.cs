using System;
using CompanyApp.Services;

namespace CompanyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Şirkət menecerimizi işə salırıq
            CompanyManager manager = new CompanyManager();
            bool isRunning = true;

            Console.WriteLine("=== Şirkət İdarəetmə Sisteminə Xoş Gəlmisiniz ===");

            // İstifadəçi çıxış edənə qədər proqramı sonsuz dövrdə saxlayırıq
            while (isRunning)
            {
                Console.WriteLine("\n--- MENYU ---");
                Console.WriteLine("1. Yeni Şöbə Yarat");
                Console.WriteLine("2. Şöbələrin Siyahısını Göstər");
                Console.WriteLine("3. Şöbəyə İşçi Əlavə Et");
                Console.WriteLine("4. Şöbədəki İşçiləri Göstər");
                Console.WriteLine("5. Bütün İşçilərin Siyahısını Göstər");
                Console.WriteLine("6. İşçinin Məlumatlarını Yenilə (Update)");
                Console.WriteLine("7. İşçini Sistemdən Sil (Delete)");
                Console.WriteLine("8. Şöbəni Sistemdən Sil");
                Console.WriteLine("9. Şöbə Məlumatlarını Yenilə");
                Console.WriteLine("0. Proqramdan Çıx");
                Console.Write("\nSeçiminiz edin: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Şöbənin adı: ");
                        string deptName = Console.ReadLine();
                        Console.Write("İşçi limiti: ");
                        int workerLimit = int.Parse(Console.ReadLine());
                        Console.Write("Maaş limiti: ");
                        double salaryLimit = double.Parse(Console.ReadLine());

                        manager.AddDepartment(deptName, workerLimit, salaryLimit);
                        break;

                    case "2":
                        manager.DisplayAllDepartments();
                        break;

                    case "3":
                        Console.Write("İşçinin adı: ");
                        string empName = Console.ReadLine();
                        Console.Write("İşçinin soyadı: ");
                        string empSurname = Console.ReadLine();
                        Console.Write("Maaşı: ");
                        double salary = double.Parse(Console.ReadLine());
                        Console.Write("Əlavə ediləcək şöbənin adı: ");
                        string targetDept = Console.ReadLine();

                        manager.AddEmployee(empName, empSurname, salary, targetDept);
                        break;

                    case "4":
                        Console.Write("Baxmaq istədiyiniz şöbənin adı: ");
                        string dName = Console.ReadLine();
                        manager.DisplayEmployeesByDepartment(dName);
                        break;

                    case "5":
                        manager.DisplayAllEmployees();
                        break;

                    case "6":
                        Console.Write("Məlumatı dəyişəcək işçinin adı: ");
                        string uName = Console.ReadLine();
                        Console.Write("Soyadı: ");
                        string uSurname = Console.ReadLine();
                        Console.Write("Yeni maaşı: ");
                        double newSalary = double.Parse(Console.ReadLine());
                        Console.Write("Yeni şöbəsinin adı: ");
                        string newDept = Console.ReadLine();

                        manager.UpdateEmployee(uName, uSurname, newSalary, newDept);
                        break;

                    case "7":
                        Console.Write("Silinəcək işçinin adı: ");
                        string delName = Console.ReadLine();
                        Console.Write("Soyadı: ");
                        string delSurname = Console.ReadLine();

                        manager.DeleteEmployee(delName, delSurname);
                        break;

                    case "8":
                        Console.Write("Silinəcək şöbənin adı: ");
                        string delDept = Console.ReadLine();
                        manager.DeleteDepartment(delDept);
                        break;

                    case "9":
                        Console.Write("Dəyişdiriləcək şöbənin köhnə adı: ");
                        string oldDName = Console.ReadLine();
                        Console.Write("Yeni adı: ");
                        string newDName = Console.ReadLine();
                        Console.Write("Yeni işçi limiti: ");
                        int newWLimit = int.Parse(Console.ReadLine());
                        Console.Write("Yeni maaş limiti: ");
                        double newSLimit = double.Parse(Console.ReadLine());

                        manager.UpdateDepartment(oldDName, newDName, newWLimit, newSLimit);
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Proqramdan çıxılır... Uğurlar!");
                        break;

                    default:
                        Console.WriteLine("Xəta: Yanlış seçim etdiniz, yenidən yoxlayın!");
                        break;
                }
            }
        }
    }
}