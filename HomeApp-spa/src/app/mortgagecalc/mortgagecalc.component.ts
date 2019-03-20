import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-mortgagecalc',
  templateUrl: './mortgagecalc.component.html',
  styleUrls: ['./mortgagecalc.component.css']
})
export class MortgagecalcComponent implements OnInit {

  @ViewChild('mortgageForm') mortgageForm: NgForm;
  mPayment: string;
  amount: number;
  downPayment = 0;
  interestRate: number;
  loanType = 30;
  loanTypes = [{ value: 30, display: '30 year fixed' }, { value: 20, display: '20 year fixed' },
  { value: 15, display: '15 year fixed' }, { value: 10, display: '10 year fixed' }];

  constructor() { }

  ngOnInit() {
  }

  calculate() {
    if (this.mortgageForm.invalid) {
      this.mortgageForm.controls.amount.markAsTouched();
      this.mortgageForm.controls.downPayment.markAsTouched();
      this.mortgageForm.controls.interestRate.markAsTouched();
      return;
    }

    const r = (this.interestRate / 100) / 12;
    const n = this.loanType * 12;
    const numerator = r * (Math.pow(1 + r, n));
    const denominator = (Math.pow(1 + r, n)) - 1;
    const result = (this.amount - this.downPayment) * (numerator / denominator);
    this.mPayment = result.toFixed(2);
  }

}
