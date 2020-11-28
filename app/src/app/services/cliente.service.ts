import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../cliente.model';
import { identifierModuleUrl } from '@angular/compiler';
@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  url="https://localhost:44386//api/cliente";
  list:Cliente[];

  constructor(private http:HttpClient) {
    console.log("clientes service is running");
   }
   getClientes():Observable<Cliente[]>{
     let header = new HttpHeaders().set('Content-Type', 'application/json');
     // let token = va el token aca
      return this.http.get<Cliente[]>(this.url, {headers:header});
   }
   // 
   onCreateCliente(cliente:Cliente):Observable<any>{
    let header = new HttpHeaders().set('Content-Type', 'application/json');
    // let token = va el token aca
     return this.http.post<Cliente>(this.url ,cliente, {headers:header});

  }
}
