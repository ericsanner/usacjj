using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Collections;
using System;

namespace Book2017.Models
{
	[SitecoreType(TemplateId = "{8C4B093F-4C46-4F85-B066-FEBE01254A1F}", AutoMap = true)]
	public interface ISection
	{
		Guid ID { get; set; }

		[SitecoreField("{22B97C4A-26F5-4825-B73F-A0B006BC8B95}")]
		string Title { get; set; }

		[SitecoreField("{91285486-78B1-4DBD-98EB-EE30B9B54EB8}")]
		string Intro { get; set; }

        ChildList Pages { get; set; }
	}
}
