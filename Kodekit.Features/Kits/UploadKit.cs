using Sparc.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features
{
    public class UploadKit : PublicFeature<string>
    {
        public override async Task<string> ExecuteAsync()
        {
            var newFile = Path.Combine(Environment.CurrentDirectory, "_Plugins\\test.css");
            FileStream stream = new FileStream(newFile, FileMode.Open);
            string fileUrl = await new UploadService().Upload(stream, "app.css");
            return fileUrl;
        }
    }

}
