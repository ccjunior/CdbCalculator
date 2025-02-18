import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { CalculatorService } from './calculator.service';
import { environment } from '../../environments/environment.development';
import { CalculatorRequest } from '../models/CalculatorRequest';
import { CalculatorResponse } from '../models/CalculatorResponse';
import { provideHttpClient } from '@angular/common/http';

describe('CalculatorService', () => {
  let service: CalculatorService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CalculatorService, provideHttpClient(), provideHttpClientTesting()]
    });

    service = TestBed.inject(CalculatorService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call API and return a CalculatorResponse', () => {
    const mockRequest: CalculatorRequest = {
      InitialValue: 1000,
      DeadlineMonths: 12
    };

    const mockResponse: CalculatorResponse = {
      grossValue: 1200,
      netValue: 1180
    };

    service.Calculator(mockRequest).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${environment.urlApi}/calculate`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockRequest);
    
    req.flush(mockResponse);
  });
});
