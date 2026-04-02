import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionsOffCanvasComponent } from './actions-off-canvas-component';

describe('ActionsOffCanvasComponent', () => {
  let component: ActionsOffCanvasComponent;
  let fixture: ComponentFixture<ActionsOffCanvasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActionsOffCanvasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActionsOffCanvasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
