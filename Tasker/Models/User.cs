using System.ComponentModel.DataAnnotations;

namespace Tasker.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Display(Name = "Имя пользователя")]
    public string? UserName { get; set; }
    public string Gender { get; set; }

    [Display(Name = "Путь к аватару")]
    public string? AvatarPath { get; set; }



    [Required]
    [Display(Name = "Логин")]
    public string? Login { get; set; }

    [Required]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } 
    public int UserRoleId { get; set; }
    public List<Task>? Tasks { get; set; }

    public User()
    {
        Password = "12345";
        UserRoleId = 2; //2 - User
        AvatarPath = "/image/AvatarM.jpg";
    }
}
