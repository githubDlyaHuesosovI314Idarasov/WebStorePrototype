import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Category } from '../../interfaces/category';
import { catchError, EMPTY, Observable, shareReplay } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private http = inject(HttpClient);
  private apiUrl = 'api/category';

  private categories$ = this.http.get<Category[]>(this.apiUrl).pipe(
      shareReplay(1),
      catchError(err => {
        console.error('Failed to load categories', err);
        return EMPTY; 
      })
    );

  getCategories() : Observable<Category[]>{
    return this.categories$;
  }
}
