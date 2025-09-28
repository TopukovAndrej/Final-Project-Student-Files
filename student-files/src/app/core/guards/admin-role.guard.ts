import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '..';
import { UserRole } from '../../shared';

export const adminRoleGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const user = authService.getCurrentUser();
  if (user?.userRole === UserRole.Admin) {
    return true;
  }

  router.navigate(['/home']);
  return false;
};
