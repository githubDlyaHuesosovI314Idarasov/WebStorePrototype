import { Component, Input } from '@angular/core';
import { Review } from '../../interfaces/review';

@Component({
  selector: 'app-profile-review-accordion-item-template-component',
  imports: [],
  templateUrl: './profile-review-accordion-item-template-component.html',
  styleUrl: './profile-review-accordion-item-template-component.css',
})
export class ProfileReviewAccordionItemTemplateComponent {
  @Input() review! : Review;
}
