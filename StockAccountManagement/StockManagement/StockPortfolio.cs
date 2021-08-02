using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAccountManagement.StockManagement
{
    class StockPortfolio
    {
        public void StockReport(string filepath)
        {
            try
            {
                using (StreamReader read = new StreamReader(filepath))
                {
                    var json = read.ReadToEnd();

                    var items = JsonConvert.DeserializeObject<List<StockModel>>(json);
                    Console.WriteLine("StockName\tShares\tSharePrice\tTotal Value Of Stock");
                    foreach (var item in items)
                    {
                        Console.WriteLine("{0}" + "\t" + "{1}" + "\t" + "{2}" + "\t\t" + "{3}", item.StockName, item.Shares, item.SharePrice, item.Shares * item.SharePrice);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
