import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cuenta } from '../models/cuenta.model';
import { TokenStorageService } from './token-storage.service';

const url = 'https://localhost:44386/api/CuentaVirtual/cliente?idCliente=';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class CuentasService {
  list: Cuenta[];
  idCliente : number;

  constructor(private http: HttpClient, private tokenService: TokenStorageService) { }

  getCuentas(): Observable<any>{
    this.idCliente = this.tokenService.getIdClient();
     return this.http.get<any>(url + this.idCliente, httpOptions);
  }

}
