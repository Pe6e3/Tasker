using System.ComponentModel.DataAnnotations;

namespace Tasker.Models;

public class Roles
{
    [Key]
    public int RoleId { get; set; }

    [Display(Name = "Роль пользователя")]
    public string? RoleName { get; set; }
    public List<User>? Users { get; set; }
}
