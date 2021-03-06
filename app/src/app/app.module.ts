import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { ClienteService } from './services/cliente.service';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { IndexComponent } from './components/index/index.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/login/login.component';
import { RecoveryMailComponent } from './components/recoverymail/recoverymail.component';
import { RecoverypasswordComponent } from './components/recoverypassword/recoverypassword.component';
import { RecoverymailService } from './services/recoverymail.service';
import { ModificarComponent } from './components/cliente/modificar/modificar.component';
import { ObtenerComponent } from './components/cliente/obtener/obtener.component';
import { IndexOperacionesComponent } from './components/operaciones/index-operaciones/index-operaciones.component';
import { CuentasComponent } from './components/cuentas/cuentas.component';
import { DetalleCuentaComponent } from './components/cuentas/detalle-cuenta/detalle-cuenta.component';
import { MovimientosComponent } from './components/cuentas/detalle-cuenta/movimientos/movimientos.component';
import {NgxPaginationModule} from 'ngx-pagination';
import { ModificarPasswordComponent } from './components/cliente/modificar-password/modificar-password.component';



@NgModule({
  declarations: [
    AppComponent,
    ClienteComponent,
    HomeComponent,
    NavbarComponent,
    IndexComponent,
    InicioComponent,
    LoginComponent,
    RecoveryMailComponent,
    RecoverypasswordComponent,
    ModificarComponent,
    ObtenerComponent,
    IndexOperacionesComponent,
    CuentasComponent,
    DetalleCuentaComponent,
    MovimientosComponent,
    ModificarPasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserModule,
    FormsModule, 
    ReactiveFormsModule,
    NgxPaginationModule
  ],
  providers: [ClienteService, RecoverymailService],
  bootstrap: [AppComponent]
})
export class AppModule { }
