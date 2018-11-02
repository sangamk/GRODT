import { Injectable }   from '@angular/core';
import { HttpClient }   from '@angular/common/http';
import { Observable }   from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { CoinStat } from '../models/coinStat.model';
import { CoinStatsView } from '../models/coinStatsView.model';
import { TopPerformerCoin } from '../models/topPerfomerCoin.model';

@Injectable()
export class CoinService {

    private serviceUrl = 'http://localhost:63018/';
    
    constructor(private http: HttpClient) { }

    getCoinDifferences(): Observable<CoinStatsView> { 
        return this.http.get<CoinStatsView>('/api/grodt/coindifferences');
    }

    getTopPerformers(): Observable<TopPerformerCoin[]> {
        return this.http.get<TopPerformerCoin[]>('/api/grodt/TopPerformers/10');
    }
}