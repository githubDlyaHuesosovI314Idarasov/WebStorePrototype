import { Component, Input } from '@angular/core';
import { Order } from '../../interfaces/order';
import { Product } from '../../interfaces/product';
@Component({
  selector: 'app-order-accordion-template-component',
  imports: [],
  templateUrl: './order-accordion-template-component.html',
  styleUrl: './order-accordion-template-component.css',
})
export class OrderAccordionTemplateComponent {
  @Input() order!: Order;

    getGroupedProducts(): { product: Product; count: number }[] {
    const map = new Map<string, { product: Product; count: number }>();

    for (const product of this.order.products) {
      const existing = map.get(product.id);
      if (existing) {
        existing.count++;
      } else {
        map.set(product.id, { product, count: 1 });
      }
    }

    return Array.from(map.values());
  }
}
