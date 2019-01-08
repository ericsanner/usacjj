using Book2017.Models;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSupergoo.ABCpdf10;

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

		public ActionResult Promotion()
		{
			Item current = SitecoreContext.GetCurrentItem<Item>();
			IPromotion model = SitecoreContext.GetCurrentItem<IPromotion>();

			model.Version = current.Version.Number;
			model.Updated = current.Statistics.Updated;
			model.Parent = current.Parent;

			//Make sure there is work to do
			if (model.IncludeTags != null)
			{
				//create dictionary
				model.IncludedItems = new Dictionary<string, List<Item>>();

				Item home = SitecoreContext.GetHomeItem<Item>();
				ChildList sections = home.GetChildren();

				foreach (Item section in sections)
				{
					model.IncludedItems.Add(section.DisplayName, new List<Item>());
				}

				//get database
				Database database = Sitecore.Configuration.Factory.GetDatabase("web");

				//get items of type content and sort by display name
				List<Item> allItems = database.SelectItems("/sitecore/Content/Home/*/*").Where(x => x.TemplateID == Constants.Templates.Content).OrderBy(x => x.DisplayName).ToList();				

				//find items that match include tags for the current item
				foreach (Item itm in allItems)
				{
					IContent tmpItm = SitecoreContext.Cast<IContent>(itm);
					IEnumerable<Guid> tmpTags = tmpItm.Tags?.Select(x => x.ID);

					if (tmpTags != null && tmpTags.Any())
					{
						foreach (ITag tag in model.IncludeTags)
						{
							if (tmpTags.Contains(tag.ID))
							{
								model.IncludedItems[tmpItm.Parent.DisplayName.ToString()].Add(itm);
							}
						}
					}
				}
			}

			return View("~/Views/UsaCjj/Promotion.cshtml", model);
		}

		public ActionResult IndexAlphabetical()
		{
            //get items
		    Database database = Sitecore.Configuration.Factory.GetDatabase("web");
		    List<Item> allItems = database.SelectItems("/sitecore/Content/Home/*/*").Where(x => x.TemplateID != Constants.Templates.Page).OrderBy(x => x.DisplayName).ToList();
		   
            //create dictionary
            Dictionary<string, List<Item>> dict = new Dictionary<string, List<Item>>();

		    for (int i = 65; i < 91; i++)
		    {
		        dict.Add(((char)i).ToString(), new List<Item>());
            }
			
            //put items into correct location in dictionary
		    foreach (Item itm in allItems)
		    {
				dict[itm.DisplayName[0].ToString()].Add(itm);
		    }
            
			return View("~/Views/UsaCjj/IndexContent.cshtml", dict);
		}

	    public ActionResult IndexSection()
	    {
	        //create dictionary
	        Dictionary<string, List<Item>> dict = new Dictionary<string, List<Item>>();
            
            //get sections
	        Item home = SitecoreContext.GetHomeItem<Item>();
	        ChildList sections = home.GetChildren();
            
	        foreach (Item section in sections)
	        {
	            dict.Add(section.DisplayName, new List<Item>());

	            ChildList items = section.GetChildren();

	            foreach (Item itm in items)
	            {
	                if (itm.TemplateID != Constants.Templates.Page)
	                {
	                    dict[section.DisplayName].Add(itm);
	                }
	            }
	        }
            
	        return View("~/Views/UsaCjj/IndexContent.cshtml", dict);
	    }

	    public ActionResult IndexTag()
	    {
	        //get database
	        Database database = Sitecore.Configuration.Factory.GetDatabase("web");

	        //get tags
            List<Item> tags = database.SelectItems("/sitecore/Content/Tags/*").OrderBy(x => x.DisplayName).ToList();

            //get items
	        List<Item> allItems = database.SelectItems("/sitecore/Content/Home/*/*").Where(x => x.TemplateID == Constants.Templates.Content).OrderBy(x => x.DisplayName).ToList();

	        //create dictionary
	        Dictionary<string, List<Item>> dict = new Dictionary<string, List<Item>>();

	        foreach(Item tag in tags)
	        {
	            dict.Add(tag.DisplayName, new List<Item>());
	        }

	        //put items into correct location in dictionary
	        foreach (Item itm in allItems)
	        {   
	            IContent contentItem = SitecoreContext.GetItem<IContent>(itm.ID.Guid);
	            foreach (ITag t in contentItem.Tags)
	            {
	                dict[t.Name].Add(itm);
                }
	        }

            return View("~/Views/UsaCjj/IndexContent.cshtml", dict);
	    }

	    public ActionResult Print()
	    {
	        const int PAGE_WIDTH = 612;
	        const int PAGE_HEIGHT = 792;
	        const int INCH = 72;
	        const int MARGIN = INCH / 2;
	        const int FOOTER_HEIGHT = 30;
	        const int COL_WIDTH = 100;
	        
	        const int TITLE_FONT_SIZE = 24;
	        const int SUBTITLE_FONT_SIZE = 18;
            const int CONTENT_FONT_SIZE = 14;
	        const int FOOTER_FONT_SIZE = 10;
	        const int LINE_BREAK_FONT_SIZE = 12;

            const string LINE_BREAK = "<br />";

	        Doc theDoc = new Doc();
	        int theID, contentStartPage, contentEndPage;
	        string content;

            //get sections
            Item home = SitecoreContext.GetHomeItem<Item>();
	        ChildList sections = home.GetChildren();

	        foreach (Item section in sections)
	        {
                ChildList items = section.GetChildren();

	            //create a new page and set our page pointer to the new page
	            theDoc.Page = theDoc.AddPage();

	            //get item as IContent
	            ISection sectionItem = SitecoreContext.GetItem<ISection>(section.ID.Guid);

                //reset text position
                theDoc.TextStyle.HPos = 0;
	            theDoc.TextStyle.VPos = 0;

	            //set container to full page (no footer on section page)
	            theDoc.Rect.Position(MARGIN, MARGIN);
	            theDoc.Rect.Width = PAGE_WIDTH - MARGIN - MARGIN;
	            theDoc.Rect.Height = PAGE_HEIGHT - MARGIN - MARGIN;

	            //title
	            theDoc.FontSize = TITLE_FONT_SIZE;
	            theDoc.AddHtml("<h1>" + sectionItem.Title + "</h1>");

	            theDoc.FontSize = LINE_BREAK_FONT_SIZE;
	            theDoc.AddHtml(LINE_BREAK);

                //intro
                if (!sectionItem.Intro.IsEmptyOrNull())
	            {
	                theDoc.FontSize = CONTENT_FONT_SIZE;
	                theDoc.AddHtml("<p>" + sectionItem.Intro + "</p>");

	                theDoc.FontSize = LINE_BREAK_FONT_SIZE;
	                theDoc.AddHtml(LINE_BREAK);
                }
                
                //subtitle
                theDoc.FontSize = SUBTITLE_FONT_SIZE;
	            theDoc.AddHtml("<p>In this section:</p>");

	            theDoc.FontSize = LINE_BREAK_FONT_SIZE;
	            theDoc.AddHtml(LINE_BREAK);

                //children
				//TODO: Print ul in 3 or 4 columns
                content = "<ul>";
                foreach (Item itm in items)
                {
                    content += "<li>" + itm.DisplayName + "</li>";
                }
	            content += "</ul>";

	            theDoc.FontSize = CONTENT_FONT_SIZE;
	            theID = theDoc.AddHtml(content);

                if (theDoc.Chainable(theID))
	            {
	                while (theDoc.Chainable(theID))
	                {
	                    theDoc.Page = theDoc.AddPage();
	                    theID = theDoc.AddHtml("", theID);
	                }
	            }

                //section pages
                foreach (Item itm in items)
	            {
	                if (itm.TemplateID != Constants.Templates.Page)
	                {
	                    //create a new page and set our page pointer to the new page
	                    theDoc.Page = theDoc.AddPage();

                        //track which page we are starting on
	                    contentStartPage = theDoc.PageNumber;

                        //get item as IContent
                        IContent contentItem = SitecoreContext.GetItem<IContent>(itm.ID.Guid);

	                    contentItem.Version = itm.Version.Number;
	                    contentItem.Updated = itm.Statistics.Updated;
	                    contentItem.Parent = itm.Parent;
                        
	                    //reset text position
	                    theDoc.TextStyle.HPos = 0;
	                    theDoc.TextStyle.VPos = 0;

	                    //set container
	                    theDoc.Rect.Position(MARGIN, MARGIN + FOOTER_HEIGHT);
	                    theDoc.Rect.Width = PAGE_WIDTH - MARGIN - MARGIN;
	                    theDoc.Rect.Height = PAGE_HEIGHT - MARGIN - MARGIN - FOOTER_HEIGHT;

                        //title
	                    theDoc.FontSize = TITLE_FONT_SIZE;
	                    theDoc.AddHtml("<h1>" + contentItem.Title + "</h1>");

	                    theDoc.FontSize = LINE_BREAK_FONT_SIZE;
	                    theDoc.AddHtml(LINE_BREAK);

                        //content
                        content = "<p>" + contentItem.Content + "</p>";
                        if (!contentItem.Notes.IsEmptyOrNull())
                        {
                            content += "<p>Notes:<br />" + contentItem.Notes + "</p>";
                        }

                        if (!contentItem.InstructorNotes.IsEmptyOrNull())
                        {
                            content += "<p>Instructor Notes:<br />" + contentItem.Notes + "</p>";
                        }

                        theDoc.FontSize = CONTENT_FONT_SIZE;
                        theID = theDoc.AddHtml(content);
                        
                        if (theDoc.Chainable(theID))
                        {
                            while (theDoc.Chainable(theID))
                            {
                                theDoc.Page = theDoc.AddPage();
                                theID = theDoc.AddHtml("", theID);
                            }
                        }

                        //track which page we are ending on
                        contentEndPage = theDoc.PageCount;

	                    //add tags to last page
                        if (contentItem.Tags.Any())
	                    {
	                        List<string> tags = new List<string>();

	                        foreach (ITag tag in contentItem.Tags)
	                        {
	                            tags.Add(tag.Name);
	                        }

	                        theDoc.Rect.Position(MARGIN, MARGIN + FOOTER_HEIGHT);
	                        theDoc.Rect.Width = PAGE_WIDTH - MARGIN - MARGIN;
	                        theDoc.Rect.Height = FOOTER_HEIGHT;
	                        theDoc.TextStyle.HPos = 0;
	                        theDoc.TextStyle.VPos = 0;
	                        theDoc.FontSize = FOOTER_FONT_SIZE;
	                        theDoc.PageNumber = contentEndPage;
	                        theDoc.AddHtml("<p>Tags: " + string.Join(", ", tags.ToArray()) + "</p>");
                        }
                        
                        //add footer to all pages for this item
                        theDoc.Rect.Position(MARGIN, MARGIN);
	                    theDoc.Rect.Width = PAGE_WIDTH - MARGIN - MARGIN;
	                    theDoc.Rect.Height = FOOTER_HEIGHT;
	                    theDoc.TextStyle.HPos = 0.5;
	                    theDoc.TextStyle.VPos = 0.5;
	                    theDoc.FontSize = FOOTER_FONT_SIZE;
	                    for (int i = contentStartPage; i <= contentEndPage; i++)
	                    {
	                        theDoc.PageNumber = i;
	                        theDoc.AddHtml(contentItem.Title + LINE_BREAK + "Section: " +
	                                       contentItem.Parent.DisplayName + " - Version " + contentItem.Version +
	                                       " - Updated " + contentItem.Updated.ToString("yyyy-MM-dd"));
	                    }
	                }
	            }
	        }

			//TODO: Print Index Pages

            //create save path
	        string tempPathRel = Sitecore.Configuration.Settings.TempFolderPath;
	        string tempPathAbs = Sitecore.IO.FileUtil.MapPath(tempPathRel);
	        string fileName = "usacjj." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
	        ViewBag.Path = tempPathRel + "/" + fileName;

            theDoc.Save(tempPathAbs + "\\" + fileName);
	        theDoc.Clear();

	        return View("~/Views/UsaCjj/Print.cshtml");
        }
    }
}