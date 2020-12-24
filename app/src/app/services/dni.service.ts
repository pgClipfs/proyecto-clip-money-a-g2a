import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Dni } from '../models/dni.model';

const url = 'https://localhost:44386/api/dni/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class DniService {
  list: Dni[];

  constructor(private http: HttpClient) { }

  onCreateFotosDni(fotosDni: Dni): Observable<any>{
    return this.http.post( url , fotosDni, httpOptions);
  }
}
