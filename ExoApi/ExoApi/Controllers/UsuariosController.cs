using ExoApi.Domains;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuariosController   (UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            return StatusCode(200, _usuarioRepository.Listar());
        }

        /// <summary>
        /// Faz Login
        /// </summary>
        /// {
        ///     "email": "augusto@gmail.com",
        ///     "senha": "1234"
        /// }
        /// <param name="usuario">Email e senha do usuario</param>
        /// <returns>Token de autenticação</returns>
        /// <response code="200">Token gerado</response>
        /// <response code="401">Email e senha incorretos</response>
        [HttpPost]
        public IActionResult FazerLogin(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.BusacarPorEmailESenha(usuario);

            if(usuarioBuscado == null)
            {
                return StatusCode(401);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("FHDFHDSJFHSDJFSFHSHFSDFDSFHSDFHJSDHFJDSHFJHDSFHDF"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "exoapi.webapi",
                audience: "exoApi.webapi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});

             
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            if(_usuarioRepository.BuscaPorId(id) == null)
            {
                return StatusCode(404);
            }

            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            if(_usuarioRepository.BuscaPorId(id) == null)
            {
                return StatusCode(404);
            }

            _usuarioRepository.Deletar(id);

            return StatusCode(204);
        }

    }
}
