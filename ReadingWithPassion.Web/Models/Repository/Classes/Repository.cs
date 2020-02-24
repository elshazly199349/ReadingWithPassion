using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingWithPassion.Web.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReadingWithPassion.Web.Models.Repository.Classes
{
    public class Repository<TEntity, TEntityViewModel> : IRepository<TEntity, TEntityViewModel>
        where TEntity : class, new() where TEntityViewModel : class, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _entities;
        private readonly IEnumerable<TEntityViewModel> _entitiesViewModels;
        private readonly IMapper _mapper;
        public Repository(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
            _entitiesViewModels = new List<TEntityViewModel>();
            _mapper = mapper;
        }

        public TEntityViewModel Add(TEntityViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _dbContext.Add(entity);
            model = _mapper.Map<TEntityViewModel>(entity);
            return model;
        }

        public void Remove(TEntityViewModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Dispose() => _dbContext.Dispose();

        public TEntity Find(params object[] id)
        {
            return _entities.Find(id);
        }

        public TEntityViewModel FindToViewModel(params object[] id)
        {
            var entity = _entities.Find(id);
            return _mapper.Map<TEntityViewModel>(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public IEnumerable<TEntityViewModel> GetAllToViewModels()
        {
            var entities = _entities.ToList();
            return _mapper.Map<IEnumerable<TEntityViewModel>>(entities);
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public IEnumerable<TEntityViewModel> SearchtoViewModels(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _entities.Where(predicate).ToList();
            return _mapper.Map<IEnumerable<TEntityViewModel>>(entities);
        }

        public TEntityViewModel Update(TEntityViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _entities.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return model;
        }
    }
}
