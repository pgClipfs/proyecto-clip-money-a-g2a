import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';
import { identifierModuleUrl } from '@angular/compiler';
import { TokenStorageService } from './token-storage.service';

const url = 'https://localhost:44386/api/cliente/';

const httpOptions =
{
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable
(
    {
        providedIn: 'root'
    }
)

export class ClienteService
{
    list: Cliente[];
    idCliente: number;

    constructor(private http: HttpClient, private tokenService: TokenStorageService)
    {

    }
	getClientes(): Observable<Cliente[]>
    {
        return this.http.get<Cliente[]>(url, httpOptions);
    }
    
    getCliente(): Observable<any>
    {
        this.idCliente = this.tokenService.getIdClient();
        return this.http.get<any>(url + this.idCliente, httpOptions);
	}

    onCreateCliente(cliente: Cliente): Observable<any>
    {
        return this.http.post( url , cliente, httpOptions);
	}

	onUpdateCliente(cliente: Cliente): Observable<any>
	{
      return this.http.put(url, cliente, httpOptions);
	}

}