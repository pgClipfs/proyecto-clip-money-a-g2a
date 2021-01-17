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
 idCliente: number;
 fotoUsuario: any;
 
  
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
  selectedLocalidad: number;
  idLocalidad: number;
  idProvincia: number;
  idPais: number;

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

        this.selectedCliente.SelfieCliente = data.SelfieCliente;
        this.selectedCliente.Id = data.Id;
        this.selectedCliente.Nombre = data.Nombre;
        this.selectedCliente.Apellido = data.Apellido;
        this.selectedCliente.Sexo = data.Sexo;
        this.selectedCliente.FechaNacimiento = this.formatDate(data.FechaNacimiento);
        this.selectedCliente.IdTipoDni= data.IdTipoDni;
        this.selectedCliente.NumDni = data.NumDni;
        this.selectedCliente.Email = data.Email;
        this.selectedCliente.Telefono = data.Telefono;
        this.selectedCliente.IdLocalidad= data.IdLocalidad;
        this.selectedCliente.Domicilio = data.Domicilio;
        this.selectedCliente.NombreUsuario = data.NombreUsuario;
        this.selectedCliente.Password = data.Password;


        //Obtenemos la lista de las localidades
        this.localService.getLocalidades().subscribe
        (
          data =>
          {
            this.localidades = data;
            this.localidadesAux = this.localidades;
            this.idProvincia = this.localidades[this.idLocalidad]['id_provincia'];

            //Obtenemos la lista de las provincias
            this.provService.getProvincias().subscribe
            (
              data =>
              {
                this.provincias = data;
                this.provinciasAux = this.provincias;
                this.idPais = this.provincias[this.idProvincia]['id_pais'];

                //Obtenemos la lista de los paises
                this.paisesService.getPaises().subscribe
                (
                  data =>
                  {
                    this.paises = data;
                    
                  }
                );                
              }
            );
          }
        );
          //asignamos el idlocalidad a la variable para ahcer el efecto cascada y cargar localida -> prov -> pais
          this.idLocalidad = this.selectedCliente.IdLocalidad;
      },
      err =>
      {

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
    //Asignamos el id de la nueva localidad al objeto cliente
    this.selectedCliente.IdLocalidad = this.idLocalidad;
      //Nos suscribimos al servicio y traemos el método del backend
      this.clienteService.onUpdateCliente(cliente).subscribe
      (
        data =>
        {
          if (data === 2)
          {
            this.message = 'Ese EMAIL ya está registrado, ingresá otro';
            this.isCreateFailed = true;
          }
          else if (data === 3)
          {
            this.message = 'Ese USUARIO ya está registrado, ingresá otro';
            this.isCreateFailed = true;
          }
          else if (data === 0)
          {
            swal.fire('Fantástico', '¡El cliente se editó con éxito!', 'success');
            this.isCreateFailed = false;
            this.router.navigate(['/home']);
          }

        }
      );
  }

  //Metodo para modificar la foto de perfil
   modificarSelfie()
  {
    (async () => {

      const { value: file } = await swal.fire({
        title: 'Selecione la imagen',
        input: 'file',
        width: 600,
        imageWidth: 100,
        inputAttributes: {
          'accept': 'image/*',
          'aria-label': 'Upload your profile picture'
        }
      })

    if (file != null) {
      if(this.validarFoto(file['name'])){
      
        const reader = new FileReader()
        reader.onload = (e) => {
        
          this.fotoUsuario = e.target.result;
          this.selectedCliente.SelfieCliente = this.fotoUsuario;
        }
        reader.readAsDataURL(file)
      }else{
        swal.fire('Formato invalido', 'Solo se permite imagenes .jpg y .png', 'warning');
      }
     
    }else{
      swal.fire('Campo Vacio', 'Seleccione un archivo', 'warning');
     }
      
      
      })()
  }

  //Métodos para cargar los select filtrados por el id del select seleccionado(Pais-->Provincia-->Localidad)
  onSelectPais(id: number): void
  {
    this.limpiarForm();
    this.localidades = null;
    this.provincias = this.provinciasAux;
    this.provincias = this.provincias.filter(item => item.id_pais == id);
  }

  onSelectProv(id: number): void
  {
    this.localidades = this.localidadesAux;
    this.localidades = this.localidades.filter(item => item.id_provincia == id);
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

  //Metodo validar extension del archivo subido
  validarFoto(foto: any): boolean{
    let extPermitidas = /(.jpg|.png)$/i;

    if(!extPermitidas.exec(foto))
    {
        
        return false;
    }
    else
    {
        return true;
    }
 }

 //metodo modificar password
 modificarPassword(){
  
 }

 //Metodo para formatear fecha a aaaa-mm-dd
 formatDate(date:string): string{
   let fechaString = date.split("-",3)
  let diaString = fechaString[0];
  let mesString = fechaString[1];
  let anioString = fechaString[2];
   return `${anioString}-${mesString}-${diaString}`;
 }

}