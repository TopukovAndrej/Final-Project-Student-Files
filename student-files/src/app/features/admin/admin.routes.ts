import { Route } from '@angular/router';
import { adminRoleGuard, authGuard } from '../../core';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';

export const adminRoutes: Route[] = [
  {
    path: 'admin/dashboard',
    canActivate: [authGuard, adminRoleGuard],
    component: AdminDashboardComponent,
  },
];
