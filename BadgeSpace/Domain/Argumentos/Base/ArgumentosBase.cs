namespace Domain.Argumentos.Base
{
    public class ArgumentosBase
    {
        public int Id { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public bool? Status { get; set; }
    }
}
