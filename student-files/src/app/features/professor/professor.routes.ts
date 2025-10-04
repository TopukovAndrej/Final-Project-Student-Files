import { Route } from '@angular/router';
import { authGuard, professorRoleGuard } from '../../core';
import { ProfessorDashboardComponent } from './components/professor-dashboard/professor-dashboard.component';

export const professorRoutes: Route[] = [
  {
    path: 'professor/dashboard',
    canActivate: [authGuard, professorRoleGuard],
    component: ProfessorDashboardComponent,
  },
];
