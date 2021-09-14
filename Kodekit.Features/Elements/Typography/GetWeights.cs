﻿using Sparc.Features;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record GetWeightsModel(Dictionary<string, string> Weights);
    public class GetWeights : PublicFeature<GetWeightsModel>
    {
        public override Task<GetWeightsModel> ExecuteAsync()
        {
            return Task.FromResult(new GetWeightsModel(Font.ValidWeights));
        }
    }
}