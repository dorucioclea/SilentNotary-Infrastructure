﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace SilentNotary.FunctionalCSharp
{
    public static class ResultExtensions
    {
        public static Result OnAllSuccess(this IEnumerable<Result> results, Action action)
        {
            return Result.Combine(results.ToArray()).OnSuccess(action);
        }

        public static Result<T> OnAllSuccess<T>(this IEnumerable<Result> results, Func<T> func)
        {
            return Result.Combine(results.ToArray()).OnSuccess(func);
        }


        public static Result OnAllSuccess<TInput, TOutput>(this IEnumerable<Result<TInput>> results, Func<IEnumerable<TInput>, Result> func)
        {
            var resultArray = results.ToArray();
            var errorResults = resultArray.Where(r => r.IsFailure).ToArray();
            if (errorResults.Any())
                return Result.Fail<TOutput>(string.Join(", ", errorResults.Select(x => x.Error).ToArray()));
            var resultValues = resultArray.Select(r => r.Value);
            return func(resultValues);
        }

        public static Result<TOutput> OnAllSuccess<TInput, TOutput>(this IEnumerable<Result<TInput>> results, Func<IEnumerable<TInput>, Result<TOutput>> func)
        {
            var resultArray = results.ToArray();
            var errorResults = resultArray.Where(r => r.IsFailure).ToArray();
            if (errorResults.Any())
                return Result.Fail<TOutput>(string.Join(", ", errorResults.Select(x => x.Error).ToArray()));
            var resultValues = resultArray.Select(r => r.Value);
            return func(resultValues);
        }

        public static async Task<Result> OnAllSuccess<TInput>(this IEnumerable<Result<TInput>> results, Func<IEnumerable<TInput>, Task<Result>> func)
        {
            var resultArray = results.ToArray();
            var errorResults = resultArray.Where(r => r.IsFailure).ToArray();
            if (errorResults.Any())
                return Result.Fail(string.Join(", ", errorResults.Select(x => x.Error).ToArray()));
            var resultValues = resultArray.Select(r => r.Value);
            return await func(resultValues);
        }
        public static async Task<TOutput> OnTaskSuccess<TOutput>(this Task<Result> resulTask, Func<TOutput> func)
        {
            var result = await resulTask.ConfigureAwait(false);
            if (result.IsSuccess)
                return func();
            return default(TOutput);
        }

        public static Result<TOutput> OnBoth<TInput, TOutput>(this IEnumerable<Result<TInput>> results, Func<IEnumerable<TInput>, Result<TOutput>> func)
        {
            var resultArray = results.ToArray();
            var resultValues = resultArray.Select(r => r.Value);
            return func(resultValues);
        }

    }
}
