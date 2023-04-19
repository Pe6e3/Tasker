using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }

        [Display(Name = "Родительская задача")]
        public int ParentMissionId { get; set; }

        [Display(Name = "Название задачи")]
        public string MissionName { get; set; } = string.Empty;

        [Display(Name = "Описание задачи")]
        public string MissionDesc { get; set; } = string.Empty;

        [Display(Name = "Исполнитель")]
        [ForeignKey("UserDoer")]
        public int DoerUserId { get; set; }
        public virtual User UserDoer { get; set; }

        [Display(Name = "Постановщик")]
        [ForeignKey("UserMaster")]
        public int MissionMasterUserId { get; set; }
        public virtual User UserMaster { get; set; }

        public int StatusId { get; set; }
        [Display(Name = "Статус задачи")]
        public virtual Status Status { get; set; }

        [Display(Name = "Создана")]
        public DateTime? DateCreate { get; set; }

        [Display(Name = "Дедлайн")]
        public DateTime? DeadLine { get; set; }

        [Display(Name = "Стоимость задачи в баллах")]
        public int MissionCost { get; set; }
    }
}