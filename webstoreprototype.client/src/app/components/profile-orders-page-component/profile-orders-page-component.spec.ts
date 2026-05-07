import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileOrdersPageComponent } from './profile-orders-page-component';

describe('ProfileOrdersPageComponent', () => {
  let component: ProfileOrdersPageComponent;
  let fixture: ComponentFixture<ProfileOrdersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileOrdersPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileOrdersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
