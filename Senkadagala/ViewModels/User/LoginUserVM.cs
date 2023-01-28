using System.ComponentModel.DataAnnotations;

namespace Senkadagala.ViewModels.User
{
    public class LoginUserVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set; } = false;
    }
}
