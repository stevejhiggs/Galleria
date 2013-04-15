using System;
using System.Collections.Generic;

namespace Galleria.ViewModels
{
    public class EditImageDetailsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string HiddenTags { get; set; }
        public string ExistingTagsJson { get; set; }
    }
}