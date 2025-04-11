using MongoDB.Driver;
using SnakeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SnakeApi.Services
{
    public class ScoreService
    {
        private readonly IMongoCollection<Score> _scores;

        public ScoreService(DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _scores = database.GetCollection<Score>(settings.CollectionName);
        }

        public async Task<Score> GetAsync(string id) =>
              await _scores.Find(score => score.Id == id).FirstOrDefaultAsync();

        public async Task<List<Score>> GetScoreAsync() =>
              await _scores.Find(score => true).SortByDescending(s => s.ScoreValue).Limit(10).ToListAsync();

        public async Task<List<Score>> GetByUsernameAsync(string username) =>
              await _scores.Find(score => score.Username == username).SortByDescending(s => s.ScoreValue).ToListAsync();

        public async Task<Score> CreateAsync(Score score)
        {
            await _scores.InsertOneAsync(score);
            return score;
        }
    }
}
