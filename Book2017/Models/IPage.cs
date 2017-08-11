using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{3ED28718-A2C6-41E2-9ADA-999074CA637A}", AutoMap = true)]
	public interface IPage
	{
		Guid ID { get; set; }
		string Title { get; set; }
		string Content { get; set; }
        [SitecoreField("Show Version Info")]
        bool ShowVersionInfo { get; set; }
        [SitecoreField("Show Parent Link")]
        bool ShowParentLink { get; set; }
	    int Version { get; set; }
	    DateTime Updated { get; set; }
	    Item Parent { get; set; }
    }
}
