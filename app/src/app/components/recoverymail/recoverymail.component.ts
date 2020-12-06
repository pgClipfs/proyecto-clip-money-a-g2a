import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Recoverymail } from 'src/app/models/recoverymail.model';
import {RecoverymailService} from '../../services/recoverymail.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import jwtDecode, * as JWT from 'jwt-decode';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-recoverymail',
  templateUrl: './recoverymail.component.html',
  styleUrls: ['./recoverymail.component.css']
})
export class RecoveryMailComponent implements OnInit {

  public email: Recoverymail[];
  selectedEmail: Recoverymail = new Recoverymail();
  noExisteEmail = false;
  errorMessage = '';

 /*Validaciones del form*/ 
 emailpattern: any = /^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$/;  
 form = new FormGroup({
  emailValue: new FormControl('', [Validators.required, Validators.minLength(1), Validators.pattern(this.emailpattern)])
 })
 

  constructor(private recoveryMail: RecoverymailService, private tokenStorage: TokenStorageService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.recoveryMail.getEmail(this.selectedEmail).subscribe(
      data => {
        this.tokenStorage.saveEmail(data);
        this.noExisteEmail = false;
        this.router.navigate(['/recoverypassword']);
      },
      err => {
        this.errorMessage = "El Email no pertenece a nuestra cartera de clientes";
        this.noExisteEmail = true;
        this.onReset();
      }
    );
  }

  public regresarclick() {
    this.router.navigate(['/login']);
  }

  onReset(): void{
    this.form.reset();
  }

}
