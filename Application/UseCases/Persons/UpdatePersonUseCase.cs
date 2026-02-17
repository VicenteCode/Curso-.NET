using Application.DTOs.Persons;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public UpdatePersonUseCase(IRepository<PersonEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PersonEntity> ExecuteAsync(UpdatePersonDto dto)
        {
            var person = await _repository.GetByIdAsync(dto.Id);

            if (person == null)
            {
                throw new InvalidOperationException($"No se encontró una persona con el ID: { dto.Id }");
            }

            person.UpdatePersonalInfo(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

            await _repository.UpdateAsync(person);
            await _repository.SaveChangesAsync();

            return person;

        }
    }
}
