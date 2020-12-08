import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Localidades } from '../models/localidades.model';

const url = 'https://localhost:44386/api/localidad/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class LocalidadesService {

  list: Localidades[];
  constructor(private http: HttpClient) { }

  getLocalidades(): Observable<Localidades[]>{
    return this.http.get<Localidades[]>(url, httpOptions);
 }
}

