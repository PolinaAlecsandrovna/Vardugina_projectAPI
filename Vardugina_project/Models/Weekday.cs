using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vardugina_project.Models
{
    [Table("weekday")]
    public class Weekday
    {
        [Key]
        [Column("weekday_id")]
        public int WeekdayId { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
