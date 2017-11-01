import { Injectable } from "@angular/core";  
import { Http, Response, Headers } from "@angular/http"; 

import { Observable } from 'rxjs/Observable';
import "rxjs/add/operator/map";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { IndexModel } from "../models/index.model"; 

@Injectable()
export class IndexService{
    private baseUrl: string;
    public indexResponse: IndexModel;
    
    constructor(private http:Http){
        this.baseUrl = '/api/';
    }
    
    public create(index: IndexModel): Observable<IndexModel>{
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        
        return this.http.post(`${this.baseUrl}index`,JSON.stringify(index),{headers:headers})
        .map(response => <IndexModel> response.json())
        .catch(this.handleError)
    }
    
    public delete(): Observable<IndexModel>{
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        
        return this.http.delete(`${this.baseUrl}index`,{headers:headers})
        .map(response => <IndexModel> response.json())
        .catch(this.handleError)
    }

    private handleError(error: Response): Observable<any> {
        console.log(error);
        return Observable.throw(error.json().error || 'Internal server error');
    }
}