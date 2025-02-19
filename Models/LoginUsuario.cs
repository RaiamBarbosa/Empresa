﻿using System.ComponentModel.DataAnnotations;

namespace Empresa.Models
{
    public class LoginUsuario
    {
        [Required(ErrorMessage = "Campo {0} deve ser preenchido.")]
        public string Usuario { get; set; } = null!;

       [Required(ErrorMessage = "Campo {0} deve ser preenchido.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "A senha deve conter no minimo e no maximo 6 digitos.")]
        public string Password { get; set; } = null!;

    }
}
