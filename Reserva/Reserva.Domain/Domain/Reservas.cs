using Reserva.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Reserva.Domain.Domain
{
    public class Reservas
    {

      public Reservas() { }
        public Reservas(int id, DateTime? dataInicio, DateTime? dataFinal, Status status, TipoAlocacao tipoAlocacao, int laboratorioId, int usuarioId)
        {
            Id = id;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            Status = status;
            TipoAlocacao = tipoAlocacao;
            LaboratorioId = laboratorioId;
            UsuarioId = usuarioId;
           
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Data inicial é obrigatória!")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "A Data final é obrigatória!")]
        public DateTime? DataFinal { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "O tipo de locaçao é obrigatório!")]
        public TipoAlocacao TipoAlocacao { get; set; }

        // RELACIONAMENTO COM LABORATÓRIO
        public int LaboratorioId { get; set; } // Chave Estrangeira

        [ForeignKey("LaboratorioId")] // Aponta para a propriedade acima
        public virtual Laboratorios? Laboratorios { get; set; }

        // RELACIONAMENTO COM USUÁRIO
        public int UsuarioId { get; set; } // Chave Estrangeira

        [ForeignKey("UsuarioId")] // Aponta para a propriedade acima
        public virtual Usuario? usuario { get; set; }

    }
}
