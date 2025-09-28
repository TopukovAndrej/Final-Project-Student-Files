import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = 'http://localhost:5022/api';

  constructor(private readonly httpClient: HttpClient) {}

  post<T>(route: string, data: any): Observable<T> {
    return this.httpClient.post<T>(`${this.apiUrl}${route}`, data);
  }
}
