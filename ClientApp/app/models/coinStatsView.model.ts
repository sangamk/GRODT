import { CoinStat } from "./coinStat.model";

export interface CoinStatsView {
    timeStamp: string;
    coinStats: CoinStat[];
}