using System.Collections.Generic;

namespace PumpAndDump
{
    public class BittrexWrapper
    {
        public bool Succes { set; get; }
        public string Message { set; get; }
        public List<Coin> Result { get; set; }
    }
}
