import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { Cuentadetallada } from 'src/app/models/cuentadetallada';
import { ClienteService } from 'src/app/services/cliente.service';
import { CuentasService } from 'src/app/services/cuentas.service';

@Component({
  selector: 'app-detalle-cuenta',
  templateUrl: './detalle-cuenta.component.html',
  styleUrls: ['./detalle-cuenta.component.css']
})
export class DetalleCuentaComponent implements OnInit {
  idCuenta: number;
  cuentaDetallada: Cuentadetallada;
  alias: string;
  cvu: string;
  nrocuenta: string;
  tipoCuenta: string;
  nombreTipoCuenta: string;
  idEstado: number;
  estado = 'INDEFINIDO';
  saldo: number;
  cliente: Cliente;
  nombre:string;
  apellido: string;
  dni:string;
  

  constructor(private cuentasService: CuentasService,private router: Router,private clienteService: ClienteService) { }

  ngOnInit(): void {
    //SUSCRIBIMOS AL SERVICIO DE LA CUENTA Y OBTENEMOS LA CUENTA DETALLADA DEL CLIENTE
    this.cuentasService.getCuentaDetallada().subscribe(
      (data: Cuentadetallada) =>{
        this.cuentaDetallada = data;
        this.alias = this.cuentaDetallada['Alias']
        this.cvu = this.cuentaDetallada['Cvu'];
        this.nrocuenta = this.cuentaDetallada['NroCuenta'];
        this.saldo = this.cuentaDetallada['Saldo'];
        this.tipoCuenta = this.cuentaDetallada['IdTipoCuenta'];
        this.nombreTipoCuenta = this.tipoCuenta['Nombre'];
        this.idEstado = this.cuentaDetallada['IdEstado'];
        console.log('la cuentadetallada trae: ', this.cuentaDetallada)

        if(this.idEstado == 1){
          this.estado = 'Activa'
        }
        else if (this.idEstado == 2){
          this.estado = 'Inactiva'
        }
        console.log(this.cuentaDetallada);
      },
      err =>{
        console.log('ERROR AL CARGAR LA CUENTA DETALLADA');
      }
    )

    //SUSCRIBIMOS PARA OBTENER DATOS DEL CLIENTE
    this.clienteService.getCliente().subscribe(
      (data : Cliente) => {
      
        this.cliente = data;
        this.nombre = this.cliente['Nombre'];
        this.apellido = this.cliente['Apellido'];
        this.dni = this.cliente['NumDni'];

        console.log(this.cliente);
        
      },
      err => {
       
      }
    );
    
    
    

  }

}
