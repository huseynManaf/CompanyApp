
namespace CompanyApp.Interfaces
{
    public interface ICompanyManager
    {
        void AddDepartment(string name, int workerLimit, double salaryLimit);
        void DisplayAllDepartments();

        // YENİ: Sistemə işçi əlavə etmək üçün metod
        void AddEmployee(string name, string surname, double salary, string departmentName);
    }
}
void DisplayEmployeesByDepartment(string departmentName);
void UpdateEmployee(string name, string surname, double newSalary, string newDepartment); void UpdateEmployee(string name, string surname, double newSalary, string newDepartment);