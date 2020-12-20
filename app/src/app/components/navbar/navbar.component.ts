import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private tokenStorage: TokenStorageService,private router: Router) { }

  ngOnInit(): void {
  }

  perfil() : void
  {
    this.router.navigate(['/obtener']);
  }

  logout() : void {
    this.tokenStorage.logOut();
    this.router.navigate(['/login']);
  }



}
