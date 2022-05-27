using DemoCrud.Data;
using DemoCrud.Entitys;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoCrud.Querys;

public static class ObeterAluno
{
    public record Query(Guid Id) : IRequest<Aluno>;

    public class Handler : IRequestHandler<Query, Aluno?>
    {
        public DemoContext Context { get; }

        public Handler(DemoContext context)
        {
            Context = context;
        }

        public async Task<Aluno?> Handle(Query request, CancellationToken cancellationToken)
        {
            var maybe = await Context.Alunos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return maybe;
        }
    }

}

