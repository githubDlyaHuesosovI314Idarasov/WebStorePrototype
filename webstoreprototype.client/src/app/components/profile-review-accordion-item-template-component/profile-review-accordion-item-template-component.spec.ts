import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileReviewAccordionItemTemplateComponent } from './profile-review-accordion-item-template-component';

describe('ProfileReviewAccordionItemTemplateComponent', () => {
  let component: ProfileReviewAccordionItemTemplateComponent;
  let fixture: ComponentFixture<ProfileReviewAccordionItemTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileReviewAccordionItemTemplateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileReviewAccordionItemTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
