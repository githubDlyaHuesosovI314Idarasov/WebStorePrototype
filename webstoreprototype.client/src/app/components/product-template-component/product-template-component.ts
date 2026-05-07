import { Component, Input } from '@angular/core';
import { Product } from '../../interfaces/product';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";
import { faHryvniaSign, faCartArrowDown } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-product-template-component',
  imports: [FaIconComponent],
  templateUrl: './product-template-component.html',
  styleUrl: './product-template-component.css',
})
export class ProductTemplateComponent {
  @Input() product!: Product;
  faHryvniaSign = faHryvniaSign;
  faCartArrowDown = faCartArrowDown;
}
