import { Component, Input } from '@angular/core';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";
import { Category } from '../../interfaces/category';

@Component({
  selector: 'app-category-navbar-component',
  imports: [FaIconComponent],
  templateUrl: './category-navbar-component.html',
  styleUrl: './category-navbar-component.css',
})
export class CategoryNavbarComponent {
  @Input() sidebarCategoryList: Category[] = [];
}
