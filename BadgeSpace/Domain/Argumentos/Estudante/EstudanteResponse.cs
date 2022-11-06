using Domain.Argumentos.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Argumentos.Estudante
{
    public class EstudanteResponse : ArgumentosBase
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
        public int EmpresaId { get; set; }

        public Domain.Entidades.Usuario.Usuario Empresa { get; set; }

        public static explicit operator EstudanteResponse(Domain.Entidades.Estudante.Estudante entidade)
        {
            return new EstudanteResponse()
            {
                Nome = entidade.Nome,
                CPF = entidade.CPF,
                Codigo = entidade.Codigo,
                Curso = entidade.Curso,
                Tipo = entidade.Tipo,
                Nivel = entidade.Nivel,
                Tempo = entidade.Tempo,
                Imagem = entidade.Imagem,
                Habilidades = entidade.Habilidades,
                EmpresaId = entidade.EmpresaId,
            };
        }
    }
}
