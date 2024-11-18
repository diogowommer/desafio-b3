export interface CalculationResponse {
  grossAmount: number;
  netAmount: number;
  errorMessage: string;
}

export interface CalculationRequest {
  initialValue: number;
  quantity: number;
}
