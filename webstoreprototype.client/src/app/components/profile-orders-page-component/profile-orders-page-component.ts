import { Component, inject,Input } from '@angular/core';
import { Order } from '../../interfaces/order';
import { OrderAccordionTemplateComponent } from "../order-accordion-template-component/order-accordion-template-component";
import { faFilter } from '@fortawesome/free-solid-svg-icons';
import { ProfileOrderSelectComponent } from "../profile-order-select-component/profile-order-select-component";
import { ProfileOrderSearchBarComponent } from "../profile-order-search-bar-component/profile-order-search-bar-component";
import { OrderService } from '../../services/order-service/order-service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile-orders-page-component',
  imports: [OrderAccordionTemplateComponent, ProfileOrderSelectComponent, ProfileOrderSearchBarComponent],
  templateUrl: './profile-orders-page-component.html',
  styleUrl: './profile-orders-page-component.css',
})
export class ProfileOrdersPageComponent {
   @Input() orderList : Order[] = [];
   faFilter = faFilter;
}
