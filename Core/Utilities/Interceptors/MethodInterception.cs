using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        public virtual void OnBefore(IInvocation ınvocation) { }
        public virtual void OnAfter(IInvocation ınvocation) { }
        public virtual void OnException(IInvocation ınvocation, Exception exception) { }
        public virtual void OnSuccess(IInvocation ınvocation) { }

        public override void Intercept(IInvocation ınvocation)
        {
            bool isSuccess = true;
            OnBefore(ınvocation);
            try
            {
                ınvocation.Proceed();
            }
            catch (Exception exception)
            {
                isSuccess = false;
                OnException(ınvocation, exception);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(ınvocation);
                }
            }
            OnAfter(ınvocation);
        }

    }
}
