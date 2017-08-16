using System.ComponentModel.DataAnnotations;

namespace TestWebApp.ViewModels
{
    public class UserManageModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }
    }
}