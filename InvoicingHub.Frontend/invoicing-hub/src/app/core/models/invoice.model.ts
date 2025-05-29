export interface InvoiceDetail {
  productId: number;
  unitPrice: number;
  quantity: number;
  notes: string;
}

export interface InvoiceRequest {
  customerId: number;
  invoiceNumber: number;
  details: InvoiceDetail[];
}
