import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterLinkComponent } from './register-link-component';

describe('RegisterLinkComponent', () => {
  let component: RegisterLinkComponent;
  let fixture: ComponentFixture<RegisterLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterLinkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
