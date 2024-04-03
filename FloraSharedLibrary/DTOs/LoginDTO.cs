using System.ComponentModel.DataAnnotations;
namespace FloraSharedLibrary.DTOs
{
    public class LoginDTO
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Password { get; set; }
    }
}
