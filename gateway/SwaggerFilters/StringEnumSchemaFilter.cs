// custom created file. Used in Program.cs > builder.Services.AddSwaggerGen()
// used to force Swashbuckle to serialize enums as strings in swagger.json 

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace gatewayRoot.SwaggerFilters
{

    public class StringEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Type = "string";
                schema.Format = null;
                schema.Enum = Enum.GetNames(context.Type)
                    .Select(n => new OpenApiString(n) as IOpenApiAny)
                    .ToList();
            }
        }
    }
}