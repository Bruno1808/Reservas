using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()

        {
            try
            {
                var usuarios = await _repository.GetAllAsync();

                if (usuarios == null)
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Em produção, use um Logger para registrar o erro ex.Message
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            string email = string.Empty;
            email = usuario.Email.ToString();

            if (usuario == null) return BadRequest();
            bool emailJaExiste = await _repository.VerificarEmailCadastradoAsync(email);
            if (emailJaExiste)
                return BadRequest("Email ja existe");
            await _repository.AddAsync(usuario);


            return Ok(StatusCode(200, "Usuario inserido com sucesso"));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
           

            try
            {
               if (id == 0) return BadRequest("Id do usuario invalido");

                 var usuario = await _repository.GetByIdAsync(id);
                 return Ok(usuario);
            }
            catch 
            {
                return StatusCode(500, "Usuario nao encontrado");
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] Usuario usuario)
        {
            // 1. Validação de segurança básica
            //if (id != usuario.Id)
            //{
            //    return BadRequest("O ID da URL não corresponde ao ID do objeto enviado.");
            //}
            int id= usuario.Id;
            // 2. Verificação de existência (Opcional, mas recomendado)
            var usuarioExiste = await _repository.GetByIdAsync(id);
            if (usuarioExiste == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            try
            {
                // 3. Chamada ao método que implementamos no repositório
                await _repository.UpdateAsync(usuario);

                // 4. Retorno 204 No Content (sucesso, mas sem corpo de resposta)
                // Ou 200 Ok(usuario) se quiser retornar o objeto atualizado
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return StatusCode(500, "Erro interno ao atualizar o usuário.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado para exclusão.");

            try
            {
                await _repository.DeleteAsync(id);
                return StatusCode(200,"Usuario Excluido com sucesso"); // 204 Sucesso (Padrão para Delete)
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir o usuário.");
            }
        }


    }
}





