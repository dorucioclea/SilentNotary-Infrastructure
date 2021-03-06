﻿using SilentNotary.Common.Entity.Uow;
using SilentNotary.Common.Query.Criterion.Abstract;
using System;
using System.Linq;

namespace SilentNotary.Common.Query
{
    [Obsolete("use specifications way")]
    public class ExpressionQuery
    {
        private readonly IDataSetUow _dataSetUow;

        public ExpressionQuery(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public IQueryable<T> Ask<T>(IExpressionCriterion<T> criterion) where T : class, IHasKey
        {
            return _dataSetUow
                .GetQuery<T>()
                .Where(criterion.Get());
        }
    }
}