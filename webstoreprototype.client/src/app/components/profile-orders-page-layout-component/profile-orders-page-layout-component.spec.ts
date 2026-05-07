import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileOrdersPageLayoutComponent } from './profile-orders-page-layout-component';

describe('ProfileOrdersPageLayoutComponent', () => {
  let component: ProfileOrdersPageLayoutComponent;
  let fixture: ComponentFixture<ProfileOrdersPageLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileOrdersPageLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileOrdersPageLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
