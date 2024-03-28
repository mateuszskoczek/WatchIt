using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WatchIt.Shared.Models
{
    public class RequestResult
    {
        #region PROPERTIES

        public RequestResultStatus Status { get; }
        public IEnumerable<string> ValidationMessages { get; init; }

        #endregion



        #region CONSTRUCTORS

        internal RequestResult(RequestResultStatus status) => Status = status;

        public static RequestResult Ok() => new RequestResult(RequestResultStatus.Ok);
        public static RequestResult<T> Ok<T>() => new RequestResult<T>(RequestResultStatus.Ok);
        public static RequestResult<T> Ok<T>(T data) => new RequestResult<T>(RequestResultStatus.Ok) { Data = data };
        public static RequestResult<T> Created<T>(string location, T resource) => new RequestResult<T>(RequestResultStatus.Created) { NewResourceLocation = location, Data = resource };
        public static RequestResult NoContent() => new RequestResult(RequestResultStatus.NoContent);
        public static RequestResult<T> NoContent<T>() => new RequestResult<T>(RequestResultStatus.NoContent);
        public static RequestResult BadRequest(params string[] validationErrors) => new RequestResult(RequestResultStatus.BadRequest) { ValidationMessages = validationErrors };
        public static RequestResult<T> BadRequest<T>(params string[] validationErrors) => new RequestResult<T>(RequestResultStatus.BadRequest) { ValidationMessages = validationErrors };
        public static RequestResult Unauthorized(params string[] validationErrors) => new RequestResult(RequestResultStatus.Unauthorized) { ValidationMessages = validationErrors };
        public static RequestResult<T> Unauthorized<T>(params string[] validationErrors) => new RequestResult<T>(RequestResultStatus.Unauthorized) { ValidationMessages = validationErrors };
        public static RequestResult Forbidden() => new RequestResult(RequestResultStatus.Forbidden);
        public static RequestResult<T> Forbidden<T>() => new RequestResult<T>(RequestResultStatus.Forbidden);
        public static RequestResult NotFound() => new RequestResult(RequestResultStatus.NotFound);
        public static RequestResult<T> NotFound<T>() => new RequestResult<T>(RequestResultStatus.NotFound);
        public static RequestResult Conflict() => new RequestResult(RequestResultStatus.Conflict);
        public static RequestResult<T> Conflict<T>() => new RequestResult<T>(RequestResultStatus.Conflict);

        #endregion



        #region CONVERSION

        public static implicit operator ActionResult(RequestResult result) => result.Status switch
        {
            RequestResultStatus.Ok => HandleOk(result),
            RequestResultStatus.NoContent => HandleNoContent(),
            RequestResultStatus.BadRequest => HandleBadRequest(result),
            RequestResultStatus.Unauthorized => HandleUnauthorized(result),
            RequestResultStatus.Forbidden => HandleForbidden(),
            RequestResultStatus.NotFound => HandleNotFound(),
            RequestResultStatus.Conflict => HandleConflict(),
        };

        protected static ActionResult HandleOk(RequestResult result) => new OkResult();
        protected static ActionResult HandleNoContent() => new NoContentResult();
        protected static ActionResult HandleBadRequest(RequestResult result) => new BadRequestObjectResult(result.ValidationMessages);
        protected static ActionResult HandleUnauthorized(RequestResult result) => new UnauthorizedObjectResult(result.ValidationMessages);
        protected static ActionResult HandleForbidden() => new ForbidResult();
        protected static ActionResult HandleNotFound() => new NotFoundResult();
        protected static ActionResult HandleConflict() => new ConflictResult();

        #endregion
    }

    public class RequestResult<T> : RequestResult
    {
        #region PROPERTIES

        public T? Data { get; init; }
        public string? NewResourceLocation { get; init; }

        #endregion



        #region CONSTRUCTORS

        internal RequestResult(RequestResultStatus type) : base(type) { }

        #endregion



        #region CONVERSION

        public static implicit operator ActionResult(RequestResult<T> result) => result.Status switch
        {
            RequestResultStatus.Ok => HandleOk(result),
            RequestResultStatus.Created => HandleCreated(result),
            RequestResultStatus.NoContent => HandleNoContent(),
            RequestResultStatus.BadRequest => HandleBadRequest(result),
            RequestResultStatus.Unauthorized => HandleUnauthorized(result),
            RequestResultStatus.Forbidden => HandleForbidden(),
            RequestResultStatus.NotFound => HandleNotFound(),
            RequestResultStatus.Conflict => HandleConflict(),
        };

        private static ActionResult HandleOk(RequestResult<T> result) => result.Data is null ? new OkResult() : new OkObjectResult(result.Data);
        private static ActionResult HandleCreated(RequestResult<T> result) => new CreatedResult(result.NewResourceLocation, result.Data);

        #endregion
    }
}
