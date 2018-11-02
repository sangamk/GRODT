import { Coin } from "./coin.model";

export interface CoinStat {
    coins: Coin;
    volumeIncrease: number;
    priceIncrease: number;
    points: number;
}