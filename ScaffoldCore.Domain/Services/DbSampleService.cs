﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Data;
using Serilog;
using ScaffoldCore.Infrastructure.Exceptions;
using ScaffoldCore.Domain.Base;
using ScaffoldCore.Infrastructure.Factories;
using System.Linq;
using ScaffoldCore.Domain.BindingModels;
using ScaffoldCore.Domain.Entities;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ScaffoldCore.Domain.Services
{
	public class DbSampleService : BaseService
	{
		private IDbConnection Connection;

		public DbSampleService(IDbConnection connection, IMapper mapper, ILogger logger) : base(mapper, logger)
		{
			Connection = connection;
		}

		public List<SampleEntityBindingModel> List()
		{
			var collection = Connection.GetAll<SampleEntity>().ToList();
			return Mapper.Map<List<SampleEntity>, List<SampleEntityBindingModel>>(collection);
		}

		public SampleEntityBindingModel Read(int id)
		{
			var model = Connection.Get<SampleEntity>(id);
			return Mapper.Map<SampleEntity, SampleEntityBindingModel>(model);
		}

		public SampleEntityBindingModel Save(SampleEntityBindingModel bindingModel)
		{
			var model = Mapper.Map<SampleEntityBindingModel, SampleEntity>(bindingModel);
			if (model.Id == default(int))
			{
				var identity = (int)Connection.Insert(model);
				model.Id = identity;
			}
			else
			{
				Connection.Update(model);
			}
			return Mapper.Map<SampleEntity, SampleEntityBindingModel>(model);
		}

		public void Delete(int id)
		{
			var model = Connection.Get<SampleEntity>(id);
			if (model != null)
			{
				Connection.Delete(model);
			}
		}

		public void Delete(SampleEntityBindingModel bindingModel)
		{
			var model = Mapper.Map<SampleEntityBindingModel, SampleEntity>(bindingModel);
			Connection.Delete(model);
		}

		public object SampleError()
		{
			throw new HandledException(ExceptionType.General, "This is a sample error.", System.Net.HttpStatusCode.NotAcceptable);
		}
	}
}