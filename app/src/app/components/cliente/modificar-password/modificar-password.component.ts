import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { ModificarPassword } from 'src/app/models/modificar-password.model';
import { ClienteService } from 'src/app/services/cliente.service';
import { RecoverypasswordService } from 'src/app/services/recoverypassword.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-modificar-password',
  templateUrl: './modificar-password.component.html',
  styleUrls: ['./modificar-password.component.css']
})
export class ModificarPasswordComponent implements OnInit {

  private patternPassword: any = /^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])([^\s]){8,16}$/;

  idCliente: number;
  isCreateFailed = false;
  message = '';
  selectedCliente: Cliente = new Cliente();
  selectedModificarPass: ModificarPassword = new ModificarPassword();

  form = this.fb.group
  (
    {
      actualpassword: ['', [Validators.required, Validators.pattern(this.patternPassword), Validators.minLength(8)]],
      password: ['', [Validators.required, Validators.pattern(this.patternPassword), Validators.minLength(8)]],
      passwordRepeat: ['', [Validators.required, Validators.pattern(this.patternPassword), Validators.minLength(8)]],
    }
  )

  constructor(private clienteService: ClienteService, private tokenStorage: TokenStorageService, private fb: FormBuilder, private recoveryPass: RecoverypasswordService, private router: Router) { }

  ngOnInit(): void {

    this.idCliente=this.tokenStorage.getIdClient();
    this.selectedModificarPass.idCliente = this.idCliente;

    //Obtenemos todos los datos del cliente logueado
    this.clienteService.getCliente().subscribe
    (
      (data : Cliente) =>
      {
        //this.selectedCliente.Password = data.Password;
        this.selectedCliente.SelfieCliente = data.SelfieCliente;
      })

  }

  public onSubmit(modificarPass: ModificarPassword)
  {
  
    
    if(this.form.get('password').value === this.form.get('passwordRepeat').value){
      
      if (this.form.get('password').value !== this.form.get('actualpassword').value){
        this.recoveryPass.modificarPassdesdeApp(modificarPass).subscribe( data =>{
            if(data == 1){
              swal.fire('Enhorabuena', 'El Password fue actualizado con exito', 'success');
              this.limpiarForm();
              this.router.navigate(['/home']);
            }
            else if(data == 2){
              swal.fire('Ups', 'Password actual erronea', 'warning');
            }
            else if(data == 3){
              swal.fire('Ups', 'El password actual es igual al password nuevo ingresado', 'warning');
            }
            else if(data == 4){
              swal.fire('Ups', 'Ha ocurrido un error inespedado', 'warning');
            }
          })
      
        }else{
        swal.fire('Ups', 'El password actual es igual al password nuevo ingresado', 'warning');
      }
    
    }else{
      swal.fire('Ups', 'El password nuevo y el password repetido no coinciden', 'warning');
    }
  }

  limpiarForm(){
    this.form.patchValue
    (
      {
        actualpassword: '',
        password: '',
        passwordRepeat: ''
      }
    );
  }
}
