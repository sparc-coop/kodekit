using Kodekit.Models.Elements;
using Newtonsoft.Json;

namespace Kodekit;

public class ElementRepository
{
    public KitRepository Kits { get; }
   
    public Task<Dictionary<string, string>> GetWeightsAsync()
    {
        return Task.FromResult(new Dictionary<string, string>(Font.ValidWeights));
    }
}