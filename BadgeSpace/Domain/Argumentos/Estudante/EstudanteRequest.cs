﻿using Domain.Argumentos.Base;
using Domain.Argumentos.Usuario;
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

        public string Descricao { get; set; }
        
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }

        public Domain.Entidades.Usuario.Usuario? Empresa { get; set; }

        public static explicit operator EstudanteRequest(Domain.Entidades.Estudante.Estudante entidade)
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
