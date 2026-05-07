import { TestBed } from '@angular/core/testing';

import { FavoriteProductsService } from './favorite-products-service';

describe('FavoriteProductsService', () => {
  let service: FavoriteProductsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FavoriteProductsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
