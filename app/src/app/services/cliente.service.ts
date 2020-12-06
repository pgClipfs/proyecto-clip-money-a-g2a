import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';
import { identifierModuleUrl } from '@angular/compiler';

const url= "https://localhost:44386/api/cliente/";
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };


@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  list: Cliente[];

  constructor(private http: HttpClient) {
    console.log('clientes service is running');
   }


   getClientes(): Observable<Cliente[]>{
      return this.http.get<Cliente[]>(url, httpOptions);
   }

   onCreateCliente(cliente: Cliente): Observable<any>{
    console.log('entra al metodo oncreatecliente');
    console.log(cliente);
    return this.http.post( url , cliente, httpOptions);

  }
}
