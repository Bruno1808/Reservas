using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioRepository _repository;

        public LaboratorioController(ILaboratorioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratorios>>> Get()
        {
            try
            {
                var laboratorios = await _repository.GetAllAsync();

                if (laboratorios == null)
                {
                    return NotFound("Nenhum laboratório encontrado.");
                }

                return Ok(laboratorios);
            }
            catch (Exception ex)
            {
                // Em produção, use um Logger para registrar o erro ex.Message
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Laboratorios>> Post(Laboratorios laboratorio)
        {
            

            if (laboratorio == null) return BadRequest();
           
            
            await _repository.AddAsync(laboratorio);


            return Ok(StatusCode(200, "Laboratório inserido com sucesso"));

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratorios>> Get(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Id do Laboratório invalido");

                var laboratorio = await _repository.GetByIdAsync(id);
                return Ok(laboratorio);
            }
            catch
            {
                return StatusCode(500, "Laboratório nao encontrado");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Laboratorios laboratorio)
        {
            // 1. Validação de segurança básica
            //if (id != usuario.Id)
            //{
            //    return BadRequest("O ID da URL não corresponde ao ID do objeto enviado.");
            //}
            int id = laboratorio.Id;
            // 2. Verificação de existência (Opcional, mas recomendado)
            var laboratorioExiste = await _repository.GetByIdAsync(id);
            if (laboratorioExiste == null)
            {
                return NotFound($"Laboratório com ID {id} não encontrado.");
            }

            try
            {
                // 3. Chamada ao método que implementamos no repositório
                await _repository.UpdateAsync(laboratorio);

                // 4. Retorno 204 No Content (sucesso, mas sem corpo de resposta)
                // Ou 200 Ok(usuario) se quiser retornar o objeto atualizado
                return Ok(laboratorio);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return StatusCode(500, "Erro interno ao atualizar o laboratório.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var laboratorio = await _repository.GetByIdAsync(id);

            if (laboratorio == null)
                return NotFound("Laboratório não encontrado para exclusão.");

            try
            {
                await _repository.DeleteAsync(id);
                return StatusCode(200, "Laboratório Excluido com sucesso"); // 204 Sucesso (Padrão para Delete)
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir o Laboratório.");
            }
        }










    }
}
