import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = 'http://localhost:5022/api';

  constructor(private readonly httpClient: HttpClient) {}

  get<T>(route: string): Observable<T> {
    return this.httpClient.get<T>(`${this.apiUrl}${route}`);
  }

  post<T>(route: string, data: any): Observable<T> {
    return this.httpClient.post<T>(`${this.apiUrl}${route}`, data);
  }
}
