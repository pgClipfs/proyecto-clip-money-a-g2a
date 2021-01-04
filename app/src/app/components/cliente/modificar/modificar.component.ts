import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { Pais } from 'src/app/models/pais.model';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { ClienteService } from '../../../services/cliente.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import swal from 'sweetalert2';//IMPORTANTE HACER EL NPM INSTALL --SAVE SWEETALERT2 PARA LAS ALERTAS
import { PaisService } from 'src/app/services/pais.service';
import { ProvinciasService } from 'src/app/services/provincias.service';
import { LocalidadesService } from 'src/app/services/localidades.service';
import { Provincias } from 'src/app/models/provincias.model';
import { Localidades } from 'src/app/models/localidades.model';
import { Tipodni } from 'src/app/models/tipodni.model';
import { TipoDniService } from 'src/app/services/tipo-dni.service';
import { Dni } from 'src/app/models/dni.model';

@Component
(
  {
    selector: 'app-modificar',
    templateUrl: './modificar.component.html',
    styleUrls: ['./modificar.component.css']
  }
)

export class ModificarComponent implements OnInit
{
  nombreUsuario : string;
  idCliente : number;
  nombre : string;
  apellido : string;
  sexo : string;
  fechaNacimiento : string;
  idTipoDni : number;
  numDni : string;
  email : string;
  telefono : string;
  idLocalidad : number;
  domicilio : string;
  password : string;
  selfie : string;
  fotoUsuario : string;
  
  //Modelo validación campos
  private patterSoloLetras: any = /^[a-zA-Z ]*$/;
  private patternUsuario: any = /^(?=.*[a-zA-Z]{1,})(?=.*[\d]{0,})[a-zA-Z0-9]{5,}$/;
  private emailpattern: any = /^(([^<>()\[\]\\.,;:\s@”]+(\.[^<>()\[\]\\.,;:\s@”]+)*)|(“.+”))@((\[[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}\.[0–9]{1,3}])|(([a-zA-Z\-0–9]+\.)+[a-zA-Z]{2,}))$/;
  private patternPassword: any = /^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])([^\s]){8,16}$/;
  private patternSoloNum: any = /^[0-9]{7,}$/;
  private patternTelefono: any = /^[0-9]{10,10}$/;
  private patternDomicilio: any = /^[A-Za-z0-9\s]{5,50}$/;

  form = this.fb.group
  (
    {
      fechaNac: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.emailpattern)]],
      telefono: ['', [Validators.required, Validators.minLength(7), Validators.pattern(this.patternTelefono)]],
      pais: ['', [Validators.required]],
      provincia: ['', [Validators.required]],
      localidad: ['', [Validators.required]],
      domicilio: ['',  [Validators.required, Validators.minLength(5), Validators.pattern(this.patternDomicilio)]],
      usuario: ['',  [Validators.required, Validators.minLength(5), Validators.pattern(this.patternUsuario)]],
    }
  )

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

  constructor(private clienteService: ClienteService, private tipodniService: TipoDniService, private paisesService: PaisService, private provService: ProvinciasService, private localService: LocalidadesService, private tokenStorage: TokenStorageService, private router: Router, private fb: FormBuilder)
  {

  }

  ngOnInit(): void
  {
    this.idCliente=this.tokenStorage.getIdClient();

    //Obtenemos todos los datos del cliente logueado
    this.clienteService.getCliente().subscribe
    (
      (data : Cliente) =>
      {
        this.selfie = data.SelfieCliente;
        this.fotoUsuario = this.selfie;

        this.selectedCliente.Id = data.Id;
        this.nombre = data.Nombre;
        this.apellido = data.Apellido;
        this.sexo = data.Sexo;
        this.fechaNacimiento = data.FechaNacimiento;
        this.idTipoDni = data.IdTipoDni;
        this.numDni = data.NumDni;
        this.email = data.Email;
        this.telefono = data.Telefono;
        this.idLocalidad = data.IdLocalidad;
        this.domicilio = data.Domicilio;
        this.nombreUsuario = data.NombreUsuario;
        this.password = data.Password;
      },
      err =>
      {

      }
    );

    //Obtenemos la lista de tipos de dni
    this.tipodniService.getTipoDni().subscribe
    (
      data =>
      {
        this.tipoDni = data;
      }
    );

    //Obtenemos la lista de los paises
    this.paisesService.getPaises().subscribe
    (
      data =>
      {
        this.paises = data;
      }
    );

    //Obtenemos la lista de las provincias
    this.provService.getProvincias().subscribe
    (
      data =>
      {
        this.provincias = data;
      }
    );

    //Obtenemos la lista de las localidades
    this.localService.getLocalidades().subscribe
    (
      data =>
      {
        this.localidades = data;
      }
    );

  }

  public regresarClick()
  {
    this.router.navigate(['/obtener']);
  }

  //Método al enviar el fomulario
  public onSubmit(cliente: Cliente)
  {
    console.log(cliente)
      //Nos suscribimos al servicio y traemos el método del backend
      this.clienteService.onUpdateCliente(cliente).subscribe
      (
        data =>
        {
          if (data === 2)
          {
            console.log(data)
            this.message = 'Ese EMAIL ya está registrado, ingresá otro';
            this.isCreateFailed = true;
          }
          else if (data === 3)
          {
            console.log(data)
            this.message = 'Ese USUARIO ya está registrado, ingresá otro';
            this.isCreateFailed = true;
          }
          else if (data === 0)
          {
            console.log(data)
            swal.fire('Fantástico', '¡El cliente se editó con éxito!', 'success');
            this.isCreateFailed = false;
            this.router.navigate(['/home']);
          }

        }
      );
  }

  modificarSelfie()
  {
    swal.fire('En Construccion','Pronto podras modificar de tu foto de perfil','info');
  }

  //Métodos para cargar los select filtrados por el id del select seleccionado(Pais-->Provincia-->Localidad)
  onSelectPais(id: number): void
  {
    this.limpiarForm();
    this.localidadesAux = null;
    this.provinciasAux = null;
    this.provinciasAux = this.provincias.filter(item => item.id_pais == id);
  }

  onSelectProv(id: number): void
  {
    this.localidadesAux = this.localidades.filter(item => item.id_provincia == id);
  }

  //Método para resetear las alerts
  resetAlert()
  {
    this.isCreateFailed = false;
    this.passwordsDistintas = false;
  }

  //Método para limpiar select al cambiar el target
  limpiarForm()
  {
    this.form.patchValue
    (
      {
        provincia: '',
        localidad: ''
      }
    );
  }

}