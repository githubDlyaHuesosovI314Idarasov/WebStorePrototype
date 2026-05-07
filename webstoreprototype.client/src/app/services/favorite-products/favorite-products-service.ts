import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, shareReplay, catchError, EMPTY } from 'rxjs';
import { Product } from '../../interfaces/product';


@Injectable({
  providedIn: 'root',
})
export class FavoriteProductsService {

  private http = inject(HttpClient);
  private apiUrl = '/api/favoriteProducts';

  private favoriteProducts$ = this.http.get<Product[]>(this.apiUrl).pipe(
    shareReplay(1),
    catchError(err => {
      console.error('Failed to load favorite products', err)
      return EMPTY;
    })
  );

  getFavoriteProducts(): Observable<Product[]>{
    return this.favoriteProducts$;
  }
}
