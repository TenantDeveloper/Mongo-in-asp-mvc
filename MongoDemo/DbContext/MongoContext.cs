using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MongoDemo.DbContext
{
    public class MongoContext
    {
        readonly MongoClient mongoClient = new MongoClient("mongodb://127.0.0.1:27017/?gssapiServiceName=mongodb");
        private IMongoDatabase DatabaseName;
        public MongoContext(string databaseName)
        {
            this.DatabaseName = mongoClient.GetDatabase(databaseName);
        }
        public async Task<IEnumerable<BsonDocument>> GetBbs() // return list of Database in mongo client;
        {
           return await mongoClient.ListDatabases().ToListAsync();
           
        }

        public IMongoDatabase GetSpecialDb (string dbName)
        {
           return mongoClient.GetDatabase(dbName);
        }


        public async Task<IEnumerable<BsonDocument>> GetCollectionsDb(IMongoDatabase mongoDatabase)
        {
          return await mongoDatabase.ListCollections().ToListAsync();
        }

        public IMongoCollection<BsonDocument> GetSpecificCollection(string collectionName)
        {
           var collection =  DatabaseName.GetCollection<BsonDocument>(collectionName);
            return collection;
        }
        public IEnumerable<BsonDocument> GetSpecificFieldInCollection( string collectionName)
        {
           var collection =  DatabaseName.GetCollection<BsonDocument>(collectionName);
          var filter =  Builders<BsonDocument>.Filter.Eq("name", "Mostafa");
            var result =  collection.Find(filter).ToList();
            return result;
        }

        //create
        [Obsolete]
        public void Create(string collectionName  ,  BsonDocument bsonElements)
        {
            var collection = DatabaseName.GetCollection<BsonDocument>(collectionName);
            //create
            BsonDocument peronalDoc = new BsonDocument();

          
            
                peronalDoc.Add(bsonElements);
            
                
            collection.InsertOne(peronalDoc);
        }

        //Read
        public IEnumerable<BsonDocument> Read(string collectionName , BsonDocument bsonDocument)
        {
         IMongoCollection<BsonDocument> collection =  DatabaseName.GetCollection<BsonDocument>(collectionName);
            
           return  collection.Find(bsonDocument).ToList(); //get object of BsonDocument;
        }

         public BsonDocument GetById(string collectionName , int id)
        {
            var collection = DatabaseName.GetCollection<BsonDocument>(collectionName);
            var filter =  Builders<BsonDocument>.Filter.Eq("_Id", id);
           return collection.Find(filter).First();
        }

        //update
        public void Update(string collectionName ,  BsonDocument oldBsonDocument , BsonDocument newBsonDocument)
        {
            IMongoCollection<BsonDocument> collection = DatabaseName.GetCollection<BsonDocument>(collectionName);
         
            
            
            var updateDoc = collection.FindOneAndReplace(oldBsonDocument, newBsonDocument);
        }

        //Delete

        public void Delete (string collectionName ,  BsonDocument bsonElements)
        {
            IMongoCollection<BsonDocument> collection = DatabaseName.GetCollection<BsonDocument>(collectionName);

        
            collection.FindOneAndDelete(bsonElements);
        }


    }
}