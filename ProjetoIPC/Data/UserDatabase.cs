using ProjetoIPC.Models;
using SQLite;

namespace ProjetoIPC.Data
{
    public class UserDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Trip>().Wait();
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
        public Task<List<Trip>> GetTripsOrderedByDateAsync()
        {
            // Ordena por Id decrescente (mais recente primeiro)
            return _database.Table<Trip>().OrderByDescending(t => t.Id).ToListAsync();
        }

        public Task<int> UpdateTripAsync(Trip trip)
        {
            return _database.UpdateAsync(trip);
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }
        public Task<int> SaveTripAsync(Trip trip)
        {
            return _database.InsertAsync(trip);
        }
    }
}
