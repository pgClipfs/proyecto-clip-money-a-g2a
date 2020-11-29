import { Component, OnInit} from '@angular/core';
import { Cliente } from 'src/app/models/cliente.model';
import { ClienteService } from '../../services/cliente.service';


@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  angular = "https://angular.io/"
  public clientes: Cliente[];
  selectedCliente: Cliente = new Cliente();

  constructor(private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.clienteService.getClientes().subscribe(resp => {
      console.log(resp);
      this.clientes = resp;
    })
  }
  nuevoCliente() {
    alert("asdads");
  }

  public onSubmit(cliente: Cliente) {
  
      this.clienteService.onCreateCliente(cliente).subscribe(resp => {
        this.clientes.push(resp);
      })
    
    
      console.log(cliente);
    this.selectedCliente = new Cliente();
    console.log(cliente);
  }
  public onSelect(item: Cliente) {
    this.selectedCliente = item;
  }
}
