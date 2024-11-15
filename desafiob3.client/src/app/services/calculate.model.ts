export interface CalculationResponse {
  grossAmount: number;
  netAmount: number;
}

export interface CalculationRequest {
  initialValue: number;
  quantity: number;
}
