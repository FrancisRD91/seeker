import { Injectable } from "@angular/core";  
import { Http, Response, Headers } from "@angular/http"; 
import "rxjs/add/operator/map"; 

import { IndexModel } from "../models/index.model"; 

@Injectable()
export class IndexService{
    private baseUrl: string;
    public indexResponse: IndexModel;

    constructor(private http:Http){
        this.baseUrl = '/api/';
    }

    public create(index: IndexModel){
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.post(`${this.baseUrl}index`,JSON.stringify(index),{headers:headers})
        .map(response => response.json())
        .subscribe(data  => this.indexResponse = data,
                   error => console.log(error));
    }

    public delete(index: IndexModel){
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.delete(`${this.baseUrl}index`,{headers:headers})
        .map(response => response.json())
        .subscribe(data  => console.log(data),
                   error => console.log(error));
    }
}