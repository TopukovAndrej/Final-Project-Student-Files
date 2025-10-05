import { Routes } from '@angular/router';
import { adminRoutes, homeRoutes } from './features';
import { signInRoutes } from './features/sign-in/sign-in.routes';
import { professorRoutes } from './features';
import { studentRoutes } from './features/student';

export const routes: Routes = [
  ...homeRoutes,
  ...signInRoutes,
  ...adminRoutes,
  ...professorRoutes,
  ...studentRoutes,
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
