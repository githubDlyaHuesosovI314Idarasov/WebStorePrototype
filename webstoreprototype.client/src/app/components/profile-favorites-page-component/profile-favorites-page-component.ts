import { Component, Input } from '@angular/core';
import { Product } from '../../interfaces/product';
import { ProductTemplateComponent } from "../product-template-component/product-template-component";

@Component({
  selector: 'app-profile-favorites-page-component',
  imports: [ProductTemplateComponent],
  templateUrl: './profile-favorites-page-component.html',
  styleUrl: './profile-favorites-page-component.css',
})
export class ProfileFavoritesPageComponent {
  @Input() favoriteProductList: Product[] = []; 
}
