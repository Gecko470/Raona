using Jones_Bank.Models;
using Jones_Bank.Services;
using JonesBank.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Jones_Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repo;
        private readonly IConfiguration _config;

        public LoginController(ILoginRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                userDTO.Pass = HashPassword(userDTO.Pass);

                var user = await _repo.GetUser(userDTO);

                if (user == null)
                {
                    return BadRequest(new { message = $"Datos de acceso incorrectos.." });
                }
                else
                {
                    string jwtToken = GenerarToken(user);
                    return Ok(new { token = jwtToken });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún problema: {ex.Message}");
            }
        }

        [HttpGet("userById")]
        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _repo.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún problema: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    userDTO.Pass = HashPassword(userDTO.Pass);

                    var user = await _repo.InsertUser(userDTO);
                    if (user == null)
                    {
                        return BadRequest("No se pudo insertar el usuario.");
                    }
                    user.Pass = "";

                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún problema: {ex.Message}");
            }
        }

        private string GenerarToken(User user)
        {
            try
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.Now.AddMinutes(60)
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún problema: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Convertir la contraseña a un array de bytes y hashearla
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convertir el array de bytes a una cadena hexadecimal
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún problema: {ex.Message}");
            }
        }
    }
}
