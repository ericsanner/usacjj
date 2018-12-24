using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Book2017.Models
{
	[SitecoreType(TemplateId = "{862B2507-2311-462F-A432-A6759F6B0F7B}", AutoMap = true)]
	public interface IContent
	{
		ID ID { get; set; }
		[SitecoreInfo(SitecoreInfoType.Path)]
		string Path { get; set; }
		string Title { get; set; }
		string Content { get; set; }
		string Notes { get; set; }
		[SitecoreField("{A5EEB78A-5C8F-4575-8CBB-769642B19DD8}")]
		string InstructorNotes { get; set; }
        IEnumerable<ITag> Tags { get; set; }
        int Version { get; set; }
	    DateTime Updated { get; set; }
        Item Parent { get; set; }
	}
}
