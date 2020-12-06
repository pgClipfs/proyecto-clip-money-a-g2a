import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../models/login.model';
import { TokenStorageService } from './token-storage.service';

const AUTH_API = 'https://localhost:44386/api/login/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,private tokenStorage:TokenStorageService) { }

  public login(usuario:Login) : Observable<any>{
    return this.http.post(AUTH_API + 'authenticate',usuario, httpOptions);
  }

  public estaAutenticado() : boolean{
    if (this.tokenStorage.getToken() != null && this.tokenStorage.getUser() != null ){
      return true;
    }else{
      return false;
    }
  }
}
