using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Description { get; set; }

        public List<User> users { get; set; } = new();

    }
}
