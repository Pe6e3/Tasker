using System.ComponentModel.DataAnnotations;

namespace Tasker.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Display(Name = "Категория задачи")]
    public string? CategoryName { get; set; }
    public List<Taskk>?  Tasks { get; set; }
}
