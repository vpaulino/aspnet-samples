using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

#if NET46

using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;
using Components.Web.Http.Tests.Models;
using Components.Web.Http.Validation;
using Moq;
using Xunit;
using System.Linq;

namespace Components.Web.Http.Tests
{
    public class PolicyModelValidationTests
    {

        [Fact]
        public void ReplaceModelValidator()
        {
             
            HttpConfiguration configuration = new HttpConfiguration();
  
            configuration.Services.Replace(typeof(IBodyModelValidator), new PolicyBasedObjectModelValidator(new InheritanceModelValidationPolicy(typeof(Entity), false)));

            PolicyBasedObjectModelValidator validator = configuration.Services.GetBodyModelValidator() as PolicyBasedObjectModelValidator;

            Assert.NotNull(validator);

        }

        [Fact]
        public async Task ModelIncludeFromPolicy_ShouldValidateFalse_ResultsAlwaysTrue()
        {
            var product = new Product();
            // Arrange
            var api = new ProductController();
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Content = new ObjectContent(typeof(Product), product, new JsonMediaTypeFormatter());

        
            configuration.Services.Replace(typeof(IBodyModelValidator), new PolicyBasedObjectModelValidator(new InheritanceModelValidationPolicy(typeof(Entity), false)));

   
            HttpControllerContext controllerContext = ContextUtil.CreateControllerContext(configuration, instance: api, routeData: null, request: request);

            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "test", typeof(ProductController));
            controllerContext.ControllerDescriptor = controllerDescriptor;
             
            
            Mock<IHttpActionSelector> mockSelector = new Mock<IHttpActionSelector>();
            mockSelector
                .Setup(invoker => invoker.SelectAction(It.IsAny<HttpControllerContext>()))
                .Returns(() =>
                {
                    Func<Product, HttpResponseMessage> testDelegate = api.CreateProduct;
                    return new ReflectedHttpActionDescriptor
                    {
                        Configuration = controllerContext.Configuration,
                        ControllerDescriptor = controllerDescriptor,
                        MethodInfo = testDelegate.Method
                    };
                });


            controllerDescriptor.Configuration.Services.Replace(typeof(IHttpActionSelector), mockSelector.Object);

            // Act
            HttpResponseMessage message = await api.ExecuteAsync(
                controllerContext,
                CancellationToken.None);

            // Assert
            Assert.Equal("true", (await message.Content.ReadAsStringAsync()).ToLower());
        }

        [Fact]
        public async Task ModelExcludeFromPolicy_ShouldValidateTrue_ResultsTrue()
        {
            var person = new Person();
            // Arrange
            var api = new ProductController();
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Content = new ObjectContent(typeof(Person), person, new JsonMediaTypeFormatter());


            configuration.Services.Replace(typeof(IBodyModelValidator), new PolicyBasedObjectModelValidator(new InheritanceModelValidationPolicy(typeof(Entity))));


            HttpControllerContext controllerContext = ContextUtil.CreateControllerContext(configuration, instance: api, routeData: null, request: request);

            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "test", typeof(ProductController));
            controllerContext.ControllerDescriptor = controllerDescriptor;


            Mock<IHttpActionSelector> mockSelector = new Mock<IHttpActionSelector>();
            mockSelector
                .Setup(invoker => invoker.SelectAction(It.IsAny<HttpControllerContext>()))
                .Returns(() =>
                {
                    Func<Person, HttpResponseMessage> testDelegate = api.AddClient;
                    return new ReflectedHttpActionDescriptor
                    {
                        Configuration = controllerContext.Configuration,
                        ControllerDescriptor = controllerDescriptor,
                        MethodInfo = testDelegate.Method
                    };
                });


            controllerDescriptor.Configuration.Services.Replace(typeof(IHttpActionSelector), mockSelector.Object);

            // Act
            HttpResponseMessage message = await api.ExecuteAsync(
                controllerContext,
                CancellationToken.None);

            // Assert
            Assert.Equal("true", (await message.Content.ReadAsStringAsync()).ToLower());
        }

        [Fact]
        public async Task InvalidModelIncludeFromPolicy_ShouldValidateTrue_ResultsFalse()
        {
            var product = new Product();
            // Arrange
            var api = new ProductController();
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Content = new ObjectContent(typeof(Product), product, new JsonMediaTypeFormatter());
            
            configuration.Services.Replace(typeof(IBodyModelValidator), new PolicyBasedObjectModelValidator(new InheritanceModelValidationPolicy(typeof(Entity))));
            
            HttpControllerContext controllerContext = ContextUtil.CreateControllerContext(configuration, instance: api, routeData: null, request: request);

            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "test", typeof(ProductController));
            controllerContext.ControllerDescriptor = controllerDescriptor;
            
            Mock<IHttpActionSelector> mockSelector = new Mock<IHttpActionSelector>();
            mockSelector
                .Setup(invoker => invoker.SelectAction(It.IsAny<HttpControllerContext>()))
                .Returns(() =>
                {
                    Func<Product, HttpResponseMessage> testDelegate = api.CreateProduct;
                    return new ReflectedHttpActionDescriptor
                    {
                        Configuration = controllerContext.Configuration,
                        ControllerDescriptor = controllerDescriptor,
                        MethodInfo = testDelegate.Method
                    };
                });


            controllerDescriptor.Configuration.Services.Replace(typeof(IHttpActionSelector), mockSelector.Object);

            // Act
            HttpResponseMessage message = await api.ExecuteAsync(
                controllerContext,
                CancellationToken.None);

            // Assert
            Assert.Equal("false", (await message.Content.ReadAsStringAsync()).ToLower());
        }


    }
}

#endif