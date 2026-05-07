import { Component } from '@angular/core';
import { HomeCarouselComponent } from "../home-carousel-component/home-carousel-component";
import { ViewedProductsComponent } from "../viewed-products-component/viewed-products-component";
import { ProductRecomendationsComponent } from "../product-recomendations-component/product-recomendations-component";
import { CategoryNavbarComponent } from "../category-navbar-component/category-navbar-component";
import { CategoryNavbarLayoutComponent } from "../category-navbar-layout-component/category-navbar-layout-component";

@Component({
  selector: 'app-home-component',
  imports: [HomeCarouselComponent, ViewedProductsComponent, ProductRecomendationsComponent, CategoryNavbarLayoutComponent],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {

}
