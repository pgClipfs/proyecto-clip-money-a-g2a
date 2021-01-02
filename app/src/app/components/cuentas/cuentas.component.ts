import { Component, OnInit } from '@angular/core';
import { Cuenta } from 'src/app/models/cuenta.model';
import { CuentasService } from 'src/app/services/cuentas.service';
import { Router } from '@angular/router';
import { Cuentadetallada } from 'src/app/models/cuentadetallada';

@Component({
  selector: 'app-cuentas',
  templateUrl: './cuentas.component.html',
  styleUrls: ['./cuentas.component.css']
})
export class CuentasComponent implements OnInit {
  cuenta: Cuenta[];
  cuentaDetallada: Cuentadetallada;
  idCuenta: number;
  idCuentapasar: number;
  nroCuenta:string;
  saldo: number;
  tipoCuenta: string;
  nombreTipoCuenta: string;
  

  constructor(private cuentasService: CuentasService, private router: Router) { }

  ngOnInit(): void {

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

}
