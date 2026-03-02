import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductRecomendationsComponent } from './product-recomendations-component';

describe('ProductRecomendationsComponent', () => {
  let component: ProductRecomendationsComponent;
  let fixture: ComponentFixture<ProductRecomendationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductRecomendationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductRecomendationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
