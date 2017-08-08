using Book2017.Models;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;
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
        public ActionResult Section()
        {
            Item itm = SitecoreContext.GetCurrentItem<Item>();
            ISection model = SitecoreContext.GetCurrentItem<ISection>();

            model.Pages = itm.GetChildren();
            
            return View("~/Views/UsaCjj/Section.cshtml", model);
        }

        public ActionResult Content()
        {
            IContent model = SitecoreContext.GetCurrentItem<IContent>();
            model.Parent = SitecoreContext.GetCurrentItem<Item>().Parent;

            return View("~/Views/UsaCjj/Content.cshtml", model);
        }

        public ActionResult Technique()
        {
            ITechnique model = SitecoreContext.GetCurrentItem<ITechnique>();
            model.Parent = SitecoreContext.GetCurrentItem<Item>().Parent;

            return View("~/Views/UsaCjj/Technique.cshtml", model);
        }
    }
}