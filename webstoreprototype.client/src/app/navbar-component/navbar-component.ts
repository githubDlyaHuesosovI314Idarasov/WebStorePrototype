import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CatalogueButtonComponent } from '../components/catalogue-button-component/catalogue-button-component';
import { AuthComponent } from '../components/auth-component/auth-component';
import { SearchBarComponent } from '../components/search-bar-component/search-bar-component';
import { ActionsComponent } from '../components/actions-component/actions-component';

@Component({
  selector: 'app-navbar-component',
  imports: [RouterLink, CatalogueButtonComponent, AuthComponent, SearchBarComponent, ActionsComponent],
  templateUrl: './navbar-component.html',
  styleUrl: './navbar-component.css',
})
export class NavbarComponent {

}
