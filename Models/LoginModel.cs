using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
