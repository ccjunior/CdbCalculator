import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { CalculatorResponse } from '../models/CalculatorResponse';
import { CalculatorRequest } from '../models/CalculatorRequest';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {

  ApiUrl = environment.urlApi;
  
  constructor(private http: HttpClient) { }

  Calculator(data: CalculatorRequest) : Observable<CalculatorResponse>{
    return this.http.post<CalculatorResponse>(`${this.ApiUrl}/calculate`, data);
  }
}
