import { createReducer, on } from '@ngrx/store';
import { loadUser, removeUser } from './user.actions';
import { User } from '../../models/User';

const initialState: User = {
  id: '',
  firstName: '',
  lastName: '',
  dateOfBirth: new Date(),
  email: '',
  roles: [],
};

export const userReducer = createReducer(
  initialState,
  on(loadUser, (state, { payload }) => payload),
  on(removeUser, (state) => initialState)
);
