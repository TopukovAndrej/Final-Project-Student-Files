import { ResultType } from '../models/result-type.model';
import { IError } from './IError';

export interface IBaseResult {
  isSuccess: boolean;
  isFailure: boolean;
  error?: IError;
  type: ResultType;
}

export interface IResult<T> extends IBaseResult {
  value?: T;
}
