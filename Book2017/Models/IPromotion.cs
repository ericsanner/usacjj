using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Book2017.Models
{
	[SitecoreType(TemplateId = "{D2311B26-B72B-4900-8A71-0553979D09FE}", AutoMap = true)]
	public interface IPromotion
	{
		ID ID { get; set; }

		[SitecoreInfo(SitecoreInfoType.Path)]
		string Path { get; set; }

		[SitecoreField("{4E880EBC-D195-4A9A-A4D5-8BEA55785EF9}")]
		string Title { get; set; }

		[SitecoreField("{494F6198-03C5-4AFF-A1C2-3B9B86BF97B0}")]
		string Content { get; set; }

		[SitecoreField("{599C7E0D-43D3-40BE-AF8D-85281B621749}")]
		string Notes { get; set; }

		[SitecoreField("{EC2CEC51-44EA-4B3D-9B10-13FB9907F361}")]
		string InstructorNotes { get; set; }

		[SitecoreField("{4A621B36-E1B6-42EE-B4F1-9D321B850608}")]
        IEnumerable<ITag> IncludeTags { get; set; }
				
		Dictionary<string, List<Item>> IncludedItems { get; set; }
		
		int Version { get; set; }

	    DateTime Updated { get; set; }

        Item Parent { get; set; }
	}
}
