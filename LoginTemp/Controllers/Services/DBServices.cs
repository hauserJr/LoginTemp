
using DB;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Services
{
    public class DBRepo
    { 
    }

    public class DBServices
    {     
        public interface IDBAction<TEntity> where TEntity : class,new()
        {           
            void InsertData(TEntity entity);
            void DeleteData(TEntity _entity);
            void UpdateData(TEntity _entity);
            void GetAllData(TEntity _entity);
        }

        public class DBService<TEntity> : IDBAction<TEntity> where TEntity : class,new()
        {
            private readonly CoreContext _db;
            //private DbSet<TEntity> dbSet;
            public DBService(CoreContext _db)
            {
                this._db = _db;
                //this.dbSet = _db.Set<TEntity>();
            }

            public void InsertData(TEntity _entity)
            {
                this._db.Set<TEntity>().Add(_entity);
                this._db.SaveChanges();
            }

            public void DeleteData(TEntity _entity)
            {
                this._db.Set<TEntity>().RemoveRange(_entity);
                this._db.SaveChanges();
            }

            public void UpdateData(TEntity _entity)
            {
                this._db.Set<TEntity>().UpdateRange(_entity);
                this._db.SaveChanges();
            }
            public void GetAllData(TEntity _entity)
            {
                var x = this._db.Set<TEntity>().Select(o => o).ToList();
            }
        }
    }
}
