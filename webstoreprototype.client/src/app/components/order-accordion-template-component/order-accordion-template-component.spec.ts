import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderAccordionTemplateComponent } from './order-accordion-template-component';

describe('OrderAccordionTemplateComponent', () => {
  let component: OrderAccordionTemplateComponent;
  let fixture: ComponentFixture<OrderAccordionTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderAccordionTemplateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderAccordionTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
