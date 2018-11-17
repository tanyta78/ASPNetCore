namespace EventWebApp.Models.Account
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

        [Required(ErrorMessage = "You must enter an first name")]
        [StringLength(20, ErrorMessage = "First name length must between 2 and 20 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter an last name")]
        [StringLength(20, ErrorMessage = "First name length must between 2 and 20 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter an UCN")]
        [StringLength(10, ErrorMessage = "UCN length must 10 characters", MinimumLength = 10)]
        public string UniqueCitizenNumber { get; set; }

        [Required(ErrorMessage = "You must enter an password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password length must between 6 and 50 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password")]
        public string ConfirmPassword { get; set; }
    }
}
