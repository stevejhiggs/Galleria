using Galleria.Core.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galleria.Core.RavenDb.Indexes
{
    public class SearchIndex : AbstractIndexCreationTask<StoredImage, RavenDb.Indexes.SearchIndex.ReduceResult>
    {
        public class ReduceResult
        {
            public object[] SearchQuery { get; set; }
            public DateTime UploadDateTime { get; set; }
        }

        public SearchIndex()
        {
            Map = images => 
                from i in images
                select new
                {
                    SearchQuery = i.Tags.Concat(new []
                    {
                        i.Title
                    }),
                    UploadDateTime = i.UploadDateTime
                };

            Index(x => x.SearchQuery, FieldIndexing.Analyzed);
        }
    }
}
