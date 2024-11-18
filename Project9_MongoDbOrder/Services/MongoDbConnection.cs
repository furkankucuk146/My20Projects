using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9_MongoDbOrder.Services
{
    public class MongoDbConnection
    {
        private IMongoDatabase _database;
        //Veritabanını burda oluşturduk
        public MongoDbConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017"); //veritabanı bağlantı adresi
            _database = client.GetDatabase("Db9ProjectOrder"); //veri tabanı ismi
        }

        public IMongoCollection<BsonDocument> GetOrdersCollection() //Koleksiyon yani tablo isimleri
        {
            return _database.GetCollection<BsonDocument>("Orders");
        }
    }
}
