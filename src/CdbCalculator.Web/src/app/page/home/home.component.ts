import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CalculatorService } from '../../services/calculator.service';
import { CalculatorRequest } from '../../models/CalculatorRequest';
import { CalculatorResponse } from '../../models/CalculatorResponse';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  cdbCalculateForm: FormGroup;
  result: CalculatorResponse | null = null;

  constructor(private formBuilder: FormBuilder,  private serviceCalculate: CalculatorService)
  {
    this.cdbCalculateForm = this.formBuilder.group({
      InitialValue: [null, [Validators.required, Validators.min(0.01)]],
      DeadlineMonths: [null, [Validators.required, Validators.min(1)]],
    });
  }

  cdbCalculator(): void {
    if (this.cdbCalculateForm.valid) {
      const request: CalculatorRequest = this.cdbCalculateForm.value;
      this.serviceCalculate.Calculator(request).subscribe((response) => {
        this.result = response;
      });
    }
  }
}
