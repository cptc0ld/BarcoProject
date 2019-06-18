using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        URVEntities1 db = new URVEntities1();
        public ActionResult Index()
        {

            List<TB_Item> ItemList = db.TB_Item.Where(a => a.Type == "Product").ToList();
            ViewBag.ItemList = new SelectList(ItemList, "ItemID", "ItemName");
            
            return View();
        }
        public JsonResult GetArticleList(int itemid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query =
            from art_id in db.TB_ITrAR
            where art_id.ItemID == itemid
            from article in db.TB_ArticleNumber
            where art_id.Arti_ID == article.Arti_ID
            select new
            {
                Arti_ID = article.Arti_ID,
                Arti_No = article.Arti_No
            };
            List<TB_ArticleNumber> temp = new List<TB_ArticleNumber>();
            foreach (var temp_item in query)
            {
                temp.Add(new TB_ArticleNumber()
                {
                    Arti_ID = temp_item.Arti_ID,
                    Arti_No= temp_item.Arti_No
                });
            }

            return Json(temp, JsonRequestBehavior.AllowGet);                
        }

       public JsonResult GetTestStageCheckBox(int itemid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TB_ITrTS> TestStageNo = db.TB_ITrTS.Where(a => a.ItemID == itemid).ToList();
            //List<TB_TestStage> TestStageName = db.TB_TestStage.Where(a => a.TestID == Convert.ToDecimal(TestStageNo)).ToList();
            
            return Json(TestStageNo, JsonRequestBehavior.AllowGet);
        }
    }
}