using SirmaInternProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirmaInternProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a file name: ");
            string fileName = Console.ReadLine();
            ReadFromFile(fileName);

            var fileLines = ReadFromFile(fileName);
            var employees = GetEmployees(fileLines);
            EmployeesWorkingAtTheSameTime(employees);
            EmployeesWorkingOnTheSameProject(employees);
            EmployeesWorkingAtTheSameTimeOnTheSameProject(employees);
            EmployeesWorkingAtSameMaxTimeOnTheSameProject(employees);
            GetPairOfEmployees(employees);
        }

        public static void EmployeesWorkingAtTheSameTimeOnTheSameProject(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                var EmployeesOnSameProject = employees[i].EmployeesWorkingOnTheSameProject;
                var EmployeesWorkingAtTheSameTime = employees[i].EmployeesWornikngAtSameTheTime;

                for (int j = 0; j < EmployeesWorkingAtTheSameTime.Count; j++)
                {
                    for (int k = 0; k < EmployeesOnSameProject.Count; k++)
                    {
                        if (EmployeesWorkingAtTheSameTime[j].Id == EmployeesOnSameProject[k].Id)
                        {
                            employees[i].AddEmployeesWorkingAtTheSameTimeOnTheSameProject(EmployeesWorkingAtTheSameTime[j]);
                        }
                    }
                }
            }
        }

        public static void EmployeesWorkingAtTheSameTime(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    if (i != j)
                    {
                        if (employees[i].DateFrom >= employees[j].DateFrom && employees[i].DateFrom < employees[j].DateTo
                            || employees[i].DateFrom <= employees[j].DateFrom && employees[i].DateTo > employees[j].DateTo
                            || employees[i].DateTo >= employees[j].DateFrom && employees[i].DateFrom < employees[j].DateFrom)
                        {
                            employees[i].AddEmployeesWornikngAtSameTheTime(employees[j]);
                        }
                    }
                }
            }
        }

        public static void EmployeesWorkingOnTheSameProject(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    if (i != j)
                    {
                        if (employees[i].ProjectId == employees[j].ProjectId)
                        {
                            employees[i].AddEmployeesWorkingOnTheSameProject(employees[j]);
                        }
                    }
                }
            }
        }

        public static List<string> ReadFromFile(string file)
        {
            using (StreamReader streamReader = new StreamReader(file))
            {
                var fileLines = File.ReadAllLines(file).Skip((1)).ToList();
                return fileLines;
            }
        }

        public static List<Employee> GetEmployees(List<string> fileLines)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var line in fileLines)
            {
                string[] temp = line.Split(',');

                temp[3] = CheckDateIsNull(temp[3].Trim().ToString());

                if (temp.Length >= 4)
                {
                    Employee employee = new Employee()
                    {
                        Id = Int32.Parse(temp[0].Trim()),
                        ProjectId = Int32.Parse(temp[1].Trim()),
                        DateFrom = DateTime.ParseExact(temp[2].Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        DateTo = DateTime.ParseExact(temp[3].Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    };
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public static string CheckDateIsNull(string date)
        {
            string nullDate = "NULL";

            if (date == nullDate)
            {
                var DateTo = DateTime.Now;
                date = DateTo.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            return date;
        }

        public static void EmployeesWorkingAtSameMaxTimeOnTheSameProject(List<Employee> employees)
        {
            double maxDifferenceBetweenDates;
            for (int i = 0; i < employees.Count; i++)
            {
                var EmployeesWorkingAThetSameTimeOnSameProject = employees[i].EmployeesWorkingAtTheSameTimeOnTheSameProject;

                if (employees[i].EmployeesWorkingAtTheSameTimeOnTheSameProject.Count != 0)
                {
                    maxDifferenceBetweenDates = (EmployeesWorkingAThetSameTimeOnSameProject[0].DateTo - EmployeesWorkingAThetSameTimeOnSameProject[0].DateFrom).TotalDays - (employees[i].DateTo - employees[i].DateFrom).TotalDays;
                    DoPositive(maxDifferenceBetweenDates);

                    if (EmployeesWorkingAThetSameTimeOnSameProject.Count >= 2)
                    {
                        int index = 0;

                        for (int j = 1; j <= EmployeesWorkingAThetSameTimeOnSameProject.Count - 1; j++)
                        {
                            if (maxDifferenceBetweenDates < DoPositive((EmployeesWorkingAThetSameTimeOnSameProject[j].DateTo -
                                EmployeesWorkingAThetSameTimeOnSameProject[j].DateFrom).TotalDays - (employees[i].DateTo - employees[i].DateFrom).TotalDays))
                            {
                                maxDifferenceBetweenDates = DoPositive((EmployeesWorkingAThetSameTimeOnSameProject[j].DateTo -
                                    EmployeesWorkingAThetSameTimeOnSameProject[j].DateFrom).TotalDays - (employees[i].DateTo - employees[i].DateFrom).TotalDays);
                                index++;
                            }
                        }
                        employees[i].AddEmployeesWorkingAtSameMaxTimeOnTheSameProject(EmployeesWorkingAThetSameTimeOnSameProject[index]);
                    }
                    else
                    {
                        employees[i].AddEmployeesWorkingAtSameMaxTimeOnTheSameProject(EmployeesWorkingAThetSameTimeOnSameProject[0]);
                    }
                }
            }
        }

        public static double DoPositive(double number)
        {
            if (number <= 0)
            {
                number = number * -1;
                return number;
            }
            else
            {
                return number;
            }
        }

        public static void GetPairOfEmployees(List<Employee> employees)
        {
            double maxWorkOnTheSameProject = 0;
            int index = 0;
            var maxPairs = new List<Employee>();

            for (int i = 0; i < employees.Count; i++)
            {
                maxPairs = employees[i].EmployeesWorkingAtSameMaxTimeOnTheSameProject;

                if (maxPairs.Count != 0)
                {
                    if (maxWorkOnTheSameProject < DoPositive((maxPairs[0].DateTo - maxPairs[0].DateFrom).TotalDays - (employees[i].DateTo - employees[i].DateFrom).TotalDays))
                    {
                        maxWorkOnTheSameProject = DoPositive((maxPairs[0].DateTo - maxPairs[0].DateFrom).TotalDays - (employees[i].DateTo - employees[i].DateFrom).TotalDays);
                        index++;
                    }
                }
            }
            Console.WriteLine(employees[index].Id + ", " + employees[index].EmployeesWorkingAtSameMaxTimeOnTheSameProject[0].Id);
        }
    }
}
