namespace Parcial_MilagrosPeyretti.Models
{
    public class Competidor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdDisciplina { get; set; }
        public Disciplina? Disciplina  { get; set; }
        public int Edad { get; set; }
        public string CuidadRecidencia { get; set; }
    }
}
