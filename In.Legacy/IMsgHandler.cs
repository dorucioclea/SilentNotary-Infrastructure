﻿using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace In.Legacy
{
    public interface IMsgHandler<in T> where T: IMessage
    {
        Task<Result> Handle(T message);
    }

    public interface IMsgHandler<in TInput, TOutput> where TInput : IMessage
    {
        Task<Result<TOutput>> Handle(TInput message);
    }
}