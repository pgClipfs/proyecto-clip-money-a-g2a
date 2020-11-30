import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.model';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { ClienteService } from '../../services/cliente.service';


@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  public clientes: Cliente[];
  selectedCliente: Cliente = new Cliente();
  errorMessage = '';
  isCreateFailed = false;

  constructor(private clienteService: ClienteService, private tokenStorage: TokenStorageService, private router: Router) { }

  ngOnInit(): void {
    this.clienteService.getClientes().subscribe(resp => {
      console.log(resp);
      this.clientes = resp;
    })
    if (this.tokenStorage.getToken()) {
      this.router.navigate(['/home']);
    }
  }
  nuevoCliente() {
    alert("asdads");
  }

  public onSubmit(cliente: Cliente) {
      
      this.clienteService.onCreateCliente(cliente).subscribe(resp => {
        this.clientes.push(resp);
        this.router.navigate(['/login']);
      })
      
    
      console.log(cliente);
    this.selectedCliente = new Cliente();
    console.log(cliente);
  }
  public onSelect(item: Cliente) {
    this.selectedCliente = item;
  }
}
