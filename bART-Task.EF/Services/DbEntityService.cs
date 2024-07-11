﻿using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_Task.EF.Services
{
    public class DbEntityService<T> : IDbEntityService<T> where T : DbItem
    {
        private readonly bARTTaskContext _dbContext;
        private bool _disposed;

        public DbEntityService(bARTTaskContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        private async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> Create(T entity)
        {
            var EntityFromDb = await _dbContext.Set<T>().AddAsync(entity);
            await SaveChanges();

            return EntityFromDb.Entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _dbContext.Dispose();
            _disposed = true;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Update(T entity)
        {
            var EntityFromDb = _dbContext.Set<T>().Update(entity);
            await SaveChanges();

            return EntityFromDb.Entity;
        }

        public T GetByIdforUser(long id)
        {
            var result = _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);

            return result;
        }
    }
}

