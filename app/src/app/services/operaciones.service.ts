import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Deposito } from '../models/deposito.model';
import { Extraccion } from '../models/extraccion.model';
import { Giro } from '../models/giro.model';
import { Operacion } from '../models/operacion.model';
import { Transferencia } from '../models/transferencia.model';
import { TokenStorageService } from './token-storage.service';

const url = 'https://localhost:44386/api/operaciones/top_diez?idCV=';
const urltodasop = 'https://localhost:44386/api/operaciones/movimientos?idCV=';
const urlCuentaDestino = 'https://localhost:44386/api/operaciones/transferencia?alias=';
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

  transferir(transferencia : Transferencia) : Observable<any>
  {
    return this.http.post<any>("https://localhost:44386/api/operaciones/transferencia",transferencia,httpOptions);
  }
  obtenerCuentaDestino(alias: string){
    return this.http.get<any>(urlCuentaDestino + alias , httpOptions);
  }
  
  girar(giro: Giro){
    return this.http.post<any>("https://localhost:44386/api/operaciones/giro",giro,httpOptions);
  }

  getMontoPosibleGiro(idCuenta: number): Observable<any>{
    return this.http.get<any>("https://localhost:44386/api/operaciones/montoPosibleGiro?idCuenta=" + idCuenta, httpOptions);
  }

  getTodasOp(idcuenta: number, fechadesde: string, fechahasta:string, concepto: number){
    return this.http.get<any>(urltodasop + idcuenta +'&fechadesde='+fechadesde + '&fechahasta='+ fechahasta + '&concepto=' + concepto, httpOptions);
  }
}
