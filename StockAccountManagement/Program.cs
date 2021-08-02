using StockAccountManagement.StockManagement;
using System;

namespace StockAccountManagement
{
    class Program
    {
        const string STOCK_JSON = @"C:\Users\USER\source\repos\StockAccountManagement\StockManagement\stock.json";
        static void Main(string[] args)
        {
            StockPortfolio main = new StockPortfolio();
            main.StockReport(STOCK_JSON);
        }
    }
}
