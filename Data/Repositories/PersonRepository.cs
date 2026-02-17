using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class PersonRepository : IRepository<PersonEntity, Guid>,
        ICodeRepository<PersonEntity>
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(p  => p.Id == id);
        }

        public async Task<IEnumerable<PersonEntity>> GetAllAsync()
        {
            return await _context.Persons
                .AsNoTracking()
                .OrderBy(P => P.FirstName)
                .ThenBy(P => P.LastName)
                .ToListAsync();
        }

        public async  Task AddAsync(PersonEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Persons.AddAsync(entity);
        }

        public Task UpdateAsync(PersonEntity entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Persons.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(PersonEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
              
            _context.Persons.Remove(entity);
            return Task.CompletedTask;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // ICodeRepository

        public async Task<PersonEntity?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El código no puede estar vacio", nameof(code)); 
            }

            var nomralizedCode = code.ToUpperInvariant();

            return await _context.Persons.FirstOrDefaultAsync(p => p.Code == nomralizedCode);
        }

        public async Task<bool> ExistsWithCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("El código no puede estar vacio", nameof(code));
            }

            var nomralizedCode = code.ToUpperInvariant();

            return await _context.Persons.AnyAsync(p => p.Code == nomralizedCode);
        }

    }
}
