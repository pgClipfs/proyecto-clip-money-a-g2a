import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Recoverypassword } from 'src/app/models/recoverypassword.model';
import {RecoverypasswordService} from '../../services/recoverypassword.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import jwtDecode, * as JWT from 'jwt-decode';
import { FormControl, FormGroup, Validators , FormBuilder} from '@angular/forms';
import { range } from 'rxjs';
import swal from 'sweetalert2'; // IMPORTANTE HACER EL NPM INSTALL --SAVE SWEETALERT2 PARA LAS ALERTAS

@Component({
  selector: 'app-recoverypassword',
  templateUrl: './recoverypassword.component.html',
  styleUrls: ['./recoverypassword.component.css']
})
export class RecoverypasswordComponent implements OnInit {

  // Modelo de validaciones
  // contraseña de 8 caracters min, 1 mayus 1 min
passpattern: any = /^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])([^\s]){8,16}$/;

form = this.fb.group({
  inputToken: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(40)]],
  inputPassword1: ['', [Validators.required, Validators.pattern(this.passpattern), Validators.minLength(8)]],
  inputPassword2: ['', [Validators.required, Validators.pattern(this.passpattern), Validators.minLength(8)]]
})

  nombreEmail: string;
  public newpassword: Recoverypassword[];
  selectednewpassword: Recoverypassword = new Recoverypassword();
  tokenExpirado = false;
  passwordsDistintas = false;
  errorMessage = '';


constructor(private recoverypassword: RecoverypasswordService, private tokenStorage: TokenStorageService,
  private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.nombreEmail = this.tokenStorage.getEmail();
    if (this.nombreEmail !== null){

      this.selectednewpassword.email = this.nombreEmail;

    }else
    {
      this.router.navigate(['/login']);
    }
  }

  onSubmit(): void {

    if (this.form.get('inputPassword1').value === this.form.get('inputPassword2').value){
    this.passwordsDistintas = false;

    this.recoverypassword.modificarPassword(this.selectednewpassword).subscribe(
      data => {

        swal.fire('Enhorabuena', data, 'success');
        this.tokenExpirado = false;
        this.tokenStorage.logOut();
        this.router.navigate(['/login']);
      },
      err => {
        this.errorMessage = 'El token es invalido o ha expirado';
        this.tokenExpirado = true;
      }
    );
  }else{
    this.passwordsDistintas = true;
    this.errorMessage = 'Las Constraseñas no coinciden';

  }
}

public regresarclick() {
  // this.tokenStorage.logOut();
  this.router.navigate(['/login']);
}
}
