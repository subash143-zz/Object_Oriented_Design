using System;
using VendingMachine.Classes;
using VendingMachine.Enums;
using VendingMachine.Services;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            IVendingMachineService vendingMachine = new VendingMachineService();
            vendingMachine.AddItems(Enums.Item.Chip, 2);
            vendingMachine.AddCoins(Enums.Coin.Cent, 0);

            Request request = new Request(Item.Chip);
            request.AddDollar();

            var retVal = vendingMachine.MakePurchase(request);


            
            
        }
    }
}
