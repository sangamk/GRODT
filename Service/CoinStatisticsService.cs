using GRODT.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PumpAndDump
{
    public class CoinStatisticsService : ICoinStatisticsService
    {
        public List<Coin> LastCoins { get; set; }
        public List<Coin> CurrentCoins { get; set; }

        public CoinStatisticsService()
        {
            LastCoins = new List<Coin>();
            CurrentCoins = new List<Coin>();
        }

        public IList<CoinStat> CalcStats()
        {
            List<CoinStat> stats = new List<CoinStat>();
            foreach (Coin lastCoin in LastCoins)
            {
                Coin currentCoin = CurrentCoins.Find(x => x.MarketName == lastCoin.MarketName);
                if (currentCoin != null)
                {
                    stats.Add(new CoinStat()
                    {
                        Coin = currentCoin,
                        PriceIncrease = Math.Round(DifferencePrice(lastCoin, currentCoin), 2),
                        VolumeIncrease = Math.Round(DifferenceVolume(lastCoin, currentCoin), 2)
                    });
                }
            }
            return stats;
        }

        public double DifferenceVolume(Coin lastCoin, Coin currentCoin)
        {
            return (currentCoin.Volume - lastCoin.Volume) / lastCoin.Volume * 100;
        }

        public double DifferencePrice(Coin lastCoin, Coin currentCoin)
        {
            return (currentCoin.Last - lastCoin.Last) / lastCoin.Last * 100;
        }

        public IEnumerable<CoinStat> Filter(IEnumerable<CoinStat> stats, double percentageIncrease)
        {
            return stats.Where(x => x.PriceIncrease >= percentageIncrease);
        }
    }
}
