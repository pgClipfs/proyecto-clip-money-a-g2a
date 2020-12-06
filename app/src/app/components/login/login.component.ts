import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import jwtDecode, * as JWT from 'jwt-decode';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

//Modelo validaciones

form = this.fb.group({
  inputUsuario: ['', [Validators.required, Validators.minLength(1)]],
  inputPassword: ['', [Validators.required,Validators.minLength(1)]] 
})

  usuario: any = {};
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService, private tokenStorage: TokenStorageService, private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['/home']);
    }
  }

  onSubmit(): void {
    this.authService.login(this.usuario).subscribe(
      data => {
        this.tokenStorage.saveToken(data);
        
        var decoded = jwtDecode(data);      
        this.tokenStorage.saveUser(decoded['unique_name']);

        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.router.navigate(['/home']);
      },
      err => {
        this.errorMessage = "¡Usuario y/o password invalidos!";
        this.isLoginFailed = true;
      }
    );
  }

}
