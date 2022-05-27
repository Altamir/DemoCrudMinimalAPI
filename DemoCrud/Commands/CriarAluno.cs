using DemoCrud.Data;
using DemoCrud.Entitys;
using MediatR;

namespace DemoCrud.Commands;

public static class CriarAluno
{
    public record Command(
        Guid id,
        string nome,
        string matricula,
        DateOnly nascimento) : IRequest<Aluno>;

    public class Handler : IRequestHandler<CriarAluno.Command, Aluno>
    {
        public Handler(DemoContext context)
        {
            this.context = context;
        }

        public DemoContext context { get; }

        public async Task<Aluno> Handle(
            CriarAluno.Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                //mapper
                Aluno aluno = new(
                    request.id,
                    request.matricula,
                    request.matricula,
                    request.nascimento);

                context.Add(aluno);
                await context.SaveChangesAsync(cancellationToken);

                return aluno;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
