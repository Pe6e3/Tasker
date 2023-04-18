using System.ComponentModel.DataAnnotations;

namespace Tasker.Models;

public class Status
{
    [Key]
    public int StatusId { get; set; }


    [Display(Name = "Статус задачи")]
    public string? StatusName { get; set; }
    public List<Taskk>? Tasks { get; set; }
}
