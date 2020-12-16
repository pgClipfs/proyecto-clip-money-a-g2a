import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';
import { identifierModuleUrl } from '@angular/compiler';

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

    constructor(private http: HttpClient)
    {

    }

    getClientes(): Observable<Cliente[]>
    {
        return this.http.get<Cliente[]>(url, httpOptions);
    }

    onCreateCliente(cliente: Cliente): Observable<any>
    {
        return this.http.post(url , cliente, httpOptions);
    }

    onUpdateCliente(cliente: Cliente): Observable<any>
    {
        return this.http.put(url, cliente, httpOptions);
    }

    onGetCliente(cliente: Cliente): Observable<any>
    {
        return this.http.get(url, httpOptions);
    }
}
