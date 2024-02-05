import { CanDeactivateFn } from '@angular/router';
import { UserEditComponent } from '../_components/users/user-edit/user-edit.component';
import { inject } from '@angular/core';

// export const preventUnsavedChangesGuard
// : CanDeactivateFn<UserEditComponent> = (component, currentRoute, currentState, nextState) => {
  
//   if(component.editform?.dirty){
//     const confirmservice = inject(confirmservice);
//     return confirmservice.confirm();
//   }
//   return of(true);
// };
