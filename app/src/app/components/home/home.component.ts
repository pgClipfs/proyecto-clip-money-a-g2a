import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  nombreUsuario : string;
  
  constructor(private tokenStorage: TokenStorageService) { }

  ngOnInit(): void {
    this.nombreUsuario=this.tokenStorage.getUser();
  }

}
