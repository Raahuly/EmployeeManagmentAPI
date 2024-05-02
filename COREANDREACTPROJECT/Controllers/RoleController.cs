using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using REPOSITORY;

namespace COREANDREACTPROJECT.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleClass>>> AllRoles()
        {
            try
            {
                var roles = _roleRepository.GetAll();
                return Ok(roles);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<RoleClass>>> GetRole(int Id)
        {
            try
            {
                var roles = _roleRepository.GetById(Id);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<ResponseStatusModel>>> Add(RoleClass RC)
        {
            try
            {
                var roles = _roleRepository.Add(RC);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<IEnumerable<ResponseStatusModel>>>  Update(RoleClass RC)
        {
            try
            {
                var roles = _roleRepository.Update(RC);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{RoleId}")]
        public async Task<ActionResult<IEnumerable<RoleClass>>> Delete(int RoleId)
        {
            try
            {
                var roles = _roleRepository.Delete(RoleId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
