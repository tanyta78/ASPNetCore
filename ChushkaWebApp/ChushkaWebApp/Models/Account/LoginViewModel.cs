namespace ChushkaWebApp.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter an username")]
        [StringLength(50, ErrorMessage = "Username length must between 6 and 50 characters", MinimumLength = 6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter an password")]
        [StringLength(50, ErrorMessage = "Password length must between 6 and 50 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
