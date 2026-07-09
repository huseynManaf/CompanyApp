namespace CompanyApp.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Salary { get; set; }
        public string DepartmentName { get; set; }

        public Employee(string name, string surname, double salary, string departmentName)
        {
            Name = name;
            Surname = surname;
            Salary = salary;
            DepartmentName = departmentName;
        }
    }
}