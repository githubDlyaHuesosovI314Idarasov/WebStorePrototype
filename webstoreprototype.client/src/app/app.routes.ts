import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth-component/auth-component';

const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent,
    title: 'Auth',
  },

];

export default routes;
