import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CoinService } from '../../services/coin.service'; 
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/interval';
import { CoinStat } from "../../models/coinStat.model";
import { CoinStatsView } from "../../models/coinStatsView.model";
import { TopPerformerCoin } from "../../models/topPerfomerCoin.model";

@Component({
    selector: 'grodt',
    templateUrl: './grodt.component.html',
    styleUrls: ['./grodt.component.css']
})


export class GrodtComponent implements OnInit {
    allCoins : CoinStat[];
    tableData : CoinStatsView[];
    topPerformers: TopPerformerCoin[];

    @ViewChild('content') content: ElementRef;

    subscribtion = Observable.interval(125000).subscribe(x => {
        this.updateAllCoins(x);
    });


    constructor(private coinService: CoinService) { }

    ngOnInit() {
        this.coinService.getCoinDifferences().subscribe(
            data => {
                this.tableData = [data];
        });
    }

    updateAllCoins(x : number){
        this.coinService.getCoinDifferences().subscribe(
            data => {
                this.tableData.unshift(data);
                this.coinService.getTopPerformers().subscribe(
                    data => {
                        this.topPerformers = data;
                    });
            });


    }

    coinStyle(points : number){
        if( points <= 5){
            return {'color': '#FAFAFA', 'font-size': 'large'};
        }else if(points <= 9){
            return {'color': '#00E676', 'font-size': 'large'};
        }else if (points <= 14){
            return {'color': '#81D4FA', 'font-size': 'large'};
        }else if(points <= 19){
            return {'color': '#FF8A65', 'font-size': 'large'};
        }else if(points <= 24){
            return {'color': '#FFF176', 'font-size': 'large'};
        }else{
            return {'color': '#F48FB1', 'font-size': 'large'};
        }
    }
}
