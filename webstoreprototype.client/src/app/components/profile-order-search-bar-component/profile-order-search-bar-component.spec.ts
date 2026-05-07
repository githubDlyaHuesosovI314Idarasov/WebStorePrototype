import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileOrderSearchBarComponent } from './profile-order-search-bar-component';

describe('ProfileOrderSearchBarComponent', () => {
  let component: ProfileOrderSearchBarComponent;
  let fixture: ComponentFixture<ProfileOrderSearchBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileOrderSearchBarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileOrderSearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
