using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using DungeonCrawler;
using Newtonsoft.Json;

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
        public async Task SaveAsync(LevelElementJsonObject levelElements, EnemyJsonObject enemies, Player player)
        {
            var bsonDocument1 = levelElements.ToBsonDocument();
            var bsonDocument2 = enemies.ToBsonDocument();
            var bsonDocument3 = player.ToBsonDocument();

            await _levelElementsCollection.ReplaceOneAsync(
            new BsonDocument("_id", bsonDocument1["_id"]),
            bsonDocument1,
            new ReplaceOptions { IsUpsert = true });

            await _enemiesCollection.ReplaceOneAsync(
                new BsonDocument("_id", bsonDocument2["_id"]),
                bsonDocument2,
                new ReplaceOptions { IsUpsert = true });

            await _playerCollection.ReplaceOneAsync(
                new BsonDocument("_id", bsonDocument3["_id"]),
                bsonDocument3,
                new ReplaceOptions { IsUpsert = true });
        }
        public async Task LoadSavedMapAsync(LevelData LData)
        {
            // Retrieve JSON from MongoDB
            var bsonDocument1 = await _levelElementsCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            var bsonDocument2 = await _enemiesCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            var bsonDocument3 = await _playerCollection.Find(new BsonDocument()).FirstOrDefaultAsync();

            if (bsonDocument1 == null || bsonDocument2 == null || bsonDocument3 == null)
            {
                // Handle the case where the documents are not found
                return;
            }

            // Convert BSON to JSON
            string json1 = bsonDocument1.ToJson();
            string json2 = bsonDocument2.ToJson();
            string json3 = bsonDocument3.ToJson();

            // Deserialize JSON to objects
            LevelElementJsonObject jsonObject = JsonConvert.DeserializeObject<LevelElementJsonObject>(json1);
            EnemyJsonObject enemyJsonObject = JsonConvert.DeserializeObject<EnemyJsonObject>(json2);
            Player player = JsonConvert.DeserializeObject<Player>(json3);

            // Set player data and draw player
            player.LData = LData;
            LData.Player = player;
            LData.Player.DrawPlayer();

            // Combine lists and add elements to LevelElementList and EnemyList
            var jsonList = jsonObject.CombineLists();
            var enemyJsonList = enemyJsonObject.CombineLists();
            foreach (var element in jsonList)
            {
                element.Player = player;
                element.CheckIfPreviouslyDrawn();
                LData.LevelElementList.Add(element);
            }
            foreach (var enemy in enemyJsonList)
            {
                enemy.Player = player;
                enemy.LData = LData;
                enemy.Draw();
                LData.EnemyList.Add(enemy);
            }

            // Play level music
            LData.Player.Music.PlayMusic("Level");
        }
    }
}
