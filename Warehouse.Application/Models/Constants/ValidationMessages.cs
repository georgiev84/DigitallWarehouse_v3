namespace Warehouse.Application.Models.Constants;

public static class ValidationMessages
{
    public const string MinPriceGreaterThanZero = "MinPrice must be greater than zero when provided";
    public const string MinPriceValidDecimal = "MinPrice must be a valid decimal number when provided";
    public const string MaxPriceGreaterThanZero = "MaxPrice must be greater than zero when provided";
    public const string MaxPriceValidDecimal = "MaxPrice must be a valid decimal number when provided";
    public const string SizeMaxLength = "Size length must not exceed 50 characters";
}
