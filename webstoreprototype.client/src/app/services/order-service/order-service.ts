import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Order } from '../../interfaces/order';
import { catchError, EMPTY, shareReplay } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private http = inject(HttpClient);
  private apiUrl = 'api/order';

  private orders$ = this.http.get<Order[]>(this.apiUrl).pipe(
    shareReplay(1),
    catchError(err => {
      console.error('Failed to load categories', err);
      return EMPTY;
    })
  );

  getOrders(){
    return this.orders$;
  }
}
