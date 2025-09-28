import { Routes } from '@angular/router';
import { adminRoutes, homeRoutes } from './features';
import { signInRoutes } from './features/sign-in/sign-in.routes';

export const routes: Routes = [
  ...homeRoutes,
  ...signInRoutes,
  ...adminRoutes,
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
