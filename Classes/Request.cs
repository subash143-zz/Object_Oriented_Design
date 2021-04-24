using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Enums;

namespace VendingMachine.Classes
{
    public class Request : InputOutput
    {
        public Request(Item order): base()
        {
            Order = order;
        }
    }
}
