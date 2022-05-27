using System.Collections.ObjectModel;

namespace DemoCrud.Entitys;

public class Turma
{
    public Guid Id { get; }
    public string Name { get; }
    public DateOnly Abertura { get; }
    public DateOnly Encerramento { get; set; }

    public virtual Collection<Aluno> Alunos { get; }

    public Turma(Guid id, string name, DateOnly abertura, DateOnly encerramento, Collection<Aluno> alunos)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Abertura = abertura;
        Encerramento = encerramento;
        Alunos = alunos;
    }
}

