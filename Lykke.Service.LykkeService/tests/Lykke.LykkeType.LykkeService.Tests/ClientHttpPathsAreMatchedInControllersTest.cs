using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lykke.Service.LykkeService;
using Lykke.Service.LykkeService.Client;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Xunit;

namespace Lykke.LykkeType.LykkeService.Tests
{
    public class ClientHttpPathsAreMatchedInControllersTest
    {
        private readonly Type _routeAttrType = typeof(RouteAttribute);
        private readonly List<Type> _refitAttrs = new List<Type>
        {
            typeof(GetAttribute), typeof(PostAttribute), typeof(PutAttribute), typeof(DeleteAttribute)
        };
        private readonly List<Type> _httpAttrs = new List<Type>
        {
            typeof(HttpGetAttribute), typeof(HttpPostAttribute), typeof(HttpPutAttribute), typeof(HttpDeleteAttribute)
        };

        [Fact]
        public void CheckRoutesInControllersTest()
        {
            var clientInterface = typeof(ILykkeServiceClient);

            var apiInterfaces = clientInterface
                .GetProperties()
                .Where(p => p.CanRead && p.PropertyType.IsInterface)
                .Select(p => p.PropertyType)
                .ToList();
            var controllers = Assembly.GetAssembly(typeof(Startup))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Controller)))
                .ToList();

            var apiErrors = new List<string>();

            foreach (var apiInterface in apiInterfaces)
            {
                var implementingController = controllers.FirstOrDefault(c => c.GetInterfaces().Any(i => i == apiInterface));
                if (implementingController == null)
                    continue;

                foreach (var apiMethod in apiInterface.GetMethods())
                {
                    var refitAttr = apiMethod.CustomAttributes.First(a => _refitAttrs.Any(i => i == a.AttributeType));
                    var apiRoute = refitAttr.ConstructorArguments[0].Value.ToString().TrimStart('/');

                    var implMethod = implementingController.GetMethod(
                        apiMethod.Name,
                        apiMethod.GetParameters().Select(p => p.ParameterType).ToArray());

                    var routeAttr = implMethod.CustomAttributes.FirstOrDefault(a => a.AttributeType == _routeAttrType);
                    var httpAttr = implMethod.CustomAttributes.First(a => _httpAttrs.Any(i => i == a.AttributeType));
                    var implRoute = string.Empty;
                    if ((routeAttr ?? httpAttr).ConstructorArguments.Count > 0)
                        implRoute = (routeAttr ?? httpAttr).ConstructorArguments[0].Value.ToString();

                    var controllerRouteAttr = implementingController.CustomAttributes.FirstOrDefault(a => a.AttributeType == _routeAttrType);
                    if (controllerRouteAttr != null)
                    {
                        var controllerRoute = controllerRouteAttr.ConstructorArguments[0].Value.ToString();
                        implRoute = string.IsNullOrWhiteSpace(implRoute)
                            ? controllerRoute.Trim('/')
                            : $"{controllerRoute.Trim('/')}/{implRoute.TrimStart('/')}";
                    }

                    if (apiRoute != implRoute)
                        apiErrors.Add(
                            $"Route '{apiRoute}' on {apiInterface.Name}.{apiMethod.Name} is not matched in controller - '{implRoute}'");

                    if (_refitAttrs.IndexOf(refitAttr.AttributeType) != _httpAttrs.IndexOf(httpAttr.AttributeType))
                        apiErrors.Add(
                            $"Refit '{refitAttr.AttributeType.Name}' on {apiInterface.Name}.{apiMethod.Name} is not matched in controller - '{httpAttr.AttributeType.Name}'");
                }
            }

            if (apiInterfaces.Any())
                Assert.True(controllers.Any());
            Assert.True(apiErrors.Count == 0, string.Join(",\t", apiErrors));
        }
    }
}
