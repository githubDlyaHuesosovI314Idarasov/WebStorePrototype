import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileOrderSelectComponent } from './profile-order-select-component';

describe('ProfileOrderSelectComponent', () => {
  let component: ProfileOrderSelectComponent;
  let fixture: ComponentFixture<ProfileOrderSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileOrderSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileOrderSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
