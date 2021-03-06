﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SpecExpress.Rules.StringValidators
{   
    public class MinLength<T> : RuleValidator<T, string>
    {
        public MinLength(int min)
        {
            if (min < 0)
            {
                throw new ArgumentOutOfRangeException("min", "Min should be greater than 0");
            }
            Params.Add(new RuleParameter("min", min));
        }

        public MinLength(Expression<Func<T, int>> expression)
        {
            Params.Add(new RuleParameter("min", expression));
        }

        public override bool Validate(RuleValidatorContext<T, string> context, SpecificationContainer specificationContainer, ValidationNotification notification)
        {

            int length = String.IsNullOrEmpty(context.PropertyValue) ? 0 : context.PropertyValue.Length;

            var contextWithLength = new RuleValidatorContext<T, string>(context.Instance, context.PropertyName, length.ToString(),
                                                                           context.PropertyInfo, context.Level, null);

            var min = (int)Params[0].GetParamValue(context);
            
            return Evaluate(length >= min, contextWithLength, notification);
        }
    }
}
