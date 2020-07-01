using System.ComponentModel.DataAnnotations;

namespace ErrorCenter.WebAPI.Models {
  public class LoginInfo {
    [Required(ErrorMessage = "Email é obrigatório")]
    [MaxLength(100)]
    [EmailAddress(ErrorMessage = "Email válido é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    [MinLength(6, ErrorMessage = "Senha deve ter um mínimo de 6 caracteres")]
    [MaxLength(12, ErrorMessage = "Senha deve ter um máximo de 12 caracteres")]
    public string Password { get; set; }
  }
}