
using DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class DBRepo
    {
    }

    public class DBServices
    {     
        public interface IDBAction<TEntity> where TEntity : DBRepo
        {           
            void InsertData(TEntity entity);
            void DeleteData(TEntity _entity);
            void UpdateData(TEntity _entity);

        }

        public class DBService<TEntity> : IDBAction<TEntity> where TEntity : DBRepo
        {
            private readonly CoreContext _db;
            private DbSet<TEntity> dbSet;
            public DBService(CoreContext _db)
            {
                this._db = _db;
                this.dbSet = _db.Set<TEntity>();
            }

            public void InsertData(TEntity _entity)
            {
                this.dbSet.Add(_entity);
                this._db.SaveChanges();
            }

            public void DeleteData(TEntity _entity)
            {
                this.dbSet.RemoveRange(_entity);
                this._db.SaveChanges();
            }

            public void UpdateData(TEntity _entity)
            {
                this.dbSet.UpdateRange(_entity);
                this._db.SaveChanges();
            }

        }
    }
}
