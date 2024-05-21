import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VotingServiceService {

  environmentURL = "https://localhost:7231/";
  headers = new HttpHeaders({
    'content-type': ['application/json']
  });

  constructor(private readonly httpclient: HttpClient) { }

  GetData<T>(apiName: string, methodName: string): Observable<T>{
    return this.httpclient.get<T>(`${this.environmentURL}${apiName}/${methodName}`, {
      headers: this.headers,
    });
  }
}
