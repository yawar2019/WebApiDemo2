using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo2.Models;

namespace WebApiDemo2.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private  IConfiguration _configuration { get; set; }
        private EmployeeContext _employeeContext { get; set; }
        
        public EmployeeRepository(IConfiguration configuration, EmployeeContext employeeContext)
        {
            _configuration = configuration;
            _employeeContext = employeeContext;
           
           
        }
        public List<employeeDetails> GetAllEmployees()
        {
            return _employeeContext.employeeDetails.ToList();
        }
        public int SaveEmployee( employeeDetails emp)
        {
            _employeeContext.employeeDetails.Add(emp);
           int i= _employeeContext.SaveChanges();
            return i;
        }
         public employeeDetails GetEmployeeById(int? id)
        {
           employeeDetails emp= _employeeContext.employeeDetails.Find(id);
            return emp;
        }

        public employeeDetails UpdateEmployee(employeeDetails emp)
        {
            employeeDetails employeeDetails = _employeeContext.employeeDetails.Find(emp.EmpId);
            employeeDetails.EmpId = emp.EmpId;
            employeeDetails.EmpName = emp.EmpName;
            employeeDetails.EmpSalary = emp.EmpSalary;
            int i = _employeeContext.SaveChanges();

            //_employeeContext.Update(emp);
            return emp;
        }

        public int DeleteEmployee(int? id)
        {
            employeeDetails emp = _employeeContext.employeeDetails.Find(id);
            _employeeContext.employeeDetails.Remove(emp);
            int i = _employeeContext.SaveChanges();

            return i;
        }
    }
}
