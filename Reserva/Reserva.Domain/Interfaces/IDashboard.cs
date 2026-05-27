using Reserva.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Interfaces
{
    public interface IDashboard
    {
        Task<DashboardDTO> ObterDadosDashboardAsync();



    }
}
