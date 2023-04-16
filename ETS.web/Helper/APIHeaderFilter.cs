using ETS.web.Helper.Attributes;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ETS.web.Helper
{
    public class APIHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is JWTTokenAttribute);
            var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            var authpath = context.ApiDescription.RelativePath.Equals("api/Authenticator/GetToken");
            if (!authpath)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Token",
                    In = ParameterLocation.Header,
                    Description = "access token",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
            }
        }
    }
}
