using JetBrains.Annotations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SwaggerTestV3
{
    public class CustomSchemaFilter : ISchemaFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomSchemaFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var requiredProperties = context.Type.GetProperties()
               .Where(x => x.IsDefined(typeof(NotNullAttribute)))
               .Select(t => char.ToLowerInvariant(t.Name[0]) + t.Name.Substring(1))
               .ToHashSet();

            if (schema.Required == null)
            {
                schema.Required = new HashSet<string>();
            }
            schema.Required = schema.Required.Union(requiredProperties).ToHashSet();
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class SwaggerSchemaFilterAttribute : Attribute
    {
        public SwaggerSchemaFilterAttribute(Type type)
        {
            Type = type;
            Arguments = new object[] { };
        }

        public Type Type { get; private set; }

        public object[] Arguments { get; set; }
    }
}
