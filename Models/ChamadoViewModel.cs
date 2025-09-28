using System;

namespace CadAlunoMVC.Models
{
    public class ChamadoViewModel
    {
        public int Id { get; set; }
        public DateTime DataAbertura { get; set; }
        public string DescricaoProblema { get; set; }
        public string DescricaoAtendimento { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public int Situacao { get; set; }
        public int? UsuarioId { get; set; }

    }
}
