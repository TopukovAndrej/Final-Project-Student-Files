export class StudentFilesConstants {
  // Sign In form Password
  public static readonly PasswordMinNumberOfLetters = 3;
  public static readonly PasswordMinNumberOfDigits = 3;
  public static readonly PasswordMinNumberOfSpecialCharacters = 1;
  public static readonly PasswordMinLength =
    this.PasswordMinNumberOfLetters +
    this.PasswordMinNumberOfDigits +
    this.PasswordMinNumberOfSpecialCharacters;

  // Messages
  public static readonly PleaseLoginMessage =
    'Please log in to use the application.';
  public static readonly WelcomeAdminMessage =
    'Welcome, Admin! Have a productive day!';
  public static readonly DeleteConfirmationMessage =
    'Are you sure you want to delete the selected user?';
}
