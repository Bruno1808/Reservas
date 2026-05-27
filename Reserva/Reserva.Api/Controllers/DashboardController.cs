using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reserva.Domain.Domain;
using Reserva.Domain.DTO;
using Reserva.Domain.Interfaces;

namespace Reserva.Api.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _repository;

        public DashboardController(IDashboard repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DashboardDTO>>> Get()
        {
            try
            {
                var Dashboard = await _repository.ObterDadosDashboardAsync();

                if (Dashboard == null)
                {
                    return NotFound("Nenhuma informaçao encontrada.");
                }

                return Ok(Dashboard);
            }
            catch (Exception ex)
            {
                // Em produção, use um Logger para registrar o erro ex.Message
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }

    }
}
