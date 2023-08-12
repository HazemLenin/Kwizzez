import { createAction, props } from '@ngrx/store';
import { User } from '../../models/User';

export const loadUser = createAction('LOAD_USER', props<{ payload: User }>());
export const removeUser = createAction('REMOVE_USER');
