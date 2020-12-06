import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recoverymail } from '../models/recoverymail.model';

const url= "https://localhost:44386/api/recoverymail/";

const httpOptions ={
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class RecoverymailService {

  constructor(private http: HttpClient) {

   }

   public getEmail(email: Recoverymail) : Observable<any>{
    return this.http.post(url + 'validation', email, httpOptions);
  }
}
