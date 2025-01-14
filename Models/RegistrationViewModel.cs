using Run.exe.Entities;
using System.ComponentModel.DataAnnotations;

namespace Run.exe.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(11, ErrorMessage = "Max 11 characters allowed.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "T-Shirt Size is required.")]
        public TShirtSize TShirtSize { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 characters allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [MaxLength(200, ErrorMessage = "Max 200 characters allowed.")]
        public string MedicalConditions { get; set; }

        [Required(ErrorMessage = "You must acknowledge the Waiver and Liability.")]
        public bool WaiverAndLiabilityAcknowledged { get; set; }

        [Required(ErrorMessage = "You must acknowledge the Photo/Video Release.")]
        public bool PhotoVideoRelease { get; set; }

        [Required(ErrorMessage = "You must agree to the Terms and Conditions.")]
        public bool TermsAndConditions { get; set; }

        [Required(ErrorMessage = "Race Category is required.")]
        public RaceCategory RaceCategory { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum TShirtSize
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL,
        XXXL
    }

    public enum RaceCategory
    {
        Sprint,
        Marathon,
        UltraMarathon,
    }
}
