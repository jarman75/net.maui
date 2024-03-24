using System.Text.Json;

namespace BlazorApp.Data;
public class DataService
{
    public List<Transaction> GetTransactions()
    {
        return new List<Transaction>
        {
            new Transaction { Id = 1, Description = "Groceries", Amount = 25.00m, Date = DateTime.Now },
            new Transaction { Id = 2, Description = "Gas", Amount = 50.00m, Date = DateTime.Now },
            new Transaction { Id = 3, Description = "Dinner", Amount = 75.00m, Date = DateTime.Now },
            new Transaction { Id = 4, Description = "Movies", Amount = 30.00m, Date = DateTime.Now },
            new Transaction { Id = 5, Description = "Rent", Amount = 1000.00m, Date = DateTime.Now }
        };
    }

    public Task CacheTransactions()
    {
        var transactions = JsonSerializer.Serialize<List<Transaction>>(GetTransactions());
        return SecureStorage.Default.SetAsync("transactions", transactions);
    } 

    public async Task<List<Transaction>> GetCacheTransactions()
    {
       var transactions = await SecureStorage.Default.GetAsync("transactions");           
        
       return transactions != null ? JsonSerializer.Deserialize<List<Transaction>>(transactions)! : [];
        
    }
}
