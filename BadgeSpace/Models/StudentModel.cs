using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BadgeSpace.Models
{
    public class StudentModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nome do Aluno")]
        public string? NomeAluno { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}")]
        public string AlunoCPF { get; set; }

        [DisplayName("Curso")]
        public string? Curso { get; set; }

        [DisplayName("Tipo")]
        public string? Tipo { get; set; }

        [DisplayName("Nivel")]
        public string? Nivel { get; set; }

        [DisplayName("Tempo de Curso")]
        public string? Tempo { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [DisplayName("Badge")]
        public byte[]? Imagem { get; set; }

        [DisplayName("Habilidades Adquiridas")]
        public string? Habilidades { get; set; }

        public string EmpresaId { get; set; }
    }
}