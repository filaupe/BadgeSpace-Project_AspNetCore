using System.ComponentModel;

namespace BadgeSpace.Models
{
    public class Student
    {
        public int Id { get; set; }

        [DisplayName("Nome do Aluno")]
        public string NomeAluno { get; set; }

        public string AlunoCPF { get; set; }

        [DisplayName("Curso")]
        public string Curso { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Nivel")]
        public string Nivel { get; set; }

        [DisplayName("Tempo de Curso")]
        public string Tempo { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Badge")]
        public byte[] Imagem { get; set; }

        [DisplayName("Habilidades Adquiridas")]
        public string Habilidades { get; set; }
    }
}