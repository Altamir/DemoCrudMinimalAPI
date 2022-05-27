using System;
namespace DemoCrud.Entitys
{
    public class Aluno
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string Matricula { get; }
        public DateOnly Nascimento { get; }

        public Aluno(Guid id, string nome, string matricula, DateOnly nascimento)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;
            Nascimento = nascimento;
        }
    }
}

