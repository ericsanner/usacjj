using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;

namespace Book2017.Models
{
	[SitecoreType(TemplateId = "{3ED28718-A2C6-41E2-9ADA-999074CA637A}", AutoMap = true)]
	public interface IPage
	{
		Guid ID { get; set; }

		[SitecoreField("{78865559-DFBC-4CE5-AD26-454DE23DFCDE}")]
		string Title { get; set; }

		[SitecoreField("{8A008441-D947-4019-A9C2-A8629EDD6D62}")]
		string Content { get; set; }

        [SitecoreField("{B0002217-AD14-40C7-9BD2-1AD63AC45581}")]
        bool ShowVersionInfo { get; set; }

        [SitecoreField("{62C88F06-ACC9-49F3-A5B5-02DC049A95E2}")]
        bool ShowParentLink { get; set; }

	    int Version { get; set; }

	    DateTime Updated { get; set; }

	    Item Parent { get; set; }
    }
}
