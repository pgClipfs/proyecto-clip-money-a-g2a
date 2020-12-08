import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tipodni } from '../models/tipodni.model';

const url = 'https://localhost:44386/api/tipodni/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class TipoDniService {

  constructor(private http: HttpClient) { }

  getTipoDni(): Observable<Tipodni[]>{
    return this.http.get<Tipodni[]>(url, httpOptions);
 }
}
