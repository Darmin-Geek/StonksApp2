using SQLite;
using StonksApp2.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StonksApp2.Repositories
{
    class Repository
    {
        public event EventHandler<Bank> OnBankAdded;
        public event EventHandler<Bank> OnBankUpdated;

        public event EventHandler<TotalStock> OnTotalStockAdded;
        public event EventHandler<TotalStock> OnTotalStockUpdated;

        private SQLiteAsyncConnection connection;
        /*public async Task<List<Bank>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<TodoItem>().ToListAsync();
        }*/

        public async Task<Bank> GetBank(string UserID)
        {
            await CreateConnection();
            return await connection.Table<Bank>().Where(b => b.UserID == UserID).FirstAsync();
        }

        public async Task<List<Bank>> GetAllBanks()
        {
            await CreateConnection();
            return await connection.Table<Bank>().ToListAsync();
        }
        public async Task<List<TotalStock>> GetTotalStocks(string UserID)
        {
            await CreateConnection();
            return await connection.Table<TotalStock>().ToListAsync();
        }

        public async Task AddBank(Bank item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnBankAdded?.Invoke(this, item);
        }

        public async Task UpdateBank(Bank item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnBankUpdated?.Invoke(this, item);
        }


        public async Task AddTotalStock(TotalStock item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnTotalStockAdded?.Invoke(this, item);
        }

        public async Task UpdateTotalsStock(TotalStock item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnTotalStockUpdated?.Invoke(this, item);
        }

        
            
        public async Task DeleteTotalStock(TotalStock item)
        {
            await CreateConnection();
            await connection.DeleteAsync(item);
        }

        private async Task CreateConnection()
        {
            if (connection != null) { return; }

            //var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var databasePath = Path.Combine(Directory.GetCurrentDirectory(), "StonkApp.db");
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(basePath, "StonkApp2.db");
            Console.WriteLine(databasePath);
            connection = new SQLiteAsyncConnection(databasePath);
            
            await connection.CreateTableAsync<Bank>();
            await connection.CreateTableAsync<TotalStock>();
            if(await connection.Table<Bank>().CountAsync()==0)
            {
                await connection.InsertAsync(new Bank()
                {
                    UserID = "Default",
                    Money = 9001
                });
            }



        }
    }
}