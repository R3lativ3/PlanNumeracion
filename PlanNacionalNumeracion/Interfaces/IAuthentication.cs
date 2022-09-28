using System;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Authentication;

namespace PlanNacionalNumeracion.Interfaces
{
	public interface IAuthentication
	{
        UserResponse Auth(AuthRequest authRequest);

		UserResponse Validate(string token);
	}
}

