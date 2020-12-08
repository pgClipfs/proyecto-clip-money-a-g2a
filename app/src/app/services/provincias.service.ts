import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, ObservableInput } from 'rxjs';
import { Provincias } from '../models/provincias.model';

const url = 'https://localhost:44386/api/provincia/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class ProvinciasService {
  list: Provincias[];

  constructor(private http: HttpClient) { }

  getProvincias(): Observable<Provincias[]>{
    return this.http.get<Provincias[]>(url, httpOptions);
 }
}
