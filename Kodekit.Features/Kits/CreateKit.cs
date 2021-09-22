﻿using System;
using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public record CreateKitResponse(string KitId);
    public class CreateKit : PublicFeature<string, CreateKitResponse>
    {
        public IRepository<Kit> Kit { get; }
        public CreateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<CreateKitResponse> ExecuteAsync(string userId)
        {
            var kit = new Kit
            {
                DateCreated = DateTime.Now,
                UserId = userId
            };

            await Kit.AddAsync(kit);
            
            return new(kit.Id);
        }
    }
}
