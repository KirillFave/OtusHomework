using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
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
        protected List<T> Data { get; set; }

        public InMemoryRepository(List<T> data)
        {
            Data = data;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public void Add(T entity)
        {
            Data.Add(entity);
        }

        public bool Update(T entity)
        {
            int index = Data.FindIndex(x => x.Id == entity.Id);

            if (index != -1)
            {
                Data[index] = entity;
                return true;
            }

            return false;
        }

        public bool Delete(Guid id)
        { 
            T entityToRemove = Data.FirstOrDefault(e => e.Id == id);

            if (entityToRemove is null)
            {
                return false;
            }

            Data.Remove(entityToRemove);
                
            return true;
        }
    }
}