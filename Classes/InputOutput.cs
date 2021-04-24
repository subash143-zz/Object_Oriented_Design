using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Enums;

namespace VendingMachine.Classes
{
    public class InputOutput
    {
        protected Dictionary<Coin, int> Coins;

        public Item Order;

        public InputOutput()
        {
            Coins = new Dictionary<Coin, int>();
            Coins[Coin.Cent] = 0;
            Coins[Coin.Nickel] = 0;
            Coins[Coin.Dime] = 0;
            Coins[Coin.Quarter] = 0;
            Coins[Coin.Dollar] = 0;
        }

        public void AddCent()
        {
            Coins[Coin.Cent]++;
        }

        public void AddNickel()
        {
            Coins[Coin.Nickel]++;
        }
        public void AddDime()
        {
            Coins[Coin.Dime]++;
        }
        public void AddQuarter()
        {
            Coins[Coin.Quarter]++;
        }
        public void AddDollar()
        {
            Coins[Coin.Dollar]++;
        }

        public void AddOrder(Item item)
        {
            this.Order = item;
        }

        public int GetTotal()
        {
            return Coins[Coin.Cent] + Coins[Coin.Nickel] * 5 + Coins[Coin.Dime] * 10 + Coins[Coin.Quarter] * 25 + Coins[Coin.Dollar] * 100;
        }

    }
}
