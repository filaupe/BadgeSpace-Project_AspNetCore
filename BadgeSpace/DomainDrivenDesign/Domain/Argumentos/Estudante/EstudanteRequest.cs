using Domain_Driven_Design.Domain.Argumentos.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Driven_Design.Domain.Argumentos.Estudante
{
    public class EstudanteRequest : ArgumentosBase
    {
        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        [Remote(action:"VerificarCodigo", controller:"Validation")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Curso { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Nivel { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Tempo { get; set; }

        public byte[]? Imagem { get; set; }

        [Required(ErrorMessage = "Essa área é obrigatória")]
        public string Descricao { get; set; }
        
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }

        public Domain_Driven_Design.Domain.Entidades.Usuario.Usuario? Empresa { get; set; }

        public static explicit operator EstudanteRequest(Domain_Driven_Design.Domain.Entidades.Estudante.Estudante entidade)
        {
            return new EstudanteRequest()
            {
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
                Empresa = entidade.Empresa,
            };
        }
    }
}
