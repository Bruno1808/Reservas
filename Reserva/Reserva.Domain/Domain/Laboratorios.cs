using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reserva.Domain.Domain
{
    public class Laboratorios
    {

        public Laboratorios() { }
        public Laboratorios(int id, string? nome, int capacidade)
        {
            Id = id;
            Nome = nome;
            Capacidade = capacidade;
          
          
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(150, MinimumLength = 1)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo capacidade é obrigatório")]
        [Range(1, 50, ErrorMessage = "A quantidade maxima do Laboratório é 50")]
        public int Capacidade { get; set; }








    }
}
