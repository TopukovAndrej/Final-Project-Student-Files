import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = 'API_URL'; // TODO: To be changed with actual API url

  constructor(private readonly httpClient: HttpClient) {}
}
