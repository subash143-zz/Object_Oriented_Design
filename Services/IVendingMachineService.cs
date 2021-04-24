using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Classes;
using VendingMachine.Enums;

namespace VendingMachine.Services
{
    interface IVendingMachineService
    {
        void AddCoins(Coin coinType, int count);

        void AddItems(Item itemType, int count);

        Response MakePurchase(Request purchaseRequest);

    }
}
