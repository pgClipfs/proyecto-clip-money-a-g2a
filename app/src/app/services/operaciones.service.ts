import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Deposito } from '../models/deposito.model';
import { Extraccion } from '../models/extraccion.model';
import { Operacion } from '../models/operacion.model';
import { TokenStorageService } from './token-storage.service';

const url = 'https://localhost:44386/api/operaciones/top_diez?idCV=';
const urltodasop = 'https://localhost:44386/api/operaciones/movimientos?idCV=';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
  providedIn: 'root'
})
export class OperacionesService {
  list: Operacion[];
  idCuenta : number;

  constructor(private http: HttpClient, private tokenService: TokenStorageService) { }

  getTop10Op(idcuenta: number): Observable<any>{
     return this.http.get<any>(url + idcuenta, httpOptions);
  }

  depositar(deposito : Deposito) : Observable<any>{
    return this.http.post<any>("https://localhost:44386/api/operaciones/deposito",deposito,httpOptions);
  }

  extraer(extraccion : Extraccion) : Observable<any>{
    return this.http.post<any>("https://localhost:44386/api/operaciones/extraccion",extraccion,httpOptions);
  }

  getTodasOp(idcuenta: number, fechadesde: string, fechahasta:string, concepto: number){
    return this.http.get<any>(urltodasop + idcuenta +'&fechadesde='+fechadesde + '&fechahasta='+ fechahasta + '&concepto=' + concepto, httpOptions);
  }
}
