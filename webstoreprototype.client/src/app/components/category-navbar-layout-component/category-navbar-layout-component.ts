import { Component, inject } from '@angular/core';
import { CategoryService } from '../../services/category-service/category-service';
import { Observable } from 'rxjs';
import { Category } from '../../interfaces/category';
import { AsyncPipe } from '@angular/common';
import { CategoryNavbarComponent } from "../category-navbar-component/category-navbar-component";

@Component({
  selector: 'app-category-navbar-layout-component',
  imports: [CategoryNavbarComponent, AsyncPipe],
  templateUrl: './category-navbar-layout-component.html',
  styleUrl: './category-navbar-layout-component.css',
})
export class CategoryNavbarLayoutComponent {
  private categoryService = inject(CategoryService);
  sidebarCategoryList$: Observable<Category[]> = this.categoryService.getCategories();
}
