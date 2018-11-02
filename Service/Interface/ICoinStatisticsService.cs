using PumpAndDump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRODT.Service
{
    public interface ICoinStatisticsService
    {
        List<Coin> LastCoins { get; set; }
        List<Coin> CurrentCoins { get; set; }

        IList<CoinStat> CalcStats();
        IEnumerable<CoinStat> Filter(IEnumerable<CoinStat> stats, double percentageIncrease);
    }
}
