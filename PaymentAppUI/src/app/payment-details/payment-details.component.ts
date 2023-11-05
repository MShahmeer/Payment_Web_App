import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { PaymentDetail } from '../shared/payment-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.css'],
})
export class PaymentDetailsComponent implements OnInit {
  constructor(
    public paymentDetailService: PaymentDetailService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.paymentDetailService.refreshList();
  }

  populateForm(selectedRecord: PaymentDetail) {
    this.paymentDetailService.formData = Object.assign({}, selectedRecord); //copy of selectedRecord is created and is assigned to formData
  }

  onDelete(id: number) {
    if (confirm('Do You Want To Delete This Record?')) {
      this.paymentDetailService.deletePaymentDetail(id).subscribe({
        next: (res) => {
          this.paymentDetailService.list = res as PaymentDetail[];
          this.toastr.error('Deleted Successfully', 'Payment Detail Register');
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
