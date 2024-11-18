import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { enviroment } from '../../environments/environment';
import { CalculationRequest, CalculationResponse } from './calculate.model';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CalculateAmountService {
  baseUrl: string = enviroment.apiUrl;
  constructor(private readonly httpClient: HttpClient) { }

  calculateAmount(data: CalculationRequest): Observable<CalculationResponse> {
    return this.httpClient.post<CalculationResponse>(`${this.baseUrl}/api/calculate-amount`, data).pipe(
      catchError(error => {
        const errorResponse: CalculationResponse = {
          grossAmount: 0,
          netAmount: 0,
          errorMessage: error?.error[0]?.errorMessage || 'Erro desconhecido!',
        };

        return of(errorResponse);
      }));
  }

}
