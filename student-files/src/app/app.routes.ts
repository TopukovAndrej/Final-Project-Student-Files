import { Routes } from '@angular/router';
import { adminRoutes, homeRoutes } from './features';
import { signInRoutes } from './features/sign-in/sign-in.routes';
import { professorRoutes } from './features';

export const routes: Routes = [
  ...homeRoutes,
  ...signInRoutes,
  ...adminRoutes,
  ...professorRoutes,
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
