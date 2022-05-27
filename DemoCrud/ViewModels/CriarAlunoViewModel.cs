namespace DemoCrud.ViewModels;

public record AlunoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Matricula { get; set; }
    public DateTime Nascimento { get; set; }
}

