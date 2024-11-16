namespace TaskManagement.Core.Interfaces;

public interface IValidator
{
    IValidator CheckStringIsNotNullOrWhiteSpace(
        string str,
        string strName,
        string? customErrorMessage = null);

    IValidator CheckObjectIsNotNull(
        object obj,
        string objectName,
        string? customErrorMessage = null);

    IValidator CheckNumberIsNotZeroOrNegative(
        int number,
        string numberName,
        string? customErrorMessage = null);

    IValidator CheckValueExistsInEnum<T>(
        T value,
        Type enumType,
        string valueName,
        string? customErrorMessage = null);
}