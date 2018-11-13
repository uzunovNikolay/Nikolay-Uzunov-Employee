using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirmaInternProject.Entities
{
    public class Employee
    {
        public Employee()
        {
            EmployeesWornikngAtSameTheTime = new List<Employee>();
            EmployeesWorkingOnTheSameProject = new List<Employee>();
            EmployeesWorkingAtTheSameTimeOnTheSameProject = new List<Employee>();
            EmployeesWorkingAtSameMaxTimeOnTheSameProject = new List<Employee>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Employee> EmployeesWornikngAtSameTheTime { get; set; }
        public List<Employee> EmployeesWorkingOnTheSameProject { get; set; }
        public List<Employee> EmployeesWorkingAtTheSameTimeOnTheSameProject { get; set; }
        public List<Employee> EmployeesWorkingAtSameMaxTimeOnTheSameProject { get; set; }

        public void AddEmployeesWorkingAtSameMaxTimeOnTheSameProject(Employee employee)
        {
            EmployeesWorkingAtSameMaxTimeOnTheSameProject.Add(employee);
        }

        public void AddEmployeesWornikngAtSameTheTime(Employee employee)
        {
            EmployeesWornikngAtSameTheTime.Add(employee);
        }

        public void AddEmployeesWorkingOnTheSameProject(Employee employee)
        {
            EmployeesWorkingOnTheSameProject.Add(employee);
        }

        public void AddEmployeesWorkingAtTheSameTimeOnTheSameProject(Employee employee)
        {
            EmployeesWorkingAtTheSameTimeOnTheSameProject.Add(employee);
        }
    }
}
