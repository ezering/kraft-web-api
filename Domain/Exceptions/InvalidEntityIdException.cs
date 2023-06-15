using System.Net;
using Domain.Abstractions;

namespace Domain.Exceptions;

public sealed class InvalidEntityIdException : Exception, IServiceException
{
    private object Id { get; }
    
    public InvalidEntityIdException(object id)
    {
        Id = id;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage => $"Cannot set: {Id}  as entity identifier.";
}