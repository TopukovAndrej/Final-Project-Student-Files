import { StudentFilesConstants } from './constants';

export class StudentFilesErrorMessages {
  public static readonly SIGN_IN_FORM_PASSWORD_MIN_LENGTH_ERROR = `The password must contain at least ${StudentFilesConstants.PasswordMinLength} characters`;
  public static readonly SIGN_IN_FORM_PASSWORD_MIN_LETTERS_ERROR = `The password must contain at least ${StudentFilesConstants.PasswordMinNumberOfLetters} letters`;
  public static readonly SIGN_IN_FORM_PASSWORD_MIN_DIGITS_ERROR = `The password must contain at least ${StudentFilesConstants.PasswordMinNumberOfDigits} digits`;
  public static readonly SIGN_IN_FORM_PASSWORD_MIN_SPECIAL_CHARACTERS_ERROR = `The password must contain at least ${StudentFilesConstants.PasswordMinNumberOfSpecialCharacters} special characters`;
}
