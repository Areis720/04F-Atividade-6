using ExoApi.Domains;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ListarProdutos()
        {
            return StatusCode(200, _projetoRepository.Listar());
        }

        [Authorize]
        [HttpPost]
        public IActionResult CriarProduto(Projeto projeto)
        {
            _projetoRepository.Criar(projeto);

            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, Projeto projeto)
        {
            Projeto projetoBuscado = _projetoRepository.BuscarPorId(id);

            if (projetoBuscado == null)
            {
                return StatusCode(404);
            }

            _projetoRepository.Atualizar(id, projeto);

            return StatusCode(204);

        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult ExcluirProduto(int id)
        {
            Projeto projeto = _projetoRepository.BuscarPorId(id);
            if (projeto == null)
            {
                return StatusCode(404);
            }
            _projetoRepository.Deletar(id);

            return StatusCode(204);
        }
    
        
     }   


}
