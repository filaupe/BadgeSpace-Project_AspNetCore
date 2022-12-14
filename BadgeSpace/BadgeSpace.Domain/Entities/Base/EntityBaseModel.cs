using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.Domain.Entities.Base
{
    public class EntityBaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? ChangeDate { get; set; } = null;
    }
}
