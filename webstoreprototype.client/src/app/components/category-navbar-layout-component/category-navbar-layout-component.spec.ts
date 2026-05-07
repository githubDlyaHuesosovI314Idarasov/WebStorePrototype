import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryNavbarLayoutComponent } from './category-navbar-layout-component';

describe('CategoryNavbarLayoutComponent', () => {
  let component: CategoryNavbarLayoutComponent;
  let fixture: ComponentFixture<CategoryNavbarLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoryNavbarLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategoryNavbarLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
