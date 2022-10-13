using BadgeSpace.Models;

namespace BadgeSpace.Utils.MethodsExtensions.UserCase
{
    public static class UserCaseExtension
    {
        public static StudentModel OldToNewRegister(StudentModel oldRegister, StudentModel newRegister)
        {
            oldRegister.Id = newRegister.Id;
            oldRegister.NomeAluno = newRegister.NomeAluno;
            oldRegister.AlunoCPF = newRegister.AlunoCPF;
            oldRegister.Curso = newRegister.Curso;
            oldRegister.Tipo = newRegister.Tipo;
            oldRegister.Nivel = newRegister.Nivel;
            oldRegister.Tempo = newRegister.Tempo;
            oldRegister.Descricao = newRegister.Descricao;
            oldRegister.Imagem = newRegister.Imagem;
            oldRegister.Habilidades = newRegister.Habilidades;
            oldRegister.EmpresaId = newRegister.EmpresaId;

            return oldRegister;
        }
    }
}
