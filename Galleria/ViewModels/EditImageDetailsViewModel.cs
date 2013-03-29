using System;
using System.Collections.Generic;

namespace Galleria.ViewModels
{
    public class EditImageDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HiddenTags { get; set; }
        public string ExistingTagsJson { get; set; }
    }
}