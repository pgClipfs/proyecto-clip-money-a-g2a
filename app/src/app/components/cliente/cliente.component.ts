import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { Pais } from 'src/app/models/pais.model';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { ClienteService } from '../../services/cliente.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import swal from 'sweetalert2';//IMPORTANTE HACER EL NPM INSTALL --SAVE SWEETALERT2 PARA LAS ALERTAS
import { PaisService } from 'src/app/services/pais.service';
import { ProvinciasService } from 'src/app/services/provincias.service';
import { LocalidadesService } from 'src/app/services/localidades.service';
import { Provincias } from 'src/app/models/provincias.model';
import { Localidades } from 'src/app/models/localidades.model';
import { Tipodni } from 'src/app/models/tipodni.model';
import { TipoDniService } from 'src/app/services/tipo-dni.service';


@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})

export class ClienteComponent implements OnInit {

  //Modelo validacion campos
private patterSoloLetras: any = /^[a-zA-Z ]*$/;
private patternUsuario: any = /^(?=.*[a-zA-Z]{1,})(?=.*[\d]{0,})[a-zA-Z0-9]{5,}$/;
private emailpattern: any = /^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$/;
private patternPassword: any = /^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])([^\s]){8,16}$/;
private patternSoloNum: any = /^[0-9]{7,}$/;
private patternTelefono: any = /^[0-9]{10,10}$/;
private patternDomicilio: any = /^[A-Za-z0-9\s]{5,50}$/;

form = this.fb.group({
  nombre: ['', [Validators.required, Validators.minLength(2), Validators.pattern(this.patterSoloLetras)]],
  apellido: ['', [Validators.required, Validators.minLength(2), Validators.pattern(this.patterSoloLetras)]],
  sexo: ['', [Validators.required]],
  fechaNac: ['', [Validators.required]],
  tipodni: ['', [Validators.required]],
  numDni: ['', [Validators.required, Validators.minLength(7), Validators.pattern(this.patternSoloNum)]],
  email: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.emailpattern)]],
  telefono: ['', [Validators.required, Validators.minLength(7), Validators.pattern(this.patternTelefono)]],
  pais: ['', [Validators.required]],
  provincia: ['', [Validators.required]],
  localidad: ['', [Validators.required]],
  domicilio: ['',  [Validators.required, Validators.minLength(5), Validators.pattern(this.patternDomicilio)]],
  usuario: ['',  [Validators.required, Validators.minLength(5), Validators.pattern(this.patternUsuario)]],
  password: ['', [Validators.required, Validators.pattern(this.patternPassword), Validators.minLength(8)]],
  passwordRepeat: ['', [Validators.required, Validators.pattern(this.patternPassword), Validators.minLength(8)]],
})
//---

  public clientes: Cliente[];
  selectedCliente: Cliente = new Cliente();
  public tipoDni: Tipodni[];
  public paises: Pais[];
  public provincias: Provincias[];
  public provinciasAux: Provincias[];
  public localidades: Localidades[];
  public localidadesAux: Localidades[];
  selectedPais: Pais = {id: 0, nombre: ''};
  selectedProv: Provincias = {id: 0, nombre: '', id_pais: 0};
  message = '';
  isCreateFailed = false;
  passwordsDistintas = false;


  constructor(private clienteService: ClienteService, private tipodniService: TipoDniService , private paisesService: PaisService,
    private provService: ProvinciasService, private localService: LocalidadesService, private tokenStorage: TokenStorageService,
    private router: Router, private fb: FormBuilder) { }

  ngOnInit(): void {
    //obtenemos la lista de clientes
    /*this.clienteService.getCliente().subscribe(resp => {
      this.clientes = resp;
    });*/
    
    if (this.tokenStorage.getToken()) {
      this.router.navigate(['/home']);
    }
    //obenemos la lista de tipos de dni
    this.tipodniService.getTipoDni().subscribe(
      data => {
        this.tipoDni = data;
      }
    );
    //obtenemos la lista de los paises
    this.paisesService.getPaises().subscribe(
      data => {
        this.paises = data;
      }
    );
    //obtenemos la lista de las provincias
    this.provService.getProvincias().subscribe(
      data => {
        this.provincias = data;
        //this.provinciasAux = data;
      }
    );
    
    //obtenemos la lista de las localidades
    this.localService.getLocalidades().subscribe(
      data => {
        this.localidades = data;
      }
    );

  }

  public regresarclick() {
    this.router.navigate(['/login']);
  }

  //Metodo al enviar el fomulario
  public onSubmit(cliente: Cliente) {

    if (this.form.get('password').value === this.form.get('passwordRepeat').value){
      this.passwordsDistintas = false;
      
      //Nos suscribimos al servicio y traemos el metodo del backend
      this.clienteService.onCreateCliente(cliente).subscribe(
        data => {
        //this.tokenStorage.saveToken(data);

        if (data === 1)
        {
          this.message = 'El Dni ingresado ya existe';
          this.isCreateFailed = true;
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
          swal.fire('Enhorabuena', 'Cliente registrado exitosamente', 'success');
          this.isCreateFailed = false;
          //this.clientes.push(data);
          this.router.navigate(['/login']);
        }
      });
  }else{
    this.passwordsDistintas = true;
    this.message = "Las constraseñas no coinciden";
  }
}

//Metodos para cargar los select filtrados por el id del select seleccionado(Pais-->Provincia-->Localidad)

 onSelectPais(id: number): void {
   this.limpiarForm();
    this.localidadesAux = null;
    this.provinciasAux = null;
    this.provinciasAux = this.provincias.filter(item => item.id_pais == id);
    
  }

  onSelectProv(id: number): void {
    this.localidadesAux = this.localidades.filter(item => item.id_provincia == id);
    
  }

  //Metodo para resetear las alerts
  resetAlert(){
    this.isCreateFailed = false;
    this.passwordsDistintas = false;
  }

  //Metodo para limpiar select al cambiar el target
  limpiarForm(){
    this.form.patchValue({
      provincia: '',
      localidad: ''

    });
  }
}
