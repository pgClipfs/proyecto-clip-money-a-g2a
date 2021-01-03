import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { Cuenta } from 'src/app/models/cuenta.model';
import { Dni } from 'src/app/models/dni.model';
import { Operacion } from 'src/app/models/operacion.model';
import { ClienteService } from 'src/app/services/cliente.service';
import { CuentasService } from 'src/app/services/cuentas.service';
import { DniService } from 'src/app/services/dni.service';
import { OperacionesService } from 'src/app/services/operaciones.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import swal from 'sweetalert2';
declare const validarExtension:any;

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {

  public fotosDni: Dni = new Dni();
  cuentaValida : number;
  isAcountValid = false;
  valorInputSelfie = '';
  valorInputFrente = '';
  valorInputDorso = '';
  cuenta: Cuenta[];
  idCuenta: number;
  nroCuenta:string;
  saldo: number;
  tipoCuenta: string;
  nombreTipoCuenta: string;
  operaciones: Operacion[];
  email: string;
  //selectedOp: Operacion =new Operacion();
  
form = this.fb.group({
  inputSelfie: ['', [Validators.required]], 
  inputfrenteDni: ['', [Validators.required]],
  inputdorsoDni: ['', [Validators.required]]

})

  constructor(private clienteService: ClienteService, private dniService: DniService, private cuentasService: CuentasService, private opServices: OperacionesService, private tokenStorage: TokenStorageService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    var idUser = this.tokenStorage.getIdClient();
    this.fotosDni.id = idUser

    //Verificar si el cliente tiene la cuenta activa
    this.clienteService.getCliente().subscribe(
      (data : Cliente) => {
        this.email = data.Email;
        this.cuentaValida = data.CuentaValida;
        if (this.cuentaValida == 1){
          this.isAcountValid = true;
        }else{
          this.isAcountValid = false;
        }
        
      },
      err => {
      
       
      }
    );

    //Obtener las cuentas del cliente
    this.cuentasService.getCuentas().subscribe(
      (data: Cuenta) => {
        if(Object.entries(data).length !== 0)
        {
          this.cuenta = data[0];
          this.idCuenta = this.cuenta['Id']
          this.nroCuenta = this.cuenta['NroCuenta'];
          this.saldo = this.cuenta['Saldo'];
          this.tipoCuenta = this.cuenta['IdTipoCuenta'];
          this.nombreTipoCuenta = this.tipoCuenta['Nombre'];


          //obtenemos las ultimas 10 operaciones de la cuenta
          this.opServices.getTop10Op(this.idCuenta).subscribe(
            data =>{
              if(Object.entries(data).length !== 0)
              {
                this.operaciones = data;
              }else{
                
              }

              
            },
            err =>{

            }
          )

        }else{
          this.nroCuenta = '---';
          this.saldo = 0;
          this.nombreTipoCuenta = '---';
        }

      
      },
      err =>{
        this.nroCuenta = '---';
        this.saldo = 0;
        this.nombreTipoCuenta = '---';
      }
    )
    
  }


  onSubmit(){
    this.fotosDni.selfieCliente = this.selfie
    this.fotosDni.fotoFrenteDni = this.frenteDni;
    this.fotosDni.fotoDorsoDni = this.dorsoDni;
    this.fotosDni.email = this.email;

    this.dniService.onCreateFotosDni(this.fotosDni).subscribe(
      data => {
        if (data){
          swal.fire('Has verificado tu identidad', 'Tu cuenta ha sido activada con exito', 'success');
          this.router.navigate(['/home/inicio']);
        }else{
          swal.fire('Ups', 'Fallo en la verificacion de tu identidad', 'error');
        }
    })



  }

  //Metodo previsualizar fotos en el inicio
  frenteDni = "./assets/images/SubirFoto.png"
  dorsoDni = "./assets/images/SubirFoto.png"
  selfie = "./assets/images/SubirFoto.png"
  onSelectFile(event, type ): void{

    if(this.validar(type))
    {
      if (event.target.files){
        var reader = new FileReader;
        if (type == 0){
          reader.readAsDataURL(event.target.files[0]);
          reader.onload = (event: any) => {
            this.selfie = event.target.result;
          };
        }else if (type == 1){
          reader.readAsDataURL(event.target.files[0]);
          reader.onload = (event: any) => {
            this.frenteDni = event.target.result;
          };
        }else{
          reader.readAsDataURL(event.target.files[0]);
          reader.onload = (event: any) => {
            this.dorsoDni = event.target.result;
          };
        }   
        
      }
    }
    else{
      if(type == 0)
      {
        this.valorInputSelfie='';
        swal.fire('Formato invalido', 'Solo se permite imagenes .jpg y .png', 'warning');
  
      }else if (type == 1)
      {
        this.valorInputFrente='';
        swal.fire('Formato invalido', 'Solo se permite imagenes .jpg y .png', 'warning');
      }
      else if (type == 2)
      {
        this.valorInputDorso='';
        swal.fire('Formato invalido', 'Solo se permite imagenes .jpg y .png', 'warning');
      }
      
    }

 
  }

 onDetallesCuenta(){
  this.router.navigate(['home/cuentas/detalle-cuenta']);
 }

  validar(id: any): boolean{
     return validarExtension(id);
  }

}
