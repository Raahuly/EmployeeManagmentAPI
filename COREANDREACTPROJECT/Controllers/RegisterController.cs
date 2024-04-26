using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MODEL;
using Newtonsoft.Json;
using REPOSITORY;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace COREANDREACTPROJECT.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterRepository _registerRepository;
        private readonly IConfiguration _config;

        public RegisterController(RegisterRepository registerRepository, IConfiguration config)
        {
            _registerRepository = registerRepository;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseStatusModel>> Register(RegisterModel RM)
        {
            try
            {
                var register = _registerRepository.Add(RM);
                return Ok(register);
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseStatusModel
                {
                    STATUS = "Error",
                    MSG = "Failed to register user",
                    STATUSCODE = 500
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Object>> Login(RegisterModel RM)
        {
            try
            {
                var GetTokenID = _registerRepository.GetRegister(RM);



                var user = _registerRepository.Login(RM);

                if (user.STATUSCODE == 200)
                {
        
                    var token = GenerateJWTToken(GetTokenID);

                    var output = new
                    {
                        Token = token,
                        Status = 200,
                        MSG = "Login Successfully"
                    };

                    return Ok(output);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        private string GenerateJWTToken(RegisterModel RM)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("Id", RM.ID.ToString()),
        new Claim("Name", RM.USER)
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }






    }
}
