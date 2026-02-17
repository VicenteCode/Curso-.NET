using Application.DTOs.Persons;
using Application.UseCases.Persons;

namespace WebApi.Endpoints
{
    public static class PersonEdpoints
    {
        public static void MapPersonsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/persons")
                .WithTags("Persons");

            group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(id);
                    return Results.Ok(person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            })
            .WithName("GetPersonById")
            .WithSummary("Obtener una persona por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreatePersonDto dto, CreatePersonUseCase useCase) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/persons/{person.Id}", person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("CreatePerson")
            .WithSummary("Crear una nueva persona")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/", async (GetAllPersonsUseCase useCase) =>
            {
                try
                {
                    var persons = await useCase.ExecuteAsync();
                    return Results.Ok(persons);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("GetAllPersons")
            .WithSummary("Obtener todas las personas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id:guid}", async (Guid id, UpdatePersonDto dto, UpdatePersonUseCase useCase) =>
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest("Los ids no corresponden");
                }

                try
                {
                    var persons = await useCase.ExecuteAsync(dto);
                    return Results.Ok(persons);
                }
                catch(InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message  });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch(Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

            }).WithName("UpdatePerson")
            .WithSummary("Actualizar una persona existente")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{id:guid}", async (Guid id, DeletePersonUseCase useCase) =>
            {
                try
                {
                    await useCase.ExecuteAsync(id);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("DeletePerson")
            .WithSummary("Eliminar una persona")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/code/{code}", async (string code, GetPersonByCodeUseCase useCase) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(code);
                    return Results.Ok(person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("GetPersonByCode")
            .WithSummary("Obtener una persona por su código")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
