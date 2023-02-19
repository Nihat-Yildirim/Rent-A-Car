using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatortype;
        public ValidationAspect(Type validatortype)
        {
            if(!typeof(IValidator).IsAssignableFrom(validatortype))
            {
                throw new System.Exception("Bu bir doğrulama sını değil");
            }
            _validatortype = validatortype;
        }

        public override void OnBefore(IInvocation ınvocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatortype);
            var entityType = _validatortype.BaseType.GetGenericArguments()[0];
            var entities = ınvocation.Arguments.Where(t => t.GetType() == entityType);
            foreach( var entity in entities )
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
