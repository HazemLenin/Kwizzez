import { createReducer, on } from '@ngrx/store';
import { login, logout } from './isAuthenticated.actions';

export const isAuthenticatedReducer = createReducer(
  false,
  on(login, (state) => true),
  on(logout, (state) => false)
);
