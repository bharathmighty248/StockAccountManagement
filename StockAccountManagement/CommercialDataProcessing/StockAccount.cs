using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAccountManagement.CommercialDataProcessing
{
    class StockAccount
    {
        const string MEMBER_JSON = @"D:\BridgeLabz Problems Git Hub Local Repository\StockAccountManagement\StockAccountManagement\CommercialDataProcessing\Members.json";
        const string COMPANY_JSON = @"D:\BridgeLabz Problems Git Hub Local Repository\StockAccountManagement\StockAccountManagement\CommercialDataProcessing\Company.json";

        /// <summary>
        /// Shows All Members 
        /// </summary>
        public void Members(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    string membersFile = File.ReadAllText(filepath);
                    List<MemberShares> membersList = JsonConvert.DeserializeObject<List<MemberShares>>(membersFile);
                    Console.WriteLine("Name  \tSymbol \tShares\tAmount \tDate&Time");
                    foreach(MemberShares members in membersList)
                    {
                        Console.WriteLine(members.Name + "\t" + members.Symbol + "\t" + members.NumberOfShares + "\t" + members.Amount + "\t" + members.DateTime);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// To Add New Member
        /// </summary>
        public void AddMemberAccount(string filepath)
        {
            if (File.Exists(filepath))
            {
                string membersFile = File.ReadAllText(filepath);
                List<MemberShares> membersList = JsonConvert.DeserializeObject<List<MemberShares>>(membersFile);
                MemberShares members = new MemberShares();
                Console.Write("Enter New Member Name: ");
                members.Name = Console.ReadLine();
                members.NumberOfShares = 0;
                Console.Write("Enter Amount: ");
                members.Amount = Convert.ToDouble(Console.ReadLine());
                Console.Write("Give Symbol: ");
                members.Symbol = Console.ReadLine();
                DateTime dateTime = DateTime.Now;
                members.DateTime = Convert.ToString(dateTime);
                membersList.Add(members);
                File.WriteAllText(filepath, JsonConvert.SerializeObject(membersList));
                Console.WriteLine("New Member Stock Account Created Successfully.....");
            }
        }

        /// <summary>
        /// Shows Value of Member Account
        /// </summary>
        public double ValueOf(string memberName)
        {
            bool check = false;
            double amount = 0;

            List<MemberShares> membersList = JsonConvert.DeserializeObject<List<MemberShares>>(File.ReadAllText(MEMBER_JSON));
            foreach (MemberShares members in membersList)
            {
                if (members.Name.Equals(memberName))
                {
                    check = true;
                    amount = members.Amount;
                    break;
                }
            }
            if (check == false)
                amount = -1;
            return amount;
        }

        public void Buy(double amount, string symbol)
        {
            double noOfShare;
            string shareName;
            bool checkSharename = false;
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(COMPANY_JSON);
            List<CompanyShares> companyList = JsonConvert.DeserializeObject<List<CompanyShares>>(File.ReadAllText(COMPANY_JSON));
            Console.WriteLine("Enter the name of share which do you want to buy");
            shareName = Console.ReadLine();
            foreach (CompanyShares allStocks in companyList)
            {
                if (allStocks.StockName.Equals(shareName))
                {
                    noOfShare = amount / allStocks.SharePrice;
                    if (allStocks.Shares >= noOfShare)
                    {
                        Console.WriteLine("Share buy=" + noOfShare);
                        allStocks.Shares = allStocks.Shares - noOfShare;
                        Console.WriteLine(noOfShare + "Share Buy successfully......");
                        File.WriteAllText(COMPANY_JSON, JsonConvert.SerializeObject(companyList));
                        List<MemberShares> membersList = JsonConvert.DeserializeObject<List<MemberShares>>(File.ReadAllText(MEMBER_JSON));
                        foreach (MemberShares members in membersList)
                        {
                            if (members.Symbol.Equals(symbol))
                            {
                                DateTime dateTime = DateTime.Now;
                                members.NumberOfShares = members.NumberOfShares + noOfShare;
                                members.Amount = members.Amount - amount;
                                members.DateTime = Convert.ToString(dateTime);
                                File.WriteAllText(MEMBER_JSON, JsonConvert.SerializeObject(membersList));
                                Console.WriteLine("Customer Information also updated..");
                                break;
                            }
                        }
                    }
                    else
                        Console.WriteLine("Don't Have Enough Amount To Buy");
                    checkSharename = true;
                    break;
                }
            }
            if (checkSharename == false)
                Console.WriteLine(shareName + " share not available");
        }

        public void Sell(double amount, string symbol)
        {
            bool checkSymbol = false, checkStock = false;
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(COMPANY_JSON);
            Console.WriteLine("Which share do you want to sell");
            string shareName = Console.ReadLine();
            List<CompanyShares> stockList = JsonConvert.DeserializeObject<List<CompanyShares>>(File.ReadAllText(COMPANY_JSON));
            foreach (CompanyShares allStocks in stockList)
            {
                if (allStocks.StockName.Equals(shareName))
                {
                    double sellShares = (amount / allStocks.SharePrice);
                    List<MemberShares> membersList = JsonConvert.DeserializeObject<List<MemberShares>>(File.ReadAllText(MEMBER_JSON));
                    foreach (MemberShares members in membersList)
                    {
                        if (members.Symbol.Equals(symbol))
                        {
                            if (members.NumberOfShares >= sellShares)
                            {
                                members.Amount += amount;
                                members.NumberOfShares -= sellShares;
                                DateTime dateTime = DateTime.Now;
                                members.DateTime = Convert.ToString(dateTime);
                                File.WriteAllText(MEMBER_JSON, JsonConvert.SerializeObject(membersList));
                                allStocks.Shares += sellShares;
                                File.WriteAllText(COMPANY_JSON, JsonConvert.SerializeObject(stockList));
                                Console.WriteLine("sell of Stock is successfull.....");
                                checkSymbol = true;
                                break;
                            }
                            else
                            {
                                checkSymbol = true;
                                Console.WriteLine("Customer does not contains this number of share :" + sellShares);
                                break;
                            }

                        }
                    }
                    checkStock = true;
                }
            }
            if (checkSymbol == false)
                Console.WriteLine("Customer does not contains this Symbol");
            if (checkStock == false)
                Console.WriteLine("This stock are not available");
        }

        public void PrintReport()
        {
            Console.WriteLine("Detail Report of stock are listed below :");
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(COMPANY_JSON);
        }
    }
}
