namespace Kodekit.Features.Components.Elements;

public record GetWeightsModel(Dictionary<string, string> Weights);
public class GetWeights : PublicFeature<GetWeightsModel>
{
    public override Task<GetWeightsModel> ExecuteAsync()
    {
        return Task.FromResult(new GetWeightsModel(Font.ValidWeights));
    }
}
