import { Component, Input } from '@angular/core';
import { Product } from '../../interfaces/product';
import { ProductCardComponent } from "../product-card-component/product-card-component";
import { ProductImage } from '../../interfaces/product-image';

@Component({
  selector: 'app-viewed-products-component',
  imports: [ProductCardComponent],
  templateUrl: './viewed-products-component.html',
  styleUrl: './viewed-products-component.css',
})
export class ViewedProductsComponent {
  @Input() viewedProductsList!: Product[];
  @Input() viewedProductImagesList!: ProductImage[];
}
