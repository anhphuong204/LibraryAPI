﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Models
{
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.ModelState.IsValid == false)
			{
				context.Result = new BadRequestResult();
			}
		}
	}
}
