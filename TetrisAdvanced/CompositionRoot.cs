﻿using Microsoft.Extensions.DependencyInjection;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Interfaces.Helpers;
using TetrisAdvanced.Services;
using TetrisAdvanced.Services.Helpers;

namespace TetrisAdvanced
{
    public static class CompositionRoot
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMathHelperService, MathHelperService>();
            services.AddTransient<IShapeService, ShapeService>();
            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<IEngineService, EngineService>();
        }
    }
}
