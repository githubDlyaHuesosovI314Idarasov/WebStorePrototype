import { Component, Input, inject } from '@angular/core';
import { Product } from '../../interfaces/product';
import { RouterLink } from "@angular/router";
import { ProductCarouselComponent } from "../product-carousel-component/product-carousel-component";
import { ProductImage } from '../../interfaces/product-image';

@Component({
  selector: 'app-product-card-component',
  imports: [RouterLink, ProductCarouselComponent],
  templateUrl: './product-card-component.html',
  styleUrl: './product-card-component.css',
})
export class ProductCardComponent {
  @Input() product!: Product;
  @Input() productImages!: ProductImage[];
  
}
