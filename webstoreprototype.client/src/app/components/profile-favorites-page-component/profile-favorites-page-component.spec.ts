import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileFavoritesPageComponent } from './profile-favorites-page-component';

describe('ProfileFavoritesPageComponent', () => {
  let component: ProfileFavoritesPageComponent;
  let fixture: ComponentFixture<ProfileFavoritesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileFavoritesPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileFavoritesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
