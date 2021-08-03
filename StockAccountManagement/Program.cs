using StockAccountManagement.CommercialDataProcessing;
using System;

namespace StockAccountManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int userChoice = 0;
            double amount;
            string symbol;
            //constants
            const string MEMBER_JSON = @"D:\BridgeLabz Problems Git Hub Local Repository\StockAccountManagement\StockAccountManagement\CommercialDataProcessing\Members.json";
            const int SHOW_ALLCUSTOMER = 1, ADD_NEW_ACCOUNT = 2, VALUE_OF_ACCOUNT = 3, BUY = 4, SELL = 5, PRINT_REPORT = 6, EXIT = 7;
            StockAccount stockAccount = new StockAccount();
            while (userChoice != 7)
            {
                Console.Write("\n" +
                    "1. Show All Account List\n" +
                    "2. Add New Account\n" +
                    "3. Get Value of Account\n" +
                    "4. Buy a Share\n" +
                    "5. Sell a Share\n" +
                    "6. PrintReport\n" +
                    "7. Exit\n" +
                    "Please Select One Option, What do You Want: ");
                userChoice = Convert.ToInt16(Console.ReadLine());
                switch (userChoice)
                {
                    case SHOW_ALLCUSTOMER:
                        stockAccount.Members(MEMBER_JSON);
                        break;
                    case ADD_NEW_ACCOUNT:
                        stockAccount.AddMemberAccount(MEMBER_JSON);
                        break;
                    case VALUE_OF_ACCOUNT:
                        string customerName;
                        Console.WriteLine("Enter the name of customer to show total amount");
                        customerName = Console.ReadLine();
                        amount = stockAccount.ValueOf(customerName);
                        if (amount < 0)
                            Console.WriteLine("Account name does not exit");
                        else
                            Console.WriteLine("Account Amount is Rs. :" + amount);
                        break;
                    case BUY:
                        Console.WriteLine("Enter a amount");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter a symbol");
                        symbol = Console.ReadLine();
                        stockAccount.Buy(amount, symbol);
                        break;
                    case SELL:
                        Console.WriteLine("Enter a amount");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter a symbol");
                        symbol = Console.ReadLine();
                        stockAccount.Sell(amount, symbol);
                        break;
                    case PRINT_REPORT:
                        stockAccount.PrintReport();
                        break;
                    case EXIT:
                        userChoice = 7;
                        break;
                    default:
                        Console.WriteLine("You have entered wrong choice");
                        break;
                }
            }
        }
    }
}
