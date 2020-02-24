using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReadingWithPassion.Web.Models.Repository.Interfaces
{
    public interface IRepository<TEntity,TEntityViewModel>:IDisposable where TEntity:class where TEntityViewModel:class
    {
        IQueryable<TEntity> GetAll();
        IEnumerable<TEntityViewModel> GetAllToViewModels();
        IEnumerable<TEntity> Search(Expression<Func<TEntity,bool>> predicate);
        IEnumerable<TEntityViewModel> SearchtoViewModels(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(params object[] id);
        TEntityViewModel FindToViewModel(params object[] id);
        TEntityViewModel Add(TEntityViewModel model);
        TEntityViewModel Update(TEntityViewModel model);
        void Remove(TEntityViewModel model);
    }
}
