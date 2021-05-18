using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StonksApp2.Models;

using StonksApp2.Repositories;

using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;


namespace StonksApp2.Library
{
    class Library
    {
        readonly static string apiKey = "CK71R2IMBRWD72QE";

        public static Repository repository = new Repository();

        public static string UserID="Default";

        public static async Task<List<Bank>> GetUsersBanks()
        {
            return await repository.GetAllBanks();
        }
        public static async Task<bool> BuyStock(string Symbol, int numberOfShares)
        {
            var stocksClient = new AlphaVantageClient(apiKey).Stocks();

            GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync(Symbol);

            double purchaseCost = (double)globalQuote.Price * numberOfShares;
            Bank userBank = await repository.GetBank(UserID);

            List<TotalStock> totalStocks = await repository.GetTotalStocks(UserID);
            for (int i = 0; i < totalStocks.Count; i++)
            {
                if (totalStocks[i].Symbol == globalQuote.Symbol)
                {
                    

                    

                    if (userBank.Money>=purchaseCost)
                    {
                        userBank.Money -= purchaseCost;
                        await repository.UpdateBank(userBank);

                        double allShares = totalStocks[i].TotalShares + numberOfShares;
                        totalStocks[i].PreviousPrice = (totalStocks[i].TotalShares / allShares) * totalStocks[i].PreviousPrice + (numberOfShares / allShares) * (double) globalQuote.Price;
                        totalStocks[i].TotalShares += numberOfShares;

                        await repository.UpdateTotalsStock(totalStocks[i]);
                        return true;

                    }
                    return false;
                }
            }
            if (userBank.Money >= purchaseCost)
            {
                userBank.Money -= purchaseCost;
                await repository.UpdateBank(userBank);
                await repository.AddTotalStock(new TotalStock()
                {
                    TradeID=new Random().Next(),
                    UserID=UserID,
                    Symbol= globalQuote.Symbol,
                    TotalShares=numberOfShares,
                    PreviousPrice=(double) globalQuote.Price

                });
                return true;
            }
            return false;

        }

       

        public async static Task CreateUser(string UserID)
        {

            Bank toAdd = new Bank() { UserID = UserID, Money = 10000 };
            await repository.AddBank(toAdd);
            
        }

        public static async Task<StockInfo> GetStockInfo(string Symbol)
        {
            var stocksClient = new AlphaVantageClient(apiKey).Stocks();

            GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync(Symbol);

            return new StockInfo { price = (double) globalQuote.Price };
        }

        public static async Task<PlayerPortfolio> GetUserInfo()
        {
            Bank UserBank = await repository.GetBank(UserID);
            Console.WriteLine("Obtained Bank");
            Console.WriteLine(UserBank.Money);
            List<TotalStock> totalStocks = await repository.GetTotalStocks(UserID);

            return new PlayerPortfolio { Money = UserBank.Money, OwnedStocks = totalStocks };
            
        }

        public static async Task<bool> SellStock(string Symbol, int numberOfShares)
        {
            var stocksClient = new AlphaVantageClient(apiKey).Stocks();

            GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync(Symbol);

            double SellCost = (double)globalQuote.Price * numberOfShares;
            Bank userBank = await repository.GetBank(UserID);

            List<TotalStock> totalStocks = await repository.GetTotalStocks(UserID);
            for (int i = 0; i < totalStocks.Count; i++)
            {
                if (totalStocks[i].Symbol == globalQuote.Symbol)
                {

                    if (totalStocks[i].TotalShares >= numberOfShares)
                    {
                        userBank.Money += SellCost;
                        await repository.UpdateBank(userBank);

                        
                        totalStocks[i].TotalShares -= numberOfShares;
                        if(totalStocks[i].TotalShares==0)
                        {
                            await repository.DeleteTotalStock(totalStocks[i]);
                        }
                        await repository.UpdateTotalsStock(totalStocks[i]);
                        return true;

                    }
                    return false;
                }
            }
            return false;
        }
    }
    public struct StockInfo
    {
        public double price;
    }

    /*
    struct OwnedStockInfo
    {
        public string Symbol;
        public int quantity;
        public double previousPrice;
    }
    */

    struct PlayerPortfolio
    {
        public double Money;
        public List<TotalStock> OwnedStocks;
    }
}