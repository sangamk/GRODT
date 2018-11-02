using PumpAndDump;
using System;
using System.Collections.Generic;

namespace GRODT.Models
{
    public class CoinStatsView
    {
        public IEnumerable<CoinStat> CoinStats { get; set; }
        public string TimeStamp { get; set; }
    }
}
