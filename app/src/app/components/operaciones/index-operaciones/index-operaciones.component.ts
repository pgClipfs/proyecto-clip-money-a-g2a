import { Component, OnInit } from '@angular/core';
import { observable } from 'rxjs';
import { Cliente } from 'src/app/models/cliente.model';
import { Cuenta } from 'src/app/models/cuenta.model';
import { Deposito } from 'src/app/models/deposito.model';
import { Extraccion } from 'src/app/models/extraccion.model';
import { Transferencia } from 'src/app/models/transferencia.model';
import { CuentasService } from 'src/app/services/cuentas.service';
import { OperacionesService } from 'src/app/services/operaciones.service';

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
  }

  habilitarExtraccion() : void{
    this.seccionExtraccion = true;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = false;
  }

  habilitarTransferencia() : void{
    this.seccionExtraccion = false;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
    this.seccionTransferencia = true;
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
    this.isOperationFailed = false;
    this.isOperationOK = false;
    let deposito = new Deposito();
    deposito.id_cuenta_virtual = this.form.cuenta.Id;
    deposito.monto = this.form.monto;
      this.operacionesService.depositar(deposito).subscribe(data => {
        console.log(data);
        if(data === 1){
          this.isOperationFailed = true;
          this.isOperationOK = false;
          this.mensaje = "El monto ingresado no es valido";
        }
        if(data === 2){
          this.isOperationFailed = true;
          this.isOperationOK = false;
          this.mensaje = "No podemos procesar el ingreso de dinero en estos momentos. Intente mas tarde.";
        }
        if(data === 0){
          this.isOperationFailed = false;
          this.isOperationOK = true;
          this.mensaje = "¡El ingreso de $"+ deposito.monto +" se proceso correctamente!";
          this.resetCamposDeposito();
        }
      });
  }

  extraer() : void {
    this.isOperationFailed = false;
    this.isOperationOK = false;
    let extraccion = new Extraccion();

    extraccion.id_cuenta_virtual = this.form.cuenta.Id;
    extraccion.monto = this.form.monto;

    this.operacionesService.extraer(extraccion).subscribe(data => {
      console.log(data);
      if(data === 1){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "El monto ingresado no es valido";
      }
      if(data === 2){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "No podemos procesar el retiro de dinero en estos momentos. Intente mas tarde.";
      }
      if(data === 3){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "No posee fondos suficientes para realizar el retiro de dinero solicitado.";
      }
      if(data === 0){
        this.isOperationFailed = false;
        this.isOperationOK = true;
        this.mensaje = "¡El retiro de $"+ extraccion.monto +" se proceso correctamente!";
        this.resetCamposDeposito();
      }
    });
  }

  transferir() : void
  {
    this.isOperationFailed = false;
    this.isOperationOK = false;
    let transferencia = new Transferencia();

    transferencia.id_cuenta_virtual = this.form.cuenta.Id;
    transferencia.monto = this.form.monto;
    transferencia.alias = this.form.alias;

    this.operacionesService.transferir(transferencia).subscribe(data => {
      console.log(data)
      if(data === 1){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "El monto ingresado no es valido";
      }
      if(data === 2){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "No podemos procesar la transferencia de dinero en estos momentos. Intente mas tarde.";
      }
      if(data === 3){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "No posee saldo suficiente para realizar la transferencia.";
      }
      if(data === 4){
        this.isOperationFailed = true;
        this.isOperationOK = false;
        this.mensaje = "No se puede transferir saldo a su misma cuenta.";
      }
      if(data === 0){
        this.isOperationFailed = false;
        this.isOperationOK = true;
        this.mensaje = "¡El retiro de $"+ transferencia.monto +" se proceso correctamente!";
        this.resetCamposDeposito();
      }
    });
  }

  resetCamposDeposito() : void {
    this.obtenerCuentasDeCliente();
    this.form.monto = "";
  }

}
