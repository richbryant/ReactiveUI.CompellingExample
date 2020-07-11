using System;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompellingExample.Blazor.Server.Extensions
{
    public static class ExtensionsForMvc
    {
        private static IActionResult GetGenericErrorResult(Exception ex) =>
            ex is AggregateException aggregate ? GetInternalServerErrorResult(aggregate.InnerExceptions)
            : GetInternalServerErrorResult(ex);

        private static IActionResult GetGenericErrorResult<T>(T error) =>
            error is Exception ex ? GetGenericErrorResult(ex)
            : GetInternalServerErrorResult(error);

        private static IActionResult GetGenericSuccessResult<T>(T value) =>
            value is Unit ? GetNoContentResult()
            : new OkObjectResult(value);

        private static IActionResult GetInternalServerErrorResult(object value) =>
            new ObjectResult(value) { StatusCode = StatusCodes.Status500InternalServerError };

        private static IActionResult GetNoContentResult() => new NoContentResult();

        private static IActionResult GetNotFoundResult() => new NotFoundResult();

        public static IActionResult ToActionResult<T>(this Try<T> self, Func<T, IActionResult> succ = null, Func<Exception, IActionResult> fail = null) =>
            self.Match(Succ: succ ?? GetGenericSuccessResult, Fail: fail ?? GetGenericErrorResult);

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> self, Func<T, IActionResult> succ = null, Func<Exception, IActionResult> fail = null) =>
            self.Match(Succ: succ ?? GetGenericSuccessResult, Fail: fail ?? GetGenericErrorResult);

        public static IActionResult ToActionResult<TL, TR>(this Either<TL, TR> self, Func<TR, IActionResult> right = null, Func<TL, IActionResult> left = null) =>
            self.Match(Right: right ?? GetGenericSuccessResult, Left: left ?? GetGenericErrorResult);

        public static Task<IActionResult> ToActionResult<TL, TR>(this EitherAsync<TL, TR> self, Func<TR, IActionResult> right = null, Func<TL, IActionResult> left = null) =>
            self.Match(Right: right ?? GetGenericSuccessResult, Left: left ?? GetGenericErrorResult);

        public static IActionResult ToActionResult<TA>(this Option<TA> self, Func<TA, IActionResult> some = null, Func<IActionResult> none = null) =>
            self.Match(Some: some ?? GetGenericSuccessResult, None: none ?? GetNotFoundResult);

        public static Task<IActionResult> ToActionResult<TA>(this OptionAsync<TA> self, Func<TA, IActionResult> some = null, Func<IActionResult> none = null) =>
            self.Match(Some: some ?? GetGenericSuccessResult, None: none ?? GetNotFoundResult);
    }
}