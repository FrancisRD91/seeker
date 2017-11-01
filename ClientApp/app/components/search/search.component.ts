import { Component } from '@angular/core';
import 'rxjs/add/operator/catch';

import {SearchService} from '../../services/search.service'
import {SearchModel} from '../../models/search.model'

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls:['./search.component.css'],
    providers: [SearchService]
})
export class SearchComponent {
    loading = require('../../assets/loading.gif');
    showLoading = false;
    showResult = false;
    newSearch: SearchModel;
    results: SearchModel[];
    resultMessage = '';

    constructor(private searchService: SearchService){
        this.newSearch = new SearchModel();
    }

    public search(newSearch: SearchModel) {
        console.log(JSON.stringify(newSearch));
        this.showLoading = true;
        this.showResult = false;
        this.searchService.search(newSearch)
        .subscribe(result=>{
            this.results = result;
            if(!result.length)
                this.resultMessage = "Not match found"
            else
                this.resultMessage = `Found ${result.length} results`;
            this.showLoading = false;
            this.showResult = true;
        },error=>{
            this.resultMessage = error;
            this.showLoading = false;
            this.showResult = true;
        })
    }
}
