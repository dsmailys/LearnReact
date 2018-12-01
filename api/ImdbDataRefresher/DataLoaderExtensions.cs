using ImdbDataRefresher;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataLoaderExtensions
    {
        public static IServiceCollection AddImdbDataLoader (this IServiceCollection serviceCollection) {
            return serviceCollection.AddSingleton <IDataRefresher, DataLoader> ();
        }
    }
}