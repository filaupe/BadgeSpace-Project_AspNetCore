using BadgeSpace.Domain.Entities.User.Empress.Course;
using BadgeSpace.Web.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BadgeSpace.Web.Models.Student
{
    public class StudentViewModel : BaseViewModel
    {
        [DisplayName("Nome do Estudante")]
        [Required(ErrorMessage = "A área Nome é obrigatória")]
        public string Name { get; set; } = String.Empty;

        [Remote(action: "VerifyEmailAdress", controller: "ValidationMethods")]
        [Required(ErrorMessage = "A área Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Adicione um Email válido")]
        [DisplayName("Email")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Escolha um curso")]
        public CourseModel Course { get; set; } = null!;

        public List<CourseModel>? Courses { get; set; } = null;
    }
}
