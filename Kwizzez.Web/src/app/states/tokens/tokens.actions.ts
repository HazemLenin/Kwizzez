import { createAction, props } from '@ngrx/store';
import Tokens from 'src/app/models/Tokens';

export const login = createAction('LOGIN', props<{ payload: Tokens }>());
export const logout = createAction('LOGOUT');
