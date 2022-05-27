using DemoCrud.Commands;
using DemoCrud.Data;
using DemoCrud.Entitys;
using DemoCrud.Querys;
using DemoCrud.ViewModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

TypeAdapterConfig<DateTime, DateOnly>.NewConfig()
    .MapWith(dest => DateOnly.FromDateTime(dest));

TypeAdapterConfig<DateOnly, DateTime>.NewConfig()
    .MapWith(dest => dest.ToDateTime(TimeOnly.MinValue));

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services
    .AddEndpointsApiExplorer()
    .AddMediatR(typeof(IMediatorMark))
    .AddSwaggerGen()
    .AddEntityFrameworkNpgsql()
    .AddDbContext<DemoContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
    });

var app = builder.Build();

await InitDB(app.Services, app.Logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/alunos", async (AlunoViewModel aluno, IMediator mediator, CancellationToken cancellationToken) =>
{
    if (aluno is null) return Results.BadRequest("Request inválido");
    CriarAluno.Command command = aluno.Adapt<CriarAluno.Command>();
    var result = await mediator.Send(command, cancellationToken);
    return Results.Created($"/alunos/{result.Id}", result.Adapt<AlunoViewModel>());
})
 .WithName("CriarAluno")
 .ProducesValidationProblem()
 .Produces<Aluno>(StatusCodes.Status201Created);

app.MapGet("/alunos/{id}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
    await mediator.Send<Aluno>(new ObeterAluno.Query(id), cancellationToken) is Aluno aluno ?
        Results.Ok(aluno.Adapt<AlunoViewModel>()) :
        Results.NotFound()
)
 .WithName("GetAluno")
 .ProducesValidationProblem()
 .Produces(StatusCodes.Status404NotFound)
 .Produces<AlunoViewModel>(StatusCodes.Status200OK);

app.Run($"http://localhost:{5000}");




static async Task InitDB(IServiceProvider services, ILogger logger)
{
    var connectionString = services.GetService<IConfiguration>().GetConnectionString("postgresdb");
    logger.LogInformation("Validando DB :'{connectionString}'", connectionString);
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<DemoContext>();

    await db.Database.MigrateAsync();
}



[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "Interface de marcacao para obter Assemble")]
public interface IMediatorMark { }
