import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { StudentFilesConstants } from './constants';
import { StudentFilesErrorMessages } from './error-messages';

export class StudentFilesFormValidators {
  public static signInPasswordFormControlValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.value == null) {
        return null;
      }

      const password: string = control.value as string;

      if (password.length < StudentFilesConstants.PasswordMinLength) {
        return {
          invalid: true,
          errorMessage:
            StudentFilesErrorMessages.SIGN_IN_FORM_PASSWORD_MIN_LENGTH_ERROR,
        };
      }

      let numberOfDigits = 0;
      let numberOfLetters = 0;
      let numberOfSpecialCharacters = 0;

      for (let character of password) {
        if (character >= '0' && character <= '9') {
          numberOfDigits++;
        }

        if (/[a-zA-Z]/.test(character)) {
          numberOfLetters++;
        }

        if (/[`~@#$%^&*()_+}{":?><}!]/.test(character)) {
          numberOfSpecialCharacters++;
        }
      }

      if (numberOfDigits < StudentFilesConstants.PasswordMinNumberOfDigits) {
        return {
          invalid: true,
          errorMessage:
            StudentFilesErrorMessages.SIGN_IN_FORM_PASSWORD_MIN_DIGITS_ERROR,
        };
      }

      if (numberOfLetters < StudentFilesConstants.PasswordMinNumberOfLetters) {
        return {
          invalid: true,
          errorMessage:
            StudentFilesErrorMessages.SIGN_IN_FORM_PASSWORD_MIN_LETTERS_ERROR,
        };
      }

      if (
        numberOfSpecialCharacters <
        StudentFilesConstants.PasswordMinNumberOfSpecialCharacters
      ) {
        return {
          invalid: true,
          errorMessage:
            StudentFilesErrorMessages.SIGN_IN_FORM_PASSWORD_MIN_SPECIAL_CHARACTERS_ERROR,
        };
      }

      return null;
    };
  }
}
