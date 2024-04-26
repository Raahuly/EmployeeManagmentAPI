using Microsoft.AspNetCore.Mvc;
using MODEL;
using REPOSITORY;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COREANDREACTPROJECT.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> AllEmployee()
        {
            try
            {
                var employees = _employeeRepository.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployee(int Id)
        {
            try
            {
                var employees = _employeeRepository.GetById(Id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<ResponseStatusModel>>> Add(EmployeeModel EM)
        {
            try
            {
                var employees = _employeeRepository.Add(EM);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<IEnumerable<ResponseStatusModel>>> Update(EmployeeModel EM)
        {
            try
            {
                var employees = _employeeRepository.Update(EM);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> Delete(int Id)
        {
            try
            {
                var employees = _employeeRepository.Delete(Id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
