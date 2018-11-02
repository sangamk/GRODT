using GRODT.Models;
using GRODT.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using PumpAndDump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRODT.Controllers
{
    [Route("api/[controller]")]
    public class GrodtController : Controller
    {
        ICoinStatService statService;

        public GrodtController(ICoinStatService statService)
        {
            this.statService = statService;
        }

        [HttpGet("[action]")]
        public async Task<CoinStatsView> CoinDifferences()
        {
            IEnumerable<CoinStat> coinStats = await statService.UpdateAsync();
            return new CoinStatsView { CoinStats = coinStats, TimeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
        }

        [HttpGet("[action]/{numberOfCoins}")]
        public IEnumerable<TopPerformerCoinView> TopPerformers(int numberOfCoins)
        {
            return statService.TopPerformers(numberOfCoins).Select(p => new TopPerformerCoinView { MarketName = p.Key, Points = p.Value });
            ;
        }
    }
}
