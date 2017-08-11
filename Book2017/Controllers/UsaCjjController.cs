using Book2017.Models;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Book2017.Controllers
{
    public class UsaCjjController : GlassController
	{
		public ActionResult Navigation()
		{
			Item home = SitecoreContext.GetHomeItem<Item>();
			Navigation model = new Navigation();

			model.Pages = home.GetChildren();

			return View("~/Views/UsaCjj/Navigation.cshtml", model);
		}

        public ActionResult Page()
	    {
	        Item current = SitecoreContext.GetCurrentItem<Item>();
            IPage model = SitecoreContext.GetCurrentItem<IPage>();

	        model.Version = current.Version.Number;
	        model.Updated = current.Statistics.Updated;
	        model.Parent = current.Parent;

            return View("~/Views/UsaCjj/Page.cshtml", model);
	    }

        public ActionResult Section()
		{
			Item current = SitecoreContext.GetCurrentItem<Item>();
			ISection model = SitecoreContext.GetCurrentItem<ISection>();

			model.Pages = current.GetChildren();
			
			return View("~/Views/UsaCjj/Section.cshtml", model);
		}

		public ActionResult Content()
		{
			Item current = SitecoreContext.GetCurrentItem<Item>();
			IContent model = SitecoreContext.GetCurrentItem<IContent>();

			model.Version = current.Version.Number;
			model.Updated = current.Statistics.Updated;
			model.Parent = current.Parent;

			return View("~/Views/UsaCjj/Content.cshtml", model);
		}

        public ActionResult IndexAlphabetical()
		{
            //get items
		    Sitecore.Data.Database database = Sitecore.Configuration.Factory.GetDatabase("web");
		    List<Item> allItems = database.SelectItems("/sitecore/Content/Home/*/*").OrderBy(x => x.DisplayName).ToList();
		   
            //create dictionary
            Dictionary<string, List<Item>> dict = new Dictionary<string, List<Item>>();

		    for (int i = 65; i < 91; i++)
		    {
		        dict.Add(((char)i).ToString(), new List<Item>());
            }

            //put items into correct location in dictionary
		    foreach (Item itm in allItems)
		    {
		        if (itm.TemplateName != "Page")
		        {
		            dict[itm.DisplayName[0].ToString()].Add(itm);
                }
		    }
            
			return View("~/Views/UsaCjj/IndexAlphabetical.cshtml", dict);
		}
	}
}