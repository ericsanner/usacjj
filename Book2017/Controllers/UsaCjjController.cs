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
			Item itm = SitecoreContext.GetHomeItem<Item>();
			Navigation model = new Navigation();

			model.Pages = itm.GetChildren();

			return View("~/Views/UsaCjj/Navigation.cshtml", model);
		}

		public ActionResult HomePage()
		{
			IHomePage model = SitecoreContext.GetCurrentItem<IHomePage>();

			return View("~/Views/UsaCjj/HomePage.cshtml", model);
		}

		public ActionResult Section()
		{
			Item itm = SitecoreContext.GetCurrentItem<Item>();
			ISection model = SitecoreContext.GetCurrentItem<ISection>();

			model.Pages = itm.GetChildren();
			
			return View("~/Views/UsaCjj/Section.cshtml", model);
		}

		public ActionResult Technique()
		{
			Item current = SitecoreContext.GetCurrentItem<Item>();
			ITechnique model = SitecoreContext.GetCurrentItem<ITechnique>();

			model.Version = current.Version.Number;
			model.Updated = current.Statistics.Updated;
			model.Parent = current.Parent;

			return View("~/Views/UsaCjj/Technique.cshtml", model);
		}
	    public ActionResult IdxPage()
	    {
	        IIdxPage model = SitecoreContext.GetCurrentItem<IIdxPage>();
            
	        return View("~/Views/UsaCjj/IdxPage.cshtml", model);
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
		        if (itm.TemplateName != "IdxPage")
		        {
		            dict[itm.DisplayName[0].ToString()].Add(itm);
                }
		    }
            
			return View("~/Views/UsaCjj/IndexAlphabetical.cshtml", dict);
		}
	}
}