import { createReducer, on } from '@ngrx/store';
import { login, logout } from './tokens.actions';

export const tokensReducer = createReducer(
  !!localStorage.getItem('token')
    ? {
        token: localStorage.getItem('token'),
        refreshToken: localStorage.getItem('refreshToken'),
      }
    : null,
  on(login, (state, { payload }: { payload: any }) => {
    localStorage.setItem('token', payload.token);
    localStorage.setItem('refreshToken', payload.refreshToken);
    return payload;
  }),
  on(logout, (state) => {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    return null;
  })
);
