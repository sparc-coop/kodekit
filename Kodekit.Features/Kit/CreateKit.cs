using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public record CreateKitRequest(int UserId, Guid KitId, string Name);
    public class CreateKit : Feature<CreateKitRequest, Kit>
    {
        readonly IRepository<Kit> _KitRepository;

        public string Name { get; set; }

        public CreateKit(IRepository<Kit> kitRepository)
        {
            _KitRepository = kitRepository;
        }

        public override async Task<Kit> ExecuteAsync(CreateKitRequest request)
        {
            Kit kit = new(request.UserId, request.KitId, request.Name);

            await _KitRepository.AddAsync(kit);

            return kit;
        }
    }


    //public class CreateKit
    //{
    //    public string Execute(Kit kit)
    //    {
    //        string cssString = "h1 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "h2 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "h3 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "h4 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "h5 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "h6 { \n" +
    //        "   font-family: Roboto Mono;\n" +
    //        "   font-size: 48px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n" +
    //        "button { \n" +
    //        "   color: #FFFFFF;\n" +
    //        "   background-color: "+ kit.PrimaryColor +";\n" +
    //        "   font-size: 16px;\n" +
    //        "   line-height: " + kit.HeaderLineHeight + ";\n" +
    //        "}\n";

    //        return cssString;
    //    }
    }
}
