using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public DeletePersonUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);

            if (person == null)
            {
                throw new InvalidOperationException($"No se encontró una persona con el ID: {id}");
            }

            await _repository.DeleteAsync(person);
            await _repository.SaveChangesAsync();
        }
    }
}
