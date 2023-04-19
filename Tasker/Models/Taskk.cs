using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class Taskk
    {
        [Key]
        public int TaskId { get; set; }

        [Display(Name = "Родительская задача")]
        public int ParentTaskId { get; set; }

        [Display(Name = "Название задачи")]
        public string TaskName { get; set; } = string.Empty;

        [Display(Name = "Описание задачи")]
        public string TaskDesc { get; set; } = string.Empty;

        [Display(Name = "Исполнитель")]
        [ForeignKey("UserDoer")]
        public int DoerUserId { get; set; }
        public virtual User UserDoer { get; set; }

        [Display(Name = "Постановщик")]
        [ForeignKey("UserMaster")]
        public int TaskMasterUserId { get; set; }
        public virtual User UserMaster { get; set; }

        [Display(Name = "Статус задачи")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Display(Name = "Дата создания задачи")]
        public DateTime? DateCreate { get; set; }

        [Display(Name = "Дедлайн")]
        public DateTime? DeadLine { get; set; }

        [Display(Name = "Стоимость задачи в баллах")]
        public int TaskCost { get; set; }
    }
}