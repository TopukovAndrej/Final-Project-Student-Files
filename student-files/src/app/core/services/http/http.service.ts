import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = 'http://localhost:5022/api';

  constructor(private readonly httpClient: HttpClient) {}
}
