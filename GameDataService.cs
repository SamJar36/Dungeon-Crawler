using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using DungeonCrawler;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Dungeon_Crawler
{
    public class GameDataService
    {
        private readonly IMongoCollection<BsonDocument> _levelElementsCollection;
        private readonly IMongoCollection<BsonDocument> _enemiesCollection;
        private readonly IMongoCollection<BsonDocument> _playerCollection;

        public GameDataService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _levelElementsCollection = database.GetCollection<BsonDocument>("LevelElements");
            _enemiesCollection = database.GetCollection<BsonDocument>("Enemies");
            _playerCollection = database.GetCollection<BsonDocument>("Player");
        }

        public void SaveMap(LevelData LData)
        {
            LevelElementBsonObject bsonObject = new LevelElementBsonObject(LData.LevelElementList);
            EnemyBsonObject enemyBsonObject = new EnemyBsonObject(LData.EnemyList);

            var bsonDocument1 = bsonObject.ToBsonDocument();
            var bsonDocument2 = enemyBsonObject.ToBsonDocument();
            var bsonDocument3 = LData.Player.ToBsonDocument();

            if (!bsonDocument1.Contains("_id"))
            {
                bsonDocument1["_id"] = ObjectId.GenerateNewId();
            }
            if (!bsonDocument2.Contains("_id"))
            {
                bsonDocument2["_id"] = ObjectId.GenerateNewId();
            }
            if (!bsonDocument3.Contains("_id"))
            {
                bsonDocument3["_id"] = ObjectId.GenerateNewId();
            }

            bool isLevelElementsCollectionEmpty = !_levelElementsCollection.Find(new BsonDocument()).Any();
            bool isEnemiesCollectionEmpty = !_enemiesCollection.Find(new BsonDocument()).Any();
            bool isPlayerCollectionEmpty = !_playerCollection.Find(new BsonDocument()).Any();

            if (!isLevelElementsCollectionEmpty)
            {
                var firstDocument = _levelElementsCollection.Find(new BsonDocument()).FirstOrDefault();
                if (firstDocument != null)
                {
                    _levelElementsCollection.DeleteOne(new BsonDocument("_id", firstDocument["_id"]));
                }
            }
            _levelElementsCollection.InsertOne(bsonDocument1);

            if (!isEnemiesCollectionEmpty)
            {
                var firstDocument = _enemiesCollection.Find(new BsonDocument()).FirstOrDefault();
                if (firstDocument != null)
                {
                    _enemiesCollection.DeleteOne(new BsonDocument("_id", firstDocument["_id"]));
                }
            }
            _enemiesCollection.InsertOne(bsonDocument2);

            if (!isPlayerCollectionEmpty)
            {
                var firstDocument = _playerCollection.Find(new BsonDocument()).FirstOrDefault();
                if (firstDocument != null)
                {
                    _playerCollection.DeleteOne(new BsonDocument("_id", firstDocument["_id"]));
                }
            }
            _playerCollection.InsertOne(bsonDocument3);
        }
        public bool IsThereSavedDataInCollections()
        {
            bool isLevelElementsCollectionEmpty = !_levelElementsCollection.Find(new BsonDocument()).Any();
            bool isEnemiesCollectionEmpty = !_enemiesCollection.Find(new BsonDocument()).Any();
            bool isPlayerCollectionEmpty = !_playerCollection.Find(new BsonDocument()).Any();

            if (isLevelElementsCollectionEmpty || isEnemiesCollectionEmpty || isPlayerCollectionEmpty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void LoadSavedMap(LevelData LData)
        {
            var bsonDocument1 = _levelElementsCollection.Find(new BsonDocument()).FirstOrDefault();
            var bsonDocument2 = _enemiesCollection.Find(new BsonDocument()).FirstOrDefault();
            var bsonDocument3 = _playerCollection.Find(new BsonDocument()).FirstOrDefault();

            if (bsonDocument1 == null || bsonDocument2 == null || bsonDocument3 == null)
            {
                return;
            }

            LevelElementBsonObject bsonObject = BsonSerializer.Deserialize<LevelElementBsonObject>(bsonDocument1);
            EnemyBsonObject enemyBsonObject = BsonSerializer.Deserialize<EnemyBsonObject>(bsonDocument2);
            Player player = BsonSerializer.Deserialize<Player>(bsonDocument3);

            player.LData = LData;
            LData.Player = player;
            LData.Player.DrawPlayer();

            var bsonList = bsonObject.CombineLists();
            var enemyBsonList = enemyBsonObject.CombineLists();
            foreach (var element in bsonList)
            {
                element.Player = player;
                element.CheckIfPreviouslyDrawn();
                LData.LevelElementList.Add(element);
            }
            foreach (var enemy in enemyBsonList)
            {
                enemy.Player = player;
                enemy.LData = LData;
                enemy.Draw();
                LData.EnemyList.Add(enemy);
            }

            LData.Player.Music.PlayMusic("Level");
        }
    }
}
