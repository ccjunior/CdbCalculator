import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CalculatorService } from '../../services/calculator.service';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { SimulatorComponent } from './simulator.component';

describe('HomeComponent', () => {
  let component: SimulatorComponent;
  let fixture: ComponentFixture<SimulatorComponent>;
  let cdbService: jasmine.SpyObj<CalculatorService>;

  beforeEach(async () => {
    const cdbServiceSpy = jasmine.createSpyObj('CalculatorService', ['Calculator']);

    await TestBed.configureTestingModule({
      declarations: [SimulatorComponent],
      imports: [ReactiveFormsModule],
      providers: [
        { provide: CalculatorService, useValue: cdbServiceSpy },
        provideHttpClientTesting(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(SimulatorComponent);
    component = fixture.componentInstance;
    cdbService = TestBed.inject(CalculatorService) as jasmine.SpyObj<CalculatorService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a valid form when inputs are valid', () => {
    component.cdbCalculateForm.controls['InitialValue'].setValue(1000);
    component.cdbCalculateForm.controls['DeadlineMonths'].setValue(12);
    expect(component.cdbCalculateForm.valid).toBeTrue();
  });

  it('should have an invalid form when inputs are invalid', () => {
    component.cdbCalculateForm.controls['InitialValue'].setValue(null);
    component.cdbCalculateForm.controls['DeadlineMonths'].setValue(null);
    expect(component.cdbCalculateForm.invalid).toBeTrue();
  });

  it('should call Calculator and set result when form is valid', () => {
    const mockResponse = { grossValue: 1123.08, netValue: 1098.46 };
    cdbService.Calculator.and.returnValue(of(mockResponse));

    component.cdbCalculateForm.controls['InitialValue'].setValue(1000);
    component.cdbCalculateForm.controls['DeadlineMonths'].setValue(12);
    component.cdbCalculator();

    expect(cdbService.Calculator).toHaveBeenCalledWith({
      InitialValue: 1000,
      DeadlineMonths: 12,
    });
    expect(component.result).toEqual(mockResponse);
  });

  it('should not call Calculator if the form is invalid', () => {
    component.cdbCalculateForm.controls['InitialValue'].setValue(null); // Form inválido
    component.cdbCalculateForm.controls['DeadlineMonths'].setValue(12);

    component.cdbCalculator();

    expect(cdbService.Calculator).not.toHaveBeenCalled();
  });
});
