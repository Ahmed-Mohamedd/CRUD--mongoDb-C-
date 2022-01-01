using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoDbdemo
{
    public class mongoCrud
    {
        private IMongoDatabase db;
        public mongoCrud(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        public void insertRecord<T>(string table, T record) // to insert one record into collection
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
        public List<T> readRecord<T>(string table)   // retrieve all records of collection (its equal in sql * )
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
            
        }
        public void upsertRecord<T>(string table, string firstName, T record) //  a method that chech if "element" is foud it wii update it otherwise it will insert it as a new record
        {
            var collection = db.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("firstName", firstName),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        public void updateRecord<T>(string table , string firstName , string lastName , DateTime datetime , string email , string address, int phoneNumber , string yourGender , byte [] img) // update certain record
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("firstName", firstName);
            var update = Builders<T>.Update
                .Set("lastName", lastName)
                .Set("dateTime", datetime)
                .Set("email", email)
                .Set("phoneNumber", phoneNumber)
                .Set("Gender", yourGender)
                .Set("address", address)
                .Set("img", img);
            collection.UpdateOne(filter, update);

        }
        public void deleteRecord<T>(string table, string firstName)   // delete a specific record (we here using filter with first name)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("firstName", firstName);
            collection.DeleteOne(filter);
        }

    }
}
