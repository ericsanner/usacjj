using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{78CDBC95-289A-4894-AE8A-3129E1BEE363}", AutoMap = true)]
	public interface IHomePage
	{
		Guid ID { get; set; }
		string Title { get; set; }
		string Content { get; set; }
	}
}
