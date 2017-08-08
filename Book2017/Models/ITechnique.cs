using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{862B2507-2311-462F-A432-A6759F6B0F7B}", AutoMap = true)]
	public interface ITechnique
	{
		Guid ID { get; set; }
		string Title { get; set; }
		string Description { get; set; }
		string Notes { get; set; }
        IEnumerable<ITag> Tags { get; set; }
        Item Parent { get; set; }
	}
}
