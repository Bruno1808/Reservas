using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reserva.Domain.Domain
{
 public class Usuario
    {

      public Usuario() { }
        public Usuario(int id, string? nome, string? email, bool ativo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Ativo = ativo;
          
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(150, MinimumLength = 5)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório!")]
        [EmailAddress]
        public string? Email { get; set; }


        public bool Ativo { get; set; } = true;

      

    }
}
