import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../../core/services/customer.service';
import { InvoiceService } from '../../../../core/services/invoice.service';
import { Customer } from '../../../../core/models/customer.model';
import { InvoiceSearchResult } from '../../../../core/models/invoice-search.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-invoices-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule
  ],
  templateUrl: './invoices-list.component.html',
  styleUrls: ['./invoices-list.component.scss']
})
export class InvoicesListComponent implements OnInit {
  customers: Customer[] = [];
  result: InvoiceSearchResult[] = [];

  searchType: 'cliente' | 'factura' = 'cliente';
  customerId: number = 0;
  invoiceNumber: number = 0;

  constructor(
    private customerService: CustomerService,
    private invoiceService: InvoiceService
  ) {}

  ngOnInit(): void {
    this.customerService.getAll().subscribe(res => this.customers = res);
  }

  onSearchTypeChange(): void {
    this.result = [];
    this.customerId = 0;
    this.invoiceNumber = 0;
  }

buscar(): void {
  if (this.searchType === 'cliente') {
    this.invoiceService.getInvoicesByCustomer(this.customerId).subscribe(res => {
      console.log('Resultado cliente:', res);
      this.result = res.data || [];
    });
  } else {
    this.invoiceService.getInvoice(this.invoiceNumber.toString()).subscribe(res => {
      console.log('Resultado factura:', res);
      this.result = res.data ? [res.data] : [];
    });
  }
}




}