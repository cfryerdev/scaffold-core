﻿using ScaffoldCore.Infrastructure.BaseModels;
using ScaffoldCore.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ScaffoldCore.Infrastructure.Interfaces
{
	public interface IValidateSaveRule<in TModel> where TModel : BaseEntityModel
	{
		List<HandledException> Validate(IDbConnection connection, TModel model, TModel oldModel);
	}
}
