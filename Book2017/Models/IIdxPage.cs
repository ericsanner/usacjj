using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace Book2017.Models
{
    [SitecoreType(TemplateId = "{6DDC39BC-F35A-4BC7-B371-AFC8A6AB5B14}", AutoMap = true)]
    public interface IIdxPage
    {
        Guid ID { get; set; }
        string Title { get; set; }
        string Content { get; set; }
	}
}
