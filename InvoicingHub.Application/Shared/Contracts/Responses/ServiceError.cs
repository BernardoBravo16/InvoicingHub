namespace InvoicingHub.Application.Shared.Contracts.Responses
{
    public class ServiceError
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string? ErrorDetail { get; set; }
        public ServiceErrorTypeEnum ErrorType { get; set; }

        public ServiceError(Exception ex)
        {
            ErrorMessage = ex.Message;
            ErrorDetail = ex.StackTrace;
            ErrorType = ServiceErrorTypeEnum.EXCEPTION_ERROR;
        }

        public ServiceError(string message)
        {
            ErrorMessage = message;
            ErrorDetail = null;
            ErrorType = ServiceErrorTypeEnum.VALIDATION_ERROR;
        }
    }
}