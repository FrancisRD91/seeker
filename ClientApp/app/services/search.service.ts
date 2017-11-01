import { Injectable } from "@angular/core";  
import { Http, Response, Headers } from "@angular/http"; 

import { Observable } from 'rxjs/Observable';
import "rxjs/add/operator/map";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { SearchModel } from "../models/search.model"; 

@Injectable()
export class SearchService{
    private baseUrl: string;

    constructor(private http:Http){
        this.baseUrl = '/api/';
    }

    public search(search: SearchModel): Observable<SearchModel[]>{
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        
        return this.http.post(`${this.baseUrl}search`,JSON.stringify(search),{headers:headers})
        .map(response => <SearchModel[]> response.json())
        .catch(this.handleError)
    }

    private handleError(error: Response): Observable<any> {
        console.log(error);
        return Observable.throw(error.json().error || 'Internal server error');
    }
}