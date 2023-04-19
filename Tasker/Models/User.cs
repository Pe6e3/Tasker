using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Display(Name = "Имя пользователя")]
    public string? UserName { get; set; }

    [Display(Name = "Пол")]
    public string? Gender { get; set; }

    [Display(Name = "Путь к аватару")]
    public string? AvatarPath { get; set; }



    [Required]
    [Display(Name = "Логин")]
    public string? Login { get; set; }

    [Required]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Роль")]
    [ForeignKey("RoleId")]
    public int RoleId { get; set; }
    public List<Taskk>? Tasks { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Category? Category { get; set; }
}
