import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileFavoritesLayoutComponent } from './profile-favorites-layout-component';

describe('ProfileFavoritesLayoutComponent', () => {
  let component: ProfileFavoritesLayoutComponent;
  let fixture: ComponentFixture<ProfileFavoritesLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileFavoritesLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileFavoritesLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
