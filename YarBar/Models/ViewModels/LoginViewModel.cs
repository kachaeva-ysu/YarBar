using System.ComponentModel.DataAnnotations;

namespace YarBar.Models.ViewModels
{
    public class LoginViewModel
    {
       [Required(ErrorMessage = "Введите email")]
       [Display(Name = "Email")]
       public string Email { get; set; }

       [Required(ErrorMessage = "Введите email")]
       [DataType(DataType.Password)]
       [Display(Name = "Пароль")]
       public string Password { get; set; }

       [Display(Name = "Запомнить?")]
       public bool RememberMe { get; set; }
    }
}
