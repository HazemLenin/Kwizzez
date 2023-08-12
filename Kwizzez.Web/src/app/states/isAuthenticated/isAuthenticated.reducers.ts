import { createReducer, on } from '@ngrx/store';
import { login, logout } from './isAuthenticated.actions';

export const isAuthenticatedReducer = createReducer(
  !!localStorage.getItem('token'),
  on(login, (state) => true),
  on(logout, (state) => false)
);
