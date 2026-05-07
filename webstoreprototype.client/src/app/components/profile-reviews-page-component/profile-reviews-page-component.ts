import { Component } from '@angular/core';
import { Review } from '../../interfaces/review';
import { ProfileReviewAccordionItemTemplateComponent } from "../profile-review-accordion-item-template-component/profile-review-accordion-item-template-component";

@Component({
  selector: 'app-profile-reviews-page-component',
  imports: [ProfileReviewAccordionItemTemplateComponent],
  templateUrl: './profile-reviews-page-component.html',
  styleUrl: './profile-reviews-page-component.css',
})
export class ProfileReviewsPageComponent {
  reviewList: Review[] = [];
}
