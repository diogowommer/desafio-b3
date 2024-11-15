import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { enviroment } from '../../environments/environment';
import { CalculationRequest, CalculationResponse } from './calculate.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalculateAmountService {
  baseUrl: string = enviroment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  calculateAmount(data: CalculationRequest): Observable<CalculationResponse> {
    return this.httpClient.post<CalculationResponse>(`${this.baseUrl}/CalculateAmount`, data);
  }

}
