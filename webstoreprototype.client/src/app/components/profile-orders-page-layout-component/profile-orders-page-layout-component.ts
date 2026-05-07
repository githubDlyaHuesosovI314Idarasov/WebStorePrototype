import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { OrderService } from '../../services/order-service/order-service';
import { Observable } from 'rxjs';
import { Order } from '../../interfaces/order';
import { ProfileOrdersPageComponent } from "../profile-orders-page-component/profile-orders-page-component";

@Component({
  selector: 'app-profile-orders-page-layout-component',
  imports: [ProfileOrdersPageComponent, AsyncPipe],
  templateUrl: './profile-orders-page-layout-component.html',
  styleUrl: './profile-orders-page-layout-component.css',
})
export class ProfileOrdersPageLayoutComponent {
   private orderService = inject(OrderService);
   orderList: Observable<Order[]> = this.orderService.getOrders();
}
