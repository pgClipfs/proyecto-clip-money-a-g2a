import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pais } from '../models/pais.model';

const url = 'https://localhost:44386/api/pais/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class PaisService {

  list: Pais[];

  constructor(private http: HttpClient) { }

  getPaises(): Observable<Pais[]>{
    return this.http.get<Pais[]>(url, httpOptions);
 }
}
