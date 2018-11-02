using GRODT.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PumpAndDump;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRODT.Service
{
    public class CoinStatService : ICoinStatService
    {
        private ICoinStatisticsService coinStatistics;
        private static Dictionary<string, int> f = new Dictionary<string, int>();

        RestClient client = new RestClient("https://bittrex.com/api/v1.1/");
        RestRequest request = new RestRequest("public/getmarketsummaries", Method.GET);

        //SETTINGS
        private static int mseconds = 60000;
        private static int trailingMultiplier = 5;
        private static double percentageIncreasePrice = 1.0;

        private IMemoryCache _cache;

        public CoinStatService(ICoinStatisticsService coinStatistics)
        {
            this.coinStatistics = coinStatistics;
        }

        public async Task<IEnumerable<CoinStat>> UpdateAsync()
        {
            coinStatistics.LastCoins = coinStatistics.CurrentCoins;
            List<Coin> coins = await GetCoins();
            coinStatistics.CurrentCoins = coins;

            var stats = coinStatistics.CalcStats();
            UpdateStats();

            var filteredStats = coinStatistics.Filter(stats, percentageIncreasePrice).ToList();

            filteredStats.ForEach(x => Print(x));

            RemoveStats();

            return filteredStats;
        }

        public void Print(CoinStat x)
        {
            if (f.ContainsKey(x.Coin.MarketName))
            {
                f[x.Coin.MarketName] += 5;
            }
            else
            {
                f.Add(x.Coin.MarketName, 5);
            }
            x.Points = f[x.Coin.MarketName];
        }

        public async Task<List<Coin>> GetCoins()
        {
            var response = await client.ExecuteAsync(request);
            BittrexWrapper wrapper = JsonConvert.DeserializeObject<BittrexWrapper>(response.Content);
            List<Coin> coins = wrapper.Result;
            return coins;
        }

        public void UpdateStats()
        {
            f = f.ToDictionary(p => p.Key, p => (p.Value - 1));
        }

        public void RemoveStats()
        {
            foreach (var s in f.Where(kv => kv.Value <= 0).ToList())
            {
                f.Remove(s.Key);
            }
        }

        public Dictionary<string, int> TopPerformers(int numberOfCoins)
        {
            var sortedDict = (from entry in f orderby entry.Value descending select entry)
                               .Take(numberOfCoins)
                               .ToDictionary(pair => pair.Key, pair => pair.Value);
            return sortedDict;
        }
    }



    public static class RestClientExtensions
    {
        public static async Task<RestResponse> ExecuteAsync(this RestClient client, RestRequest request)
        {
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)(await taskCompletion.Task);
        }
    }
}
