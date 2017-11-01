import { Component } from '@angular/core';
import 'rxjs/add/operator/catch'; 

import {IndexService} from '../../services/index.service'
import {IndexModel} from '../../models/index.model'

@Component({
    selector: 'index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css'],
    providers: [IndexService]
})
export class IndexComponent {
    loading = require('../../assets/loading.gif');
    showLoading = false;
    showResutl = false;
    infoMessage = false;
    noticeIndex = false;
    noticeClean = false;
    newIndex: IndexModel;
    recibedIndex: IndexModel;
    errorMessage: string;

    constructor(private indexService: IndexService){
        this.newIndex = new IndexModel();
    }

    public addIndex(newIndex: IndexModel){
        console.log(JSON.stringify(newIndex));
        this.showLoading = true;
        this.showResutl = false;
        this.errorMessage = '';
        this.indexService.create(newIndex)
        .subscribe(index=>{
            this.recibedIndex = index;
            this.showLoading = false;
            this.infoMessage = (index.indexes == null || index.indexes == 0);
            this.noticeIndex = !this.infoMessage;
            this.noticeClean = false;
            this.showResutl = true;
        },
        error=>{
            this.errorMessage = error;
            this.showLoading = false;
            this.showResutl = true;
        });
    }

    public clear(){
        this.showLoading = true;
        this.showResutl = false;
        this.errorMessage = '';
        this.indexService.delete()
        .subscribe(index=>{
            this.recibedIndex = index;
            this.showLoading = false;
            this.noticeIndex = false;
            this.noticeClean = true;
            this.infoMessage = false;
            this.showResutl = true;
        },
        error=>{
            this.errorMessage = error;
            this.showLoading = false;
            this.showResutl = true;
        });
    }
}
