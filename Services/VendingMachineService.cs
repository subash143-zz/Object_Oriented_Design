using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Classes;
using VendingMachine.Enums;

namespace VendingMachine.Services
{
    public class VendingMachineService: IVendingMachineService
    {
        private Dictionary<Coin, int> coinsCount = new Dictionary<Coin, int>();
        private Dictionary<Item, int> itemsCount = new Dictionary<Item, int>();

        public VendingMachineService()
        {
            //Initializing all the counsCount to 0
            foreach (string name in Enum.GetNames(typeof(Coin)))
            {
                Enum.TryParse(name, out Coin mal);
                coinsCount[mal] = 0;
            }

            //Initializing all the itemsCount to 0
            foreach (string name in Enum.GetNames(typeof(Item)))
            {
                Enum.TryParse(name, out Item itm);
                itemsCount[itm] = 0;
            }
        }

        public void AddCoins(Coin coinType, int count)
        {
            coinsCount[coinType] += count;
        }

        public void AddItems(Item itemType, int count)
        {
            itemsCount[itemType] += count;
        }
        public int GetTotal()
        {
            return coinsCount[Coin.Cent] + coinsCount[Coin.Nickel] * 5 + coinsCount[Coin.Dime] * 10 + coinsCount[Coin.Quarter] * 25 + coinsCount[Coin.Dollar] * 100;
        }

        public Response MakePurchase(Request purchaseRequest)
        {
            if((int)purchaseRequest.Order > purchaseRequest.GetTotal())
            {
                return Response.MakeInSufficientMoneyResponse(purchaseRequest);
            }
            else if(itemsCount[purchaseRequest.Order] == 0)
            {
                return Response.MakeOutOfStockResponse(purchaseRequest);
            }

            int totalAmount = purchaseRequest.GetTotal();
            int price = (int)purchaseRequest.Order;

            int change = totalAmount - price;
            if (change == 0)
            {
                //Successful response without a change
                return Response.MakeSuccessfulResponseWithoutChange(purchaseRequest);
            }
            else if(change > GetTotal()){
                return Response.MakeInSufficientChangeResponse(purchaseRequest);
            }


            int dollarsToDeduct = change / 100;
            change = change % 100;
            if(dollarsToDeduct > coinsCount[Coin.Dollar])
            {
                change += (dollarsToDeduct - coinsCount[Coin.Dollar]) * 100;
                dollarsToDeduct = coinsCount[Coin.Dollar];
            }

            int quartersToDeduct = change / 25;
            change = change % 25;
            if (quartersToDeduct > coinsCount[Coin.Quarter])
            {
                change += (quartersToDeduct - coinsCount[Coin.Quarter]) * 25;
                quartersToDeduct = coinsCount[Coin.Quarter];
            }

            int dimesToDeduct = change / 10;
            change = change % 10;
            if (dimesToDeduct > coinsCount[Coin.Dime])
            {
                change += (dimesToDeduct - coinsCount[Coin.Dime]) * 10;
                dimesToDeduct = coinsCount[Coin.Dime];
            }

            int nickelsToDeduct = change / 5;
            change = change % 5;
            if (nickelsToDeduct > coinsCount[Coin.Nickel])
            {
                change += (nickelsToDeduct - coinsCount[Coin.Nickel]) * 5;
                nickelsToDeduct = coinsCount[Coin.Nickel];
            }

            int centsToDeduct = change;
            if(centsToDeduct > coinsCount[Coin.Cent])
            {
                return Response.MakeInSufficientChangeResponse(purchaseRequest);
            }

            //Successful Reseponse with a change
            coinsCount[Coin.Dollar] -= dollarsToDeduct;
            coinsCount[Coin.Quarter] -= quartersToDeduct;
            coinsCount[Coin.Dime] -= dimesToDeduct;
            coinsCount[Coin.Nickel] -= nickelsToDeduct;
            coinsCount[Coin.Cent] -= centsToDeduct;

            var coins = new Dictionary<Coin, int>();
            coins[Coin.Cent] = centsToDeduct;
            coins[Coin.Nickel] = nickelsToDeduct;
            coins[Coin.Dime] = dimesToDeduct;
            coins[Coin.Quarter] = quartersToDeduct;
            coins[Coin.Dollar] = dollarsToDeduct;
            return Response.MakeSuccessfulResponseWithChange(purchaseRequest, coins);
        }
    }
}
