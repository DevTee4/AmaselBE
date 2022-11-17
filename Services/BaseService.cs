using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AmaselBE.Configuration;
using AmaselBE.Model;
using MongoDB.Driver;

namespace AmaselBE.Services
{
    public class BaseService<T> where T : BaseModel
    {
        const int limit = 10;
        const int skip = 0;
        public string TableName { get; set; }
        // public Company Company { get; set; }
        private readonly IMongoCollection<T> _entities;

        public Setting Setting { get; set; }

        public MongoClient Client { get; set; }

        public IMongoDatabase Database { get; set; }
        public BaseService(Setting setting)
        {
            Setting = setting;
            Client = new MongoClient(Setting.ConnectionString);
            Database = Client.GetDatabase(Setting.DatabaseName);
            _entities = Database.GetCollection<T>(typeof(T).Name);
            _entities.Indexes.CreateOne(new CreateIndexModel<T>(Builders<T>.IndexKeys.Text("$**")));
        }

        //  public void SetCompany(string id){
        //      var companyEntity = Database.GetCollection<T>(TableName);
        //      Company = companyEntity.Find(entity => (entity as Company).Id==id).FirstOrDefault() as Company;
        //  }

        public List<T> Get(int skip = skip, int limit = limit) =>
                  _entities.Find(entity => true).ToList();

        public List<T> Get(Expression<Func<T, bool>> predicate, int skip = skip, int limit = limit) =>
            _entities.Find(predicate).Skip(skip).Limit(limit).ToList();

        public T Get(string id) =>
            _entities.Find<T>(entity => entity.Id == id).FirstOrDefault();

        public T Create(T entity)
        {
            _entities.InsertOne(entity);
            return entity;
        }
        public List<T> Create(List<T> entity)
        {
            entity.ForEach(entity =>
            {
                entity.CreatedAt = DateTime.Now;
            });
            _entities.InsertMany(entity);
            return entity;
        }

        public List<T> Save(List<T> entity)
        {
            entity.ForEach(entity =>
            {
                entity.UpdatedAt = DateTime.Now;
            });
            var newObjs = entity.Where(ent => ent.State == ObjectState.New).ToList();
            var updateObjs = entity.Where(ent => ent.State == ObjectState.Changed).ToList();
            var delObjs = entity.Where(ent => ent.State == ObjectState.Removed).ToList();

            if (newObjs.ToList().Count > 0)
            {
                Create(newObjs);
            }
            if (updateObjs.ToList().Count > 0)
            {
                Update(updateObjs);
            }
            if (delObjs.ToList().Count > 0)
            {
                Remove(delObjs.Select(s => s.Id).ToList());
            }
            entity.ForEach(entity =>
            {
                entity.State = ObjectState.Unchanged;
            });
            return entity;
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _entities.ReplaceOne(book => book.Id == entity.Id, entity);
        }

        public void Update(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                _entities.ReplaceOne(e => e.Id == entity.Id, entity);
            });
        }

        public List<T> Search(string param) =>
           _entities.Find(Builders<T>.Filter.Text(param)).ToList();

        public void Remove(string id)
        {
            var split = id.Split(",");
            var filter = Builders<T>.Filter.In(u => u.Id, split);
            _entities.DeleteMany(filter);
        }
        public void RemoveAll() =>
            _entities.DeleteManyAsync(Builders<T>.Filter.Empty);

        public void Remove(T entity) =>
            _entities.DeleteOne(book => book.Id == entity.Id);

        public void Remove(List<string> ids)
        {
            var idsFilter = Builders<T>.Filter.In(d => d.Id, ids);
            _entities.DeleteMany(idsFilter);

        }
    }
}