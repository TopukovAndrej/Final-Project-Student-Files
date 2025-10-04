import { Route } from '@angular/router';
import { studentRoleGuard, authGuard } from '../../core';
import { StudentDashboardComponent } from './components/student-dashboard/student-dashboard.component';

export const studentRoutes: Route[] = [
  {
    path: 'student/dashboard',
    canActivate: [authGuard, studentRoleGuard],
    component: StudentDashboardComponent,
  },
];
