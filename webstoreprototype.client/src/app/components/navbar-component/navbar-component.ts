import { Component, inject, effect } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CatalogueButtonComponent } from '../catalogue-button-component/catalogue-button-component';
import { SearchBarComponent } from '../search-bar-component/search-bar-component';
import { ActionsComponent } from '../actions-component/actions-component';
import { ShoppingCartComponent } from "../shopping-cart-component/shopping-cart-component";
import { ActionsOffCanvasComponent } from '../actions-off-canvas-component/actions-off-canvas-component';


@Component({
  selector: 'app-navbar-component',
  imports: [RouterLink, CatalogueButtonComponent, SearchBarComponent, ActionsComponent, ShoppingCartComponent, ActionsOffCanvasComponent],
  templateUrl: './navbar-component.html',
  styleUrl: './navbar-component.css',
})
export class NavbarComponent {

}
