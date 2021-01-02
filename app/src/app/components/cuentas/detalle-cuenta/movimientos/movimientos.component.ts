import { Component, OnInit } from '@angular/core';
import { Operacion } from 'src/app/models/operacion.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import swal from 'sweetalert2';//IMPORTANTE HACER EL NPM INSTALL --SAVE SWEETALERT2 PARA LAS ALERTAS
import { getLocaleDateFormat } from '@angular/common';
import { OperacionesService } from 'src/app/services/operaciones.service';
import { CuentasService } from 'src/app/services/cuentas.service';
import { Cuentadetallada } from 'src/app/models/cuentadetallada';

@Component({
  selector: 'app-movimientos',
  templateUrl: './movimientos.component.html',
  styleUrls: ['./movimientos.component.css']
})
export class MovimientosComponent implements OnInit {
  operaciones: Operacion[];
  form = this.fb.group({
   fechadesde:['', [Validators.required]],
   fechahasta:['', [Validators.required]],
   concepto: ['', [Validators.required]],
  })

  fechadesde: string;
  fechahasta: string;
  concepto: number;
  idCuenta: number;
  nroCuenta: string;
  cuentaDetallada: Cuentadetallada;

  filtros: any = {};

  constructor(private fb: FormBuilder, private operacionesServices: OperacionesService, private cuentasService: CuentasService) { }

  ngOnInit(): void {
     //SUSCRIBIMOS AL SERVICIO DE LA CUENTA Y OBTENEMOS LA CUENTA DETALLADA DEL CLIENTE
     this.cuentasService.getCuentaDetallada().subscribe(
      (data: Cuentadetallada) =>{
        this.cuentaDetallada = data;
        this.idCuenta = this.cuentaDetallada['Id'];
        this.nroCuenta = this.cuentaDetallada['NroCuenta'];
      },
      err =>{
        console.log('ERROR AL CARGAR LA CUENTA DETALLADA');
      }
    )
  }

    //Metodo al enviar el fomulario
    public onSubmit() {
      if(this.form.valid){
        this.fechadesde = this.filtros.fechaDesde;
        this.fechadesde = this.convertDateFormat(this.fechadesde);
        this.fechahasta = this.filtros.fechaHasta;
        this.fechahasta = this.convertDateFormat(this.fechahasta);
        this.concepto = this.filtros.concepto;

        if(this.filtros.fechaDesde > this.filtros.fechaHasta){
          
          swal.fire('Ups', 'La fecha de comienzo no puede ser mayor a la final', 'warning');
        
        }else{
        
          this.operacionesServices.getTodasOp(this.idCuenta,this.fechadesde,this.fechahasta, this.concepto).subscribe(
            data =>{
              this.operaciones = data;

            },
            error =>{}
          )
        }
       


      }else
      {
        swal.fire('Ups', 'Seleccione el rango de fechas y el concepto para realizar la busqueda', 'warning');
      }
  }




// @param string (string) : Fecha en formato YYYY-MM-DD
// @return (string)       : Fecha en formato DD-MM-YYYY
 public convertDateFormat(fecha: string) {
  var fechaformat = fecha.split('-');
  return fechaformat[2] + '-' + fechaformat[1] + '-' + fechaformat[0];
}

}
