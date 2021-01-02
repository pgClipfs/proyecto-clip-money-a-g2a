import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { ModificarComponent } from './components/cliente/modificar/modificar.component';
import { ObtenerComponent } from './components/cliente/obtener/obtener.component';
import { CuentasComponent } from './components/cuentas/cuentas.component';
import { DetalleCuentaComponent } from './components/cuentas/detalle-cuenta/detalle-cuenta.component';
import { MovimientosComponent } from './components/cuentas/detalle-cuenta/movimientos/movimientos.component';
import { HomeComponent } from './components/home/home.component';
import { IndexComponent } from './components/index/index.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/login/login.component';
import { RecoveryMailComponent } from './components/recoverymail/recoverymail.component';
import { RecoverypasswordComponent } from './components/recoverypassword/recoverypassword.component';
import { GuardService } from './services/guard.service';

const routes: Routes =
[
  {path: '', component: IndexComponent},
  {path: 'index', component: IndexComponent},
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent,canActivate:[GuardService],
   children: [
    {path: '', component: InicioComponent},
    {path: 'inicio', component: InicioComponent},
    {path: 'cuentas', component: CuentasComponent},
    {path: 'cuentas/detalle-cuenta', component: DetalleCuentaComponent},
    {path: 'cuentas/detalle-cuenta/movimientos', component: MovimientosComponent}
       
  ]},
  {path: 'cliente', component: ClienteComponent},
  {path: 'modificar', component: ModificarComponent, canActivate:[GuardService]},
  {path: 'obtener', component: ObtenerComponent, canActivate:[GuardService]},
  {path: 'recoverymail', component: RecoveryMailComponent},
  {path: 'recoverypassword', component: RecoverypasswordComponent},
  {path: '**', component: IndexComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
