import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShoppingCart, faCartShopping } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-shopping-cart-component',
  imports: [FontAwesomeModule],
  templateUrl: './shopping-cart-component.html',
  styleUrl: './shopping-cart-component.css',
})
export class ShoppingCartComponent {
  faShoppingCart = faShoppingCart; 
  faCartShopping = faCartShopping; 
}
