using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Enums;

namespace VendingMachine.Classes
{
    public class Response: InputOutput
    {
        public string Message;
        public bool isError;
        public Response(): base()
        {

        }
        private Response(bool isError)
        {
            this.isError = false;
        }

        private Response(string Message, bool isError)
        {
            this.Message = Message;
            this.isError = isError;
        }

        public static Response MakeSuccessfulResponseWithoutChange(Request purchaseRequest)
        {
            Response response = new Response();
            response.Order = purchaseRequest.Order;
            return response;
        }

        public static Response MakeSuccessfulResponseWithChange(Request purchaseRequest, Dictionary<Coin, int> coins)
        {
            Response response = new Response(false);
            response.Coins = coins;
            response.Order = purchaseRequest.Order;
            return response;
        }

        public static Response MakeInSufficientMoneyResponse(Request purchaseRequest)
        {
            Response response =  new Response("Error making purchase, please input enough money!", true);
            return response;
        }
        public static Response MakeInSufficientChangeResponse(Request purchaseRequest)
        {
            Response response = new Response("Sorry, the machine doesn't have enough change, please ask the support!", true);
            return response;
        }
        public static Response MakeOutOfStockResponse(Request purchaseRequest)
        {
            Response response = new Response("Error making purchase, the item is out of stock!", true);
            return response;
        }


    }
}
