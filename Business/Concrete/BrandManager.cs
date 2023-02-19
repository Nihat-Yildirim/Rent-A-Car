﻿using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal= brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand entity)
        {
            _brandDal.Add(entity);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b=> b.Id == id));
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult();
        }
    }
}
