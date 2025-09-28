import { Routes } from '@angular/router';
import { homeRoutes } from './features';
import { signInRoutes } from './features/sign-in/sign-in.routes';

export const routes: Routes = [
  ...homeRoutes,
  ...signInRoutes,
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
