using PumpAndDump;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRODT.Service.Interface
{
    public interface ICoinStatService
    {
        Task<IEnumerable<CoinStat>> UpdateAsync();
        Dictionary<string, int> TopPerformers(int numberOfCoins);
    }
}
