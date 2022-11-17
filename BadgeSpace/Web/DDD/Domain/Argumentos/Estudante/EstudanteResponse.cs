using Domain_Driven_Design.Domain.Argumentos.Base;
using Domain_Driven_Design.Domain.Argumentos.Usuario;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Driven_Design.Domain.Argumentos.Estudante
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

        public string Descricao { get; set; }

        public int EmpresaId { get; set; }

        public static explicit operator EstudanteResponse(Domain_Driven_Design.Domain.Entidades.Estudante.Estudante entidade)
        {
            return new EstudanteResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                CPF = entidade.CPF,
                Codigo = entidade.Codigo,
                Curso = entidade.Curso,
                Tipo = entidade.Tipo,
                Nivel = entidade.Nivel,
                Tempo = entidade.Tempo,
                Imagem = entidade.Imagem,
                Descricao = entidade.Descricao,
                EmpresaId = entidade.EmpresaId,
            };
        }
    }
}
