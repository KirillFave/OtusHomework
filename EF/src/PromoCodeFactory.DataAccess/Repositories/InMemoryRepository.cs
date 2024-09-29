using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T : BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task Add(T entity)
        {
            Data = Data.Append<T>(entity);
            return Task.CompletedTask;
        }

        public Task Update(T entity)
        {
            List<T> list = Data.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == entity.Id)
                {
                    list[i] = entity;
                    Data = list;
                    return Task.CompletedTask;
                }
            }

            throw new KeyNotFoundException($"{nameof(T)} with ID {entity.Id} not found.");
        }

        public Task Delete(Guid id)
        { 
            List<T> list = Data.ToList();

            T entityToRemove = list.FirstOrDefault(e => e.Id == id);

            if (entityToRemove is null)
            {
                throw new KeyNotFoundException($"{nameof(T)} with ID {id} not found.");
            }

            list.Remove(entityToRemove);
                
            Data = list;
            return Task.CompletedTask;
        }
    }
}