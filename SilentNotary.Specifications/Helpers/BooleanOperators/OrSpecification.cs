﻿using System;
using System.Linq.Expressions;
using SilentNotary.Specifications.Helpers.ExpressionCombining;

namespace SilentNotary.Specifications.Helpers.BooleanOperators
{
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec1;
        private readonly Specification<T> _spec2;

        internal OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            _spec1 = spec1 ?? throw new ArgumentException(nameof(spec1));
            _spec2 = spec2 ?? throw new ArgumentException(nameof(spec2));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = _spec1.ToExpression();
            var expr2 = _spec2.ToExpression();
            return expr1.OrElse(expr2);
        }
    }
}
