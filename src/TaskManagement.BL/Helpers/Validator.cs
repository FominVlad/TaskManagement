using TaskManagement.Core.Exceptions;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.BL.Helpers;

public class Validator : IValidator
{
    #region Public Methods

    public IValidator CheckNumberIsNotZeroOrNegative(
        int number,
        string numberName,
        string? customErrorMessage = null)
    {
        if (number == 0)
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"Number {numberName} should not be zero.");
        }

        if (number < 0)
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"Number {numberName} should be greater than zero.");
        }

        return this;
    }

    public IValidator CheckObjectIsNotNull(
        object? obj,
        string objectName,
        string? customErrorMessage = null)
    {
        if (obj == null)
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"Object {objectName} should not be null.");
        }

        return this;
    }

    public IValidator CheckStringIsNotNullOrWhiteSpace(
        string? str,
        string strName,
        string? customErrorMessage = null)
    {
        if (str == null)
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"String {strName} should not be null.");
        }

        if (string.IsNullOrEmpty(str))
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"String {strName} should not be empty.");
        }

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"String {strName} should not be white space.");
        }

        return this;
    }

    public IValidator CheckValueExistsInEnum<T>(
        T value,
        Type enumType,
        string valueName,
        string? customErrorMessage = null)
    {
        if (!Enum.IsDefined(enumType, value))
        {
            throw new InvalidArgumentException(customErrorMessage ?? $"Value {valueName} is not existing in enum.");
        }

        return this;
    }

    #endregion
}