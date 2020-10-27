﻿using System;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public interface ISchemaFilter
    {
        void Apply(OpenApiSchema schema, SchemaFilterContext context);
    }

    public class SchemaFilterContext
    {
        public SchemaFilterContext(
            Type type,
            SchemaRepository schemaRepository,
            MemberInfo memberInfo = null,
            ParameterInfo parameterInfo = null)
        {
            Type = type;
            SchemaRepository = schemaRepository;
            MemberInfo = memberInfo;
            ParameterInfo = parameterInfo;
        }

        public Type Type { get; }

        public ISchemaGenerator SchemaGenerator { get; }

        public SchemaRepository SchemaRepository { get; }

        public MemberInfo MemberInfo { get; }

        public ParameterInfo ParameterInfo { get; }
    }
}