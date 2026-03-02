import { Routes } from '@angular/router';
import { HomeComponent } from './components/home-component/home-component';
import { SelectedCategoryPageComponent } from './components/selected-category-page-component/selected-category-page-component';
import { SearchResultPageComponent } from './components/search-result-page-component/search-result-page-component';
import { OrderPageComponent } from './components/order-page-component/order-page-component';
import { ComparePageComponent } from './components/compare-page-component/compare-page-component';
import { ProductInfoPageComponent } from './components/product-info-page-component/product-info-page-component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home page',
  },
  {
    path: 'category/:name',
    component: SelectedCategoryPageComponent,
    title: 'Searched Category'
  },
  {
    path: 'search/:text',
    component: SearchResultPageComponent,
    title: 'Search',
  },
  {
    path: 'order',
    component: OrderPageComponent,
    title: 'Order'
  },
  {
    path: 'compare',
    component: ComparePageComponent,
    title: 'Compare'
  },
  {
    path: 'product/:id',
    component: ProductInfoPageComponent,
    title: 'Product'
  }

];

export default routes;
