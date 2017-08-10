using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{54F5FBC8-E8E6-4F2F-B217-F6AE90343F94}", AutoMap = true)]
    public interface IContent
    {
        Guid ID { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        IEnumerable<ITag> Tags { get; set; }
        int Version { get; set; }
		DateTime Updated { get; set; }
		Item Parent { get; set; }
	}
}
