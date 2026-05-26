using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reserva.Domain.Domain;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasLabController : ControllerBase
    {
        public readonly IReservaRepository _repository;

        public ReservasLabController(IReservaRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservas>>> Get()
        {
            try
            {
                var reservaslab = await _repository.GetAllAsync();

                if (reservaslab == null)
                {
                    return NotFound("Nenhuma reserva encontrada.");
                }

                return Ok(reservaslab);
            }
            catch (Exception ex)
            {
                // Em produção, use um Logger para registrar o erro ex.Message
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reservas>> Post(Reservas reservaslab)
        {
            if (reservaslab == null) return BadRequest();

            if (reservaslab.DataInicio == null || reservaslab.DataFinal == null) return BadRequest("Verifique as datas por favor");

            else
            {
                bool validar = await _repository.ValidarHorarioDisponivelLaboratorio(reservaslab.DataInicio, reservaslab.DataFinal, reservaslab.LaboratorioId);
                if (validar == true) return StatusCode(500, "Reserva ja existe reservas nesta data ou horario");
                else
                {
                    await _repository.AddAsync(reservaslab);

                    return Ok(StatusCode(200, "Reserva inserida com sucesso"));
                }
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {


            try
            {
                if (id == 0) return BadRequest("Id da reserva invalido");

                var usuario = await _repository.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch
            {
                return StatusCode(500, "Reserva nao encontrada");
            }

        }

        [HttpGet("Laboratorio/{id}")]
        public async Task<ActionResult<Usuario>> GetBylaboratorioid(int id)
        {


            try
            {
                if (id == 0) return BadRequest("Id do laboratório invalido");

                var reserva = await _repository.GetBylaboratorioidAsync(id);
                return Ok(reserva);
            }
            catch
            {
                return StatusCode(500, "Nenhuma reserva para este laboratório foi encontrada");
            }

        }

        [HttpGet("Usuario/{id}")]
        public async Task<ActionResult<Usuario>> GetByusuarioid(int id)
        {


            try
            {
                if (id == 0) return BadRequest("Id do usuario invalido");

                var reserva = await _repository.GetByusuarioidAsync(id);
                return Ok(reserva);
            }
            catch
            {
                return StatusCode(500, "Nenhuma reserva para este usuario foi encontrada");
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Reservas reserva)
        {
            // 1. Validação de segurança básica
            //if (id != usuario.Id)
            //{
            //    return BadRequest("O ID da URL não corresponde ao ID do objeto enviado.");
            //}
            int id = reserva.Id;
            // 2. Verificação de existência (Opcional, mas recomendado)
            var reservaExiste = await _repository.GetByIdAsync(id);
            if (reservaExiste == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
           

            try
            {
                if (reserva.Status == Domain.Enum.Status.CANCELADO)
                    await _repository.UpdateAsync(reserva);
                
                else
                {
                    if (reserva.DataInicio == null || reserva.DataFinal == null) return BadRequest("Verifique as datas por favor");

                    else
                    {
                        bool validar = await _repository.ValidarHorarioDisponivelLaboratorio(reserva.DataInicio, reserva.DataFinal, reserva.LaboratorioId);
                        if (validar == true) return Ok("Já existe Reservas nesta data para este laboraório");
                        else
                            await _repository.UpdateAsync(reserva);

                        return Ok(StatusCode(200, "Reserva alterada com sucesso"));

                    }
                }


                return Ok(reserva);

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
            var reserva = await _repository.GetByIdAsync(id);

            if (reserva == null)
                return NotFound("Usuário não encontrado para exclusão.");

            try
            {
                await _repository.DeleteAsync(id);
                return StatusCode(200, "reserva excluida com sucesso"); // 204 Sucesso (Padrão para Delete)
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir a reserva.");
            }
        }


    }
}
