import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { HomeComponent } from './components/home/home.component';
import { IndexComponent } from './components/index/index.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { LoginComponent } from './components/login/login.component';
import { RecoveryMailComponent } from './components/recoverymail/recoverymail.component';
import { RecoverypasswordComponent } from './components/recoverypassword/recoverypassword.component';
import { GuardService } from './services/guard.service';

const routes: Routes = [
  {path: '', component: IndexComponent},
  {path: 'index', component: IndexComponent},
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent,canActivate:[GuardService],
   children: [
    {path: '', component: InicioComponent},
    {path: 'inicio', component: InicioComponent}
  ]},
  {path: 'cliente', component: ClienteComponent},
  {path: 'recoverymail', component: RecoveryMailComponent},
  {path: 'recoverypassword', component: RecoverypasswordComponent},
  {path: '**', component: IndexComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
