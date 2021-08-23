using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features.Kits.Entities
{
    public class GlobalSettings
    {
        public bool? ResetEverything { get; set; }
        public bool? ResetHeadings { get; set; }
        public bool? ResetParagraphs { get; set; }
        public bool? ResetLists { get; set; }
        public bool? ResetCheckboxes { get; set; }
        public bool? ResetAnchors { get; set; }
        public bool? AllKodekitElements { get; set; }
        public bool? KodekitTypography { get; set; }
        public bool? KodekitColors { get; set; }
        public bool? KodekitIcons { get; set; }
        public bool? KodekitLayout { get; set; }
        public bool? KodekitButtons { get; set; }
        public bool? KodekitInputs { get; set; }
        public bool? KodekitCheckboxes { get; set; }
        public bool? KodekitDropdowns { get; set; }
        public bool? KodekitAnchors { get; set; }
        public bool? KodekitImages { get; set; }
    }
}
