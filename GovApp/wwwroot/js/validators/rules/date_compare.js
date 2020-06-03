import { toDate } from '../utils';
/**
 * Compares the selected date if earlier or beyond from the date provided
 */
export default ({ value, compare, validationType }) => {
    let pass = true;

    if (toDate(value) instanceof Date && toDate(compare) instanceof Date) {
        let valueDate = toDate(value);
        let compDate = toDate(compare)

        pass = validateDate(valueDate, compDate, validationType);
    }

    if (Array.isArray(value) && toDate(compare) instanceof Date) {
        let compDate = toDate(compare);

        for (let i = 0; i < value.length; i++) {
            let valueDate = toDate(value[i]);

            pass = validateDate(valueDate, compDate, validationType);

            if (!pass) return pass;
        }
    }

    if (Array.isArray(compare) && toDate(value) instanceof Date) {
        let valueDate = toDate(value);

        for (let i = 0; i < compare.length; i++) {
            let compDate = toDate(compare[i]);

            pass = validateDate(valueDate, compDate, validationType);

            if (!pass) return pass;
        }
    }

    return pass;
}

const validateDate = (value, compare, validationType) => {
    let pass = true;

    if (validationType === 'earlier') {
        pass = isEarlier(value, compare);
    }

    if (validationType === 'beyond') {
        pass = isBeyond(value, compare);
    }

    return pass;
}

const isEarlier = (value, compare) => {
    return (value < compare) ? false : true;
}

const isBeyond = (value, compare) => {
    return (value > compare) ? false : true;
}