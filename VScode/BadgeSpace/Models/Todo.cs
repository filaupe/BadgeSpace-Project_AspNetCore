using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Title { get; set; }

        [DisplayName("Concluído")]
        public bool Done { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Última atualização")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public string User { get; set; }
    }
}