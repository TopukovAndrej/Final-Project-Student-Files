export class StudentFilesConstants {
  // Sign In form Password
  public static readonly PasswordMinNumberOfLetters = 3;
  public static readonly PasswordMinNumberOfDigits = 3;
  public static readonly PasswordMinNumberOfSpecialCharacters = 1;
  public static readonly PasswordMinLength =
    this.PasswordMinNumberOfLetters +
    this.PasswordMinNumberOfDigits +
    this.PasswordMinNumberOfSpecialCharacters;
}
