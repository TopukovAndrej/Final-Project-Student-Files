export interface IDecodedUserToken {
  sub: string;
  email: string;
  role: string;
  jti: string;
  iat: number;
  exp: number;
  iss?: string;
  aud?: string;
}
