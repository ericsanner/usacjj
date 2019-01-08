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

		[SitecoreField("{3D9692BD-2127-432D-9FCC-3E7417CB06BE}")]
		string Title { get; set; }

		[SitecoreField("{2F311DEC-578C-444A-A4BD-BA05A74E9C2F}")]
		string Content { get; set; }

		[SitecoreField("{6D6E772B-9F30-4B90-ACDF-9C610B78D321}")]
		string Notes { get; set; }

		[SitecoreField("{A5EEB78A-5C8F-4575-8CBB-769642B19DD8}")]
		string InstructorNotes { get; set; }

		[SitecoreField("{9ED7933A-0033-41CB-91C0-FD2884445631}")]
        IEnumerable<ITag> Tags { get; set; }

        int Version { get; set; }

	    DateTime Updated { get; set; }

        Item Parent { get; set; }
	}
}
