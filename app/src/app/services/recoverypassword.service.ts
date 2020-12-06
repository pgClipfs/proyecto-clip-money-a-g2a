import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recoverypassword } from '../models/recoverypassword.model';
import { Login } from '../models/login.model';
import { TokenStorageService } from './token-storage.service';

const url= "https://localhost:44386/api/recoverypassword/";

const httpOptions ={
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class RecoverypasswordService {

  constructor(private http: HttpClient, private tokenStorage: TokenStorageService) {
    console.log('recoverypassword service is running');
   }

   public modificarPassword(np: Recoverypassword): Observable<any>{

    return this.http.post(url + 'newpassword', np, httpOptions);
   }
}
