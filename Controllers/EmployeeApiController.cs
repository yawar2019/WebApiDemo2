using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo2.Models;
using WebApiDemo2.Repository;

namespace WebApiDemo2.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeApiController : ControllerBase
    {
        private IEmployeeRepository _employeeRep; 
        public EmployeeApiController(IEmployeeRepository employeeRep)
        {
            _employeeRep = employeeRep;
        }

        [HttpGet("api/getAllEmployees")]
        public  IActionResult getEmployeeDetails()
        {
           var result=  _employeeRep.GetAllEmployees();
            return Ok(result);
        }

        [HttpPost("api/SaveEmployee")]
        public IActionResult SaveEmployeeDetails(employeeDetails emp)
        {
            int result = _employeeRep.SaveEmployee(emp);
            return Created("api/getAllEmployees", result);
        }

        [HttpGet("api/getEmployees/{id}")]
        public IActionResult getEmployeeDetailsById(int? id)
        {
            var result = _employeeRep.GetEmployeeById(id);
            return Ok(result);
        }

        [HttpPut("api/UpdateEmployees/{id}")]
        public IActionResult UpdateEmployeeDetailsById(int? id,employeeDetails emp)
        {
            var employee = _employeeRep.GetEmployeeById(id);
            if (employee.EmpId == id) 
            {
                _employeeRep.UpdateEmployee(emp);
            }
            else
            {
                return NoContent();
            }
            return StatusCode(204);
        }

        [HttpDelete("api/deleteEmployee/{id}")]
        public IActionResult deleteEmployeeDetailsById(int? id)
        {
            var result = _employeeRep.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
