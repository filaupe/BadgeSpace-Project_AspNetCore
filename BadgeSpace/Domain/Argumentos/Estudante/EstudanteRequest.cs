using Domain.Argumentos.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Argumentos.Estudante
{
    public class EstudanteRequest : ArgumentosBase
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Codigo { get; set; }

        public string Curso { get; set; }

        public string Tipo { get; set; }

        public string Nivel { get; set; }

        public string Tempo { get; set; }

        public byte[]? Imagem { get; set; }

        public string Habilidades { get; set; }

        [ForeignKey("Empresa")]
        public int EmpresaId { get; private set; }

        public Domain.Entidades.Usuario.Usuario Empresa { get; set; }
    }
}
