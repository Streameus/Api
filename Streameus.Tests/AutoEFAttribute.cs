using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using AutoFixture.AutoEF;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Streameus.DataAbstractionLayer;

namespace Streameus.Tests
{
    /// <summary>
    /// This Attribute automatically mocks classes and entities
    /// </summary>
    public class AutoEFAttribute : AutoDataAttribute
    {
        public AutoEFAttribute()
            : base(new Fixture()
                .Customize(new HttpRequestMessageCustomization())
                .Customize(new ApiControllerCustomization())
                .Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof (StreameusContext))))
                .Customize(new AutoMoqCustomization())
                )
        {
            var throwing = this.Fixture.Behaviors.FirstOrDefault(b => b.GetType() == typeof (ThrowingRecursionBehavior));
            this.Fixture.Behaviors.Remove(throwing);
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }


    internal class ApiControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new Postprocessor(
                        new MethodInvoker(
                            new ModestConstructorQuery()),
                        new ApiControllerFiller()),
                    new ApiControllerSpecification()));
        }

        private class ApiControllerFiller : ISpecimenCommand
        {
            public void Execute(object specimen, ISpecimenContext context)
            {
                if (specimen == null)
                    throw new ArgumentNullException("specimen");
                if (context == null)
                    throw new ArgumentNullException("context");

                var target = specimen as ApiController;
                if (target == null)
                    throw new ArgumentException(
                        "The specimen must be an instance of ApiController.",
                        "specimen");

                target.Request =
                    (HttpRequestMessage) context.Resolve(
                        typeof (HttpRequestMessage));
            }
        }

        private class ApiControllerSpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                var requestType = request as Type;
                if (requestType == null)
                    return false;
                return typeof (ApiController).IsAssignableFrom(requestType);
            }
        }
    }

    internal class HttpRequestMessageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpRequestMessage>(c => c
                .Without(x => x.Content)
                .Do(x => x.Properties[HttpPropertyKeys.HttpConfigurationKey] =
                    new HttpConfiguration()));
        }
    }
}