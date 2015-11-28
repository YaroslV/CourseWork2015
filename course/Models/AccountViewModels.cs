using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace course.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть пошту")]
        [Display(Name = "Пошта")]
        [EmailAddress(ErrorMessage = "Якась у Вас неправильна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password, ErrorMessage ="Поганий пароль. Хороший пароль містить мінімум 1 велику літеру, 1 маленьку літеру, 1 символ")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати Вас?")]
        public bool RememberMe { get; set; }
    }

    public enum Role
    {
        [Display(Name = "Студент")]
        Student,
        [Display(Name = "Викладач")]
        Tutor
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть пошту")]
        [EmailAddress(ErrorMessage = "Якась у Вас неправильна пошта")]
        [Display(Name = "Пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Введіть пароль")]
        [StringLength(100, ErrorMessage = "{0} повинен бути не коротший {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Поганий пароль. Хороший пароль містить мінімум 1 велику літеру, 1 маленьку літеру, 1 символ")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Студент чи викладач?")]
        [Required(ErrorMessage = "Ви студент чи викладач")]
        public Role AppRole { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Введіть пошту")]
        [EmailAddress(ErrorMessage = "Якась у Вас неправильна пошта")]
        [Display(Name = "Пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [StringLength(100, ErrorMessage = "{0} повинен бути не коротший {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введіть пошту")]
        [EmailAddress]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }
}
