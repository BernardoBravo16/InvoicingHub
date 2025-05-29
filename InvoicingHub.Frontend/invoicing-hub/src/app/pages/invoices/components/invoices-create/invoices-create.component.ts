import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';

import { InvoiceRequest, InvoiceDetail } from '../../../../core/models/invoice.model';
import { Customer } from '../../../../core/models/customer.model';
import { Product } from '../../../../core/models/product.model';

import { InvoiceService } from '../../../../core/services/invoice.service';
import { CustomerService } from '../../../../core/services/customer.service';
import { ProductService } from '../../../../core/services/product.service';

@Component({
  selector: 'app-invoices-create',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule
  ],
  templateUrl: './invoices-create.component.html',
  styleUrls: ['./invoices-create.component.scss']
})
export class InvoicesCreateComponent {
  customers: Customer[] = [];
  products: Product[] = [];
  total: number = 0;

  invoice: InvoiceRequest = {
    customerId: 0,
    invoiceNumber: 0,
    details: []
  };

  editQuantityIndex: number | null = null;

  constructor(
    private invoiceService: InvoiceService,
    private customerService: CustomerService,
    private productService: ProductService
  ) {
    this.loadData();
  }

  loadData(): void {
    this.customerService.getAll().subscribe(c => this.customers = c);
    this.productService.getAll().subscribe(p => this.products = p);
  }

  getProduct(productId: number): Product | undefined {
    return this.products.find(p => p.id === productId);
  }

  getProductImage(productId: number): string | null {
  const product = this.products.find(p => p.id === productId);
  return product?.image ? `data:image/jpeg;base64,${product.image}` : null;
}

  addDetail(): void {
    this.invoice.details.push({
      productId: 0,
      unitPrice: 0,
      quantity: 1,
      notes: ''
    });
  }

onProductSelect(detail: InvoiceDetail): void {
  const product = this.products.find(p => p.id === detail.productId);
  if (product) {
    detail.unitPrice = product.unitPrice;
    detail.quantity = 1;
    this.updateTotal();
  }
}

  enableEdit(index: number): void {
    this.editQuantityIndex = index;
  }

  disableEdit(): void {
    this.editQuantityIndex = null;
    this.updateTotal();
  }

  removeDetail(index: number): void {
    this.invoice.details.splice(index, 1);
    this.updateTotal();
  }

updateTotal(): void {
  this.total = this.invoice.details.reduce((sum, item) => sum + (item.unitPrice * item.quantity), 0);
}

  createInvoice(): void {
    this.invoiceService.createInvoice(this.invoice).subscribe({
      next: () => {
        alert('Factura creada exitosamente');
        this.invoice = {
          customerId: 0,
          invoiceNumber: 0,
          details: []
        };
        this.total = 0;
      },
      error: err => alert('Error al crear factura: ' + err.message)
    });
  }
}
