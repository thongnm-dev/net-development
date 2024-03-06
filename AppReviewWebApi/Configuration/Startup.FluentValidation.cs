using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppReviewWebApi.Validators;
using Net.WebCore.Validator;
using Net.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReviewWebApi.Models.Request;

namespace AppReviewWebApi.Configuration
{
    public class FluentValidationStartup : IServiceStartup
    {
        public int Order => 150;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<LoginModel>, LoginValidator>();
        }
    }
}
