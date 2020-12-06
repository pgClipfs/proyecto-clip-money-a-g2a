import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { ClienteService } from '../../services/cliente.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})

export class ClienteComponent implements OnInit {

  //Modelo validacion campos
private patterSoloLetras: any = /^[a-zA-Z ]*$/;
private patternUsuario: any = /^[a-zA-Z ].{4,}/;
private emailpattern: any = /^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$/;
private passpattern: any = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{7,}/;
private patternSoloNum: any = /^([0-9]).{6,}/;

form = this.fb.group({
  nombre: ['', [Validators.required, Validators.minLength(2), Validators.pattern(this.patterSoloLetras)]],
  apellido: ['', [Validators.required, Validators.minLength(2), Validators.pattern(this.patterSoloLetras)]],
  fechaNac: ['', [Validators.required]],
  numDni: ['', [Validators.required, Validators.minLength(7), Validators.pattern(this.patternSoloNum)]],
  email: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.emailpattern)]],
  usuario: ['',  [Validators.required, Validators.minLength(5), Validators.pattern(this.patternUsuario)]],
  password: ['', [Validators.required, Validators.pattern(this.passpattern), Validators.minLength(8)]],
  passwordRepeat: ['', [Validators.required, Validators.pattern(this.passpattern), Validators.minLength(8)]],
})
//---

  public clientes: Cliente[];
  selectedCliente: Cliente = new Cliente();
  message = '';
  isCreateFailed = false;
  passwordsDistintas = false;


  constructor(private clienteService: ClienteService, private tokenStorage: TokenStorageService, private router: Router, private fb: FormBuilder) {


  }

  ngOnInit(): void {
    this.clienteService.getClientes().subscribe(resp => {
      console.log(resp);
      this.clientes = resp;
    })
    if (this.tokenStorage.getToken()) {
      this.router.navigate(['/home']);
    }

  }

  public regresarclick() {
    this.router.navigate(['/login']);
  }
  public onSubmit(cliente: Cliente) {

    if (this.form.get('password').value === this.form.get('passwordRepeat').value){
      this.passwordsDistintas = false;
      console.log('Contraseñas iguales');
      
      this.clienteService.onCreateCliente(cliente).subscribe(
        data => {
        //this.tokenStorage.saveToken(data);

        if (data === 1)
        {
          this.message = 'El Dni ingresado ya existe';
          this.isCreateFailed = true;
          console.log('usuario existente')
        }
        else if (data === 2)
        {
          this.message = 'El Email ingresado ya existe';
          this.isCreateFailed = true;
        }
        else if (data === 3)
        {
          this.message = 'El Usuario ingresado ya existe';
          this.isCreateFailed = true;
        }
        else if (data === 0)
        {
          console.log('cliente agregado exitosamente');
          this.isCreateFailed = false;
          //this.clientes.push(data);
          this.router.navigate(['/login']);
        }
      });
  }else{
    this.passwordsDistintas = true;
    this.message = "Las Constraseñas no coinciden";
    console.log('Contraseñas no coinciden');
  }
}

  public onSelect (item: Cliente) {
    this.selectedCliente = item;
  }
}
