using System.Collections.Generic;
using Glimpse.Core2;
using Glimpse.Core2.Extensibility;
using Glimpse.Core2.Framework;
using Glimpse.Test.Core2.TestDoubles;
using Moq;

namespace Glimpse.Test.Core2.Extensions
{
    public static class TestingExtensions
    {
        public static Mock<IFrameworkProvider> Setup(this Mock<IFrameworkProvider> frameworkProvider)
        {
            frameworkProvider.Setup(fp => fp.RuntimeContext).Returns(new DummyObjectContext());
            frameworkProvider.Setup(fp => fp.HttpRequestStore).Returns(
                new DictionaryDataStoreAdapter(new Dictionary<string, object>()));
            frameworkProvider.Setup(fp => fp.HttpServerStore).Returns(new DictionaryDataStoreAdapter(new Dictionary<string, object>()));

            return frameworkProvider;
        }

        public static Mock<ITabMetadata> Setup(this Mock<ITabMetadata> metadata)
        {
            metadata.Setup(m => m.RequestContextType).Returns(typeof(DummyObjectContext));
            metadata.Setup(m => m.LifeCycleSupport).Returns(LifeCycleSupport.EndRequest);

            return metadata;
        }

        public static Mock<ITab> Setup(this Mock<ITab> tab)
        {
            tab.Setup(p => p.GetData(It.IsAny<ITabContext>())).Returns("a result");

            return tab;
        }

        public static GlimpseConfiguration TurnOffAutoDiscover(this GlimpseConfiguration configuration)
        {
            configuration.Tabs.Discoverability.AutoDiscover = false;
            configuration.RuntimePolicies.Discoverability.AutoDiscover = false;
            configuration.SerializationConverters.Discoverability.AutoDiscover = false;

            return configuration;
        }
    }
}
