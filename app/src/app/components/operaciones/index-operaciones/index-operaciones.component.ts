import { Component, OnInit } from '@angular/core';
import { observable } from 'rxjs';
import { Cliente } from 'src/app/models/cliente.model';
import { Cuenta } from 'src/app/models/cuenta.model';
import { Deposito } from 'src/app/models/deposito.model';
import { Extraccion } from 'src/app/models/extraccion.model';
import { Giro } from 'src/app/models/giro.model';
import { Transferencia } from 'src/app/models/transferencia.model';
import { CuentasService } from 'src/app/services/cuentas.service';
import { OperacionesService } from 'src/app/services/operaciones.service';
import swal from 'sweetalert2';
import { threadId } from 'worker_threads';

@Component({
  selector: 'app-index-operaciones',
  templateUrl: './index-operaciones.component.html',
  styleUrls: ['./index-operaciones.component.css']
})
export class IndexOperacionesComponent implements OnInit {

  seccionDeposito = false;
  seccionExtraccion = false;
  seccionTransferencia = false;
  seccionBuscar = true;
  seccionGiro = false;
  cuentasDelCliente : Cuenta[] = [];
  cuentaDestino : Cliente[] = [];
  form: any = {};
  isOperationFailed = false;
  isOperationOK = false;
  mensaje = "";
  apellido = "";
  nombre = "";
  email = "";
  dni = "";
  montoPosibleGiro: any;
  isMontoGiroOk = false;
  isMontoGiroValid = false;

  constructor(private cuentasService: CuentasService,private operacionesService:OperacionesService) { }

  ngOnInit(): void {
    this.obtenerCuentasDeCliente();
  }

  habilitarDeposito() : void {
    this.seccionDeposito = true;
    this.seccionExtraccion = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = false;
    this.seccionGiro = false;
  }

  habilitarExtraccion() : void{
    this.seccionExtraccion = true;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = false;
    this.seccionGiro = false;
  }

  habilitarTransferencia() : void{
    this.seccionExtraccion = false;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = true;
    this.seccionGiro = false;
  }

  habilitarGiro() : void{
    this.seccionExtraccion = false;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = false;
    this.seccionGiro = true;

  }

  buscarCuentaDestino(){
    this.seccionBuscar = true;
    this.operacionesService.obtenerCuentaDestino(this.form.alias).subscribe((data: Cliente) =>{
      if(data != null){
      this.apellido = data.Apellido;
      this.nombre = data.Nombre;
      this.email = data.Email;
      this.dni = data.NumDni;
      console.log(data);
      console.log(this.email);}
      else{
        alert("no existe cuenta");
      }
    }
    )
  }

  obtenerCuentasDeCliente() : void {
    this.cuentasService.getCuentas().subscribe(data => {
      this.cuentasDelCliente = data;
    });
  }

  depositar() : void {   
    if (this.form.cuenta != undefined && this.form.monto != undefined)
    {
      let deposito = new Deposito();
      deposito.id_cuenta_virtual = this.form.cuenta.Id;
      deposito.monto = this.form.monto;

      this.operacionesService.depositar(deposito).subscribe(data => {
          if(data === 1){
            this.mensaje = "El monto ingresado no es valido";
            swal.fire('Ups', this.mensaje, 'warning');
          }
          if(data === 2){
            this.mensaje = "No podemos procesar el ingreso de dinero en estos momentos. Intente mas tarde.";
            swal.fire('Ups', this.mensaje, 'warning');
          }
          if(data === 0){
            this.mensaje = "¡El ingreso de $"+ deposito.monto +" se proceso correctamente!";
            swal.fire('Enhorabuena', this.mensaje, 'success');
            this.resetCamposDeposito();
          }
        });
    }else
    {
      {
        swal.fire('Campos vacios', 'Por favor complete todo los campos', 'warning');
      }
    }
  }

  extraer() : void {
    
    if (this.form.cuenta != undefined && this.form.monto != undefined)
    {
      let extraccion = new Extraccion();
      extraccion.id_cuenta_virtual = this.form.cuenta.Id;
      extraccion.monto = this.form.monto;

      this.operacionesService.extraer(extraccion).subscribe(data => {
        if(data === 1){
          this.mensaje = "El monto ingresado no es valido";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 2){
          this.mensaje = "No podemos procesar el retiro de dinero en estos momentos. Intente mas tarde";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 3){
          this.mensaje = "No posee fondos suficientes para realizar el retiro de dinero solicitado";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 0){
          this.mensaje = "¡El retiro de $"+ extraccion.monto +" se proceso correctamente!";
          swal.fire('Enhorabuena', this.mensaje, 'success');
          this.resetCamposDeposito();
        }
      });
    }else
    {
      swal.fire('Campos vacios', 'Por favor complete todo los campos', 'warning');
    }
  }

  transferir() : void{


    if (this.form.cuenta != undefined && this.form.monto != undefined && this.form.alias != undefined)
    {
      let transferencia = new Transferencia();
      transferencia.id_cuenta_virtual = this.form.cuenta.Id;
      transferencia.monto = this.form.monto;
      transferencia.alias = this.form.alias;

      this.operacionesService.transferir(transferencia).subscribe(data => {
        if(data === 1){
          this.mensaje = "El monto ingresado no es valido";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 2){
          this.mensaje = "No podemos procesar la transferencia de dinero en estos momentos. Intente mas tarde";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 3){
          this.mensaje = "No posee saldo suficiente para realizar la transferencia.";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 4){
          this.mensaje = "No se puede transferir saldo a su misma cuenta.";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 0){
          this.mensaje = "¡La transferencia de $"+ transferencia.monto +" se proceso correctamente!";
          swal.fire('Enhorabuena', this.mensaje, 'success');
          this.resetCamposDeposito();
          this.form.alias = undefined;
        }
      });
    }else
    {
      swal.fire('Campos vacios', 'Por favor complete todo los campos', 'warning');
    }
  }

 girar(){
  if (this.form.cuenta != undefined && this.form.monto != undefined)
    {
      let giro = new Giro();
      giro.id_cuenta_virtual = this.form.cuenta.Id;
      giro.monto = this.form.monto;

      this.operacionesService.girar(giro).subscribe(data =>{
        if(data === 1){
          this.mensaje = "El monto a girar es mayor al  monto posible";
          swal.fire('Ups', this.mensaje, 'warning');
        }
        if(data === 2){
          this.mensaje = "Giro realizado correctamente";
          swal.fire('Enhorabuena', this.mensaje, 'success');
          this.form.cuenta = undefined;
          this.form.monto = undefined;
        }
        if(data === 3){
          this.mensaje = "No posee saldo suficiente para realizar el giro";
          swal.fire('Ups', this.mensaje, 'error');
        }
        if(data === 0){
          swal.fire('No se puede realizar esta accion', 'Para el monto deseado realize una extraccion comun', 'warning');
        }
      })

    }else
    {
      swal.fire('Campos vacios', 'Por favor complete todo los campos', 'warning');
    }
  }

  resetCamposDeposito() : void {
    this.obtenerCuentasDeCliente();
    this.form.monto = "";
  }

  //Cargar el monto al seleccionar la cuenta
  changeSelectCuenta(): void{
       //Llamamos el servicio para consultar el saldo posible para hacer el giro
       this.operacionesService.getMontoPosibleGiro(this.form.cuenta.Id).subscribe(data =>{
        if (data > 0)
        {
          this.montoPosibleGiro = data;
          this.isMontoGiroOk = true;
          this.isMontoGiroValid = true;
        }else
        {
          swal.fire('Ups', 'No tienes saldo suficiente para realizar el giro', 'error');
          this.form.cuenta = "";
          this.isMontoGiroOk = false;
          this.isMontoGiroValid = false;
        }
      
      })
  }

}
