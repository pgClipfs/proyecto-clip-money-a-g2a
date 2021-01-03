import { Component, OnInit } from '@angular/core';
import { Cuenta } from 'src/app/models/cuenta.model';
import { Deposito } from 'src/app/models/deposito.model';
import { Extraccion } from 'src/app/models/extraccion.model';
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
  cuentasDelCliente : Cuenta[] = [];
  form: any = {};
  isOperationFailed = false;
  isOperationOK = false;
  mensaje = "";

  constructor(private cuentasService: CuentasService,private operacionesService:OperacionesService) { }

  ngOnInit(): void {
    this.obtenerCuentasDeCliente();
  }

  habilitarDeposito() : void {
    this.seccionDeposito = true;
    this.seccionExtraccion = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
  }

  habilitarExtraccion() : void{
    this.seccionExtraccion = true;
    this.seccionDeposito = false;
    this.isOperationFailed = false;
    this.isOperationOK = false;
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

  resetCamposDeposito() : void {
    this.obtenerCuentasDeCliente();
    this.form.monto = "";
  }

}
