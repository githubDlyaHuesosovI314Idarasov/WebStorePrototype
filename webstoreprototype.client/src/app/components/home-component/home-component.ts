import { Component } from '@angular/core';
import { HomeCarouselComponent } from "../home-carousel-component/home-carousel-component";
import { ViewedProductsComponent } from "../viewed-products-component/viewed-products-component";
import { ProductRecomendationsComponent } from "../product-recomendations-component/product-recomendations-component";

@Component({
  selector: 'app-home-component',
  imports: [HomeCarouselComponent, ViewedProductsComponent, ProductRecomendationsComponent],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {

}
