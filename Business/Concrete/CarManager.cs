﻿using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal= carDal;
        }

        public IResult Add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> GeyById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id==id));
        }

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }
    }
}
