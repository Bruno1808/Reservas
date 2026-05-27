using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.DTO
{
    public class DashboardDTO
    {
        public int QuantidadeLaboratorios { get; set; }
        public int QuantidadeUsuarios { get; set; }


        public List<TopLaboratorioDto> Top10Laboratorios { get; set; } = new();

        // 3. Lista dos 10 usuários que mais reservaram
        public List<TopUsuarioDto> Top10Usuarios { get; set; } = new();

        // 4. Dados Consolidados para o Gráfico de Reservas
        // (Ex: Evolução por meses ou dias)
        public List<DadosGraficoReservaDto> GraficoReservas { get; set; } = new();


    }
}
