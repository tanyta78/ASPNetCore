namespace ChushkaWebApp.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must enter an username")]
        [StringLength(50, ErrorMessage = "Username length must between 6 and 50 characters", MinimumLength = 6)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "You must enter an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter an full name")]
        [StringLength(50, ErrorMessage = "Full name length must between 6 and 50 characters", MinimumLength = 6)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You must enter an password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password length must between 6 and 50 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password")]
        public string ConfirmPassword { get; set; }
    }
}
