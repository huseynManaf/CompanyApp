namespace CompanyApp.Interfaces
{
    public interface ICompanyManager
    {
        void AddDepartment(string name, int workerLimit, double salaryLimit);
        void DisplayAllDepartments();
        void AddEmployee(string name, string surname, double salary, string departmentName);
        void DisplayEmployeesByDepartment(string departmentName);
        void UpdateEmployee(string name, string surname, double newSalary, string newDepartment);
        void DeleteEmployee(string name, string surname);
        void UpdateDepartment(string oldName, string newName, int newWorkerLimit, double newSalaryLimit);
        void DisplayAllEmployees();
        void DeleteDepartment(string name);
        void ShowCompanyStatistics();
    }
}