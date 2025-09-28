import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "..";
import { UserRole } from "../../shared";

export const studentRoleGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const user = authService.getCurrentUser();

  if (user?.userRole === UserRole.Student) {
    return true;
  }

  router.navigate(['/home']);
  return false;
};
