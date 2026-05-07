import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, catchError, shareReplay, EMPTY } from 'rxjs';
import { Product } from '../../interfaces/product';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private http = inject(HttpClient);
  private apiUrl = '/api/favoriteProducts';

  private products$ = this.http.get<Product[]>(this.apiUrl).pipe(
    shareReplay(1),
    catchError(err => {
      console.error('Failed to load favorite products', err)
      return EMPTY;
    })
  );

  getFavoriteProducts(): Observable<Product[]>{
    return this.products$;
  }
}
