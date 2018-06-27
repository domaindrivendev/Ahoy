﻿using System;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Basic.Swagger
{
    public class ExamplesSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var type = context.ModelMetadata.ModelType;
            schema.Example = GetExampleOrNullFor(type);
        }

        private object GetExampleOrNullFor(Type systemType)
        {
            switch (systemType.Name)
            {
                case "Product":
                    return new
                    {
                        Id = "123",
                        Description = "foobar"
                    };
                default:
                    return null;
            }
        }
    }
}
