using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo2.Models;

namespace WebApiDemo2.Repository
{
    public interface IEmployeeRepository
    {
        public List<employeeDetails> GetAllEmployees();
        public int SaveEmployee(employeeDetails emp);
        public int DeleteEmployee(int? id);
        public employeeDetails GetEmployeeById(int? id);
        public employeeDetails UpdateEmployee(employeeDetails emp);
    }
}
