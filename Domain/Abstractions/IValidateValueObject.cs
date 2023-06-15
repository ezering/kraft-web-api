using Domain.Primitives;
using ErrorOr;

namespace Domain.Abstractions;

public interface IValidateValueObject<T> where T : ValueObject
{
    ErrorOr<T> ValidateValueObject();
}