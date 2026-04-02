import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogoutLinkComponent } from './logout-link-component';

describe('LogoutLinkComponent', () => {
  let component: LogoutLinkComponent;
  let fixture: ComponentFixture<LogoutLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogoutLinkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogoutLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
