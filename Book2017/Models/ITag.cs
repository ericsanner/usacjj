using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace Book2017.Models
{
	[SitecoreType(TemplateId = "{639D14E5-021B-4889-8BB6-E4AE85E43A20}", AutoMap = true)]
	public interface ITag
	{
		Guid ID { get; set; }

		[SitecoreField("{487B380B-D2C4-4152-9B0D-1CCF5C515650}")]
		string Name { get; set; }
	}
}
