import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedCategoryPageComponent } from './selected-category-page-component';

describe('SelectedCategoryPageComponent', () => {
  let component: SelectedCategoryPageComponent;
  let fixture: ComponentFixture<SelectedCategoryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectedCategoryPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectedCategoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
