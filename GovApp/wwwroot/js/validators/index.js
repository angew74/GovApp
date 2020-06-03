import { extend } from 'vee-validate';

import {
    email,
    required,
    password
} from 'vee-validate/dist/rules';
extend('email', {
    ...email,
    message: 'You should add a valid email address'
});


extend('required', {
    ...required,
    message: 'Il campo {_field_} è obbligatorio.'
});


extend('is_earlier', {
    validate: (value, { compare }) => {
        return date_compare({ value, compare, validationType: 'earlier' });
    },
    params: ['compare', 'dateType'],
    message: 'The selected date must not be earlier than {dateType}'
});

extend('is_beyond', {
    validate: (value, { compare }) => {
        return date_compare({ value, compare, validationType: 'beyond' });
    },
    params: ['compare', 'dateType'],
    message: 'The selected date must not be older than {dateType}'
});

extend('confirm_password', {
    params: ['target'],
    validate(value, { target }) {
        return value === target;
    },
    message: 'La conferma password non coincide con la conferma password'
});