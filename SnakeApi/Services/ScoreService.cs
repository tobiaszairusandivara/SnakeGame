using MongoDB.Driver;
using SnakeApi.Models;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;


namespace SnakeApi.Services
{
    public class ScoreService
    {
        private readonly IMongoCollection<Score> _scoreCollection;

        public ScoreService(SnakeDatabaseSettings snakeDbSettings)
        {
        //En la llamada al método GetCollection<TDocument>(collection):
        //collection representa el nombre de la colección.
        //TDocument representa el tipo de objeto CLR almacenado en la colección.
            var client = new MongoClient(snakeDbSettings.ConnectionString);
            var database = client.GetDatabase(snakeDbSettings.DatabaseName);
            _scoreCollection = database.GetCollection<Score>(snakeDbSettings.CollectionName);
        }

        public async Task<List<Score>> GetAsync() =>
              await _scoreCollection.Find(score => true).SortByDescending(s => s.ScoreValue).Limit(10).ToListAsync();

        public async Task<Score> GetAsync(string id) =>
              await _scoreCollection.Find(score => score.Id == id).FirstOrDefaultAsync();

        public async Task<List<Score>> GetByUsernameAsync(string username) =>
              await _scoreCollection.Find(score => score.Username == username).SortByDescending(s => s.ScoreValue).ToListAsync();

        public async Task<Score> CreateAsync(Score score)
        {
            await _scoreCollection.InsertOneAsync(score);
            return score;
        }
    }
}
