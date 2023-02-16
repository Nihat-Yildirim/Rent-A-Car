using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    public class DataResult<TEntity> : Result, IDataResult<TEntity>
    {
        public DataResult(bool success, TEntity data, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(bool success, TEntity data) : base(success)
        {
            Data = data;
        }

        public TEntity Data { get; }
    }
}
