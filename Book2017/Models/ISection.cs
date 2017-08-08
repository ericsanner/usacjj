using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Collections;
using System;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{8C4B093F-4C46-4F85-B066-FEBE01254A1F}", AutoMap = true)]
	public interface ISection
	{
		Guid ID { get; set; }
		string Title { get; set; }
		string Intro { get; set; }
        ChildList Pages { get; set; }
	}
}
