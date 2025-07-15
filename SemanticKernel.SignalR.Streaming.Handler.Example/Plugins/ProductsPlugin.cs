using Microsoft.SemanticKernel;
using SemanticKernel.SignalR.Streaming.Handler.Example.ViewModels;
using System.ComponentModel;
using System.Text.Json;

namespace SemanticKernel.SignalR.Streaming.Handler.Example.Plugins
{
    public class ProductsPlugin(HttpClient httpClient)
    {
        [KernelFunction("bestSellingProducts")]
        [Description("En çok satış yapılan ürünleri getirir..")]
        [return: Description("En çok satış yapılan ürünleri json döndürür.")]
        public async Task<string> BestSellingProducts()
        {
            var response = await httpClient.GetAsync("https://localhost:7217/best-selling-products");
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
