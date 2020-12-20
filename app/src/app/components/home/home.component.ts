import { Component, OnInit } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { StringifyOptions } from 'querystring';
import { Observable } from 'rxjs';
import { Cliente } from 'src/app/models/cliente.model';
import { ClienteService } from 'src/app/services/cliente.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  nombreUsuario : string;
  selfie: string;
  fotoUsuario: string;
  
  

  
  constructor(private tokenStorage: TokenStorageService, private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.nombreUsuario=this.tokenStorage.getUser();

    this.clienteService.getCliente().subscribe(
      (data : Cliente) => {
        this.selfie = data.SelfieCliente;
        if (this.selfie == 'nulo')
        {
          this.fotoUsuario = "./assets/images/foto-block.jpg"

        }else
        {
          this.fotoUsuario = this.selfie
        }
        
        
      },
      err => {
       
      }
    );
    
  }

}
