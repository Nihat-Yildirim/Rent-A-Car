using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<TEntity> : DataResult<TEntity>
    {
        public SuccessDataResult(TEntity data, string message) : base(true, data, message)
        {

        }

        public SuccessDataResult(TEntity data) : base(true, data)
        {

        }

        public SuccessDataResult(string message) : base(true, default, message)
        {

        }

        public SuccessDataResult() : base(true, default)
        {

        }
    }
}
