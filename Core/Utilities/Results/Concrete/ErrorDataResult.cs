using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<TEntity> : DataResult<TEntity>
    {
        public ErrorDataResult(TEntity data,string message) : base(false,data,message)
        {

        }

        public ErrorDataResult(TEntity data):base(false,data)
        {

        }

        public ErrorDataResult(string message):base(false,default,message)
        {

        }

        public ErrorDataResult() : base(false,default) 
        {

        }
    }
}
