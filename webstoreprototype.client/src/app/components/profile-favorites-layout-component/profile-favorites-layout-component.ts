import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { FavoriteProductsService } from '../../services/favorite-products/favorite-products-service';
import { ProfileFavoritesPageComponent } from "../profile-favorites-page-component/profile-favorites-page-component";
import { Observable } from 'rxjs';
import { Product } from '../../interfaces/product';

@Component({
  selector: 'app-profile-favorites-layout-component',
  imports: [ProfileFavoritesPageComponent, AsyncPipe],
  templateUrl: './profile-favorites-layout-component.html',
  styleUrl: './profile-favorites-layout-component.css',
})
export class ProfileFavoritesLayoutComponent {

  favoriteProductsService = inject(FavoriteProductsService);
  favoriteProducts$: Observable<Product[]> = this.favoriteProductsService.getFavoriteProducts();

}
