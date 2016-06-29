using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;
using System.Web.Configuration;
using Senparc.Weixin.MP.CommonAPIs;

namespace WeiXin.Controllers
{
    public class TestController : Controller
    {
        private static string _appId = string.Empty;
        private static string _appSecret = string.Empty;
        private static string _token = string.Empty;
        static TestController()
        {
            _appId = WebConfigurationManager.AppSettings["WeixinAppId"];
            _appSecret = WebConfigurationManager.AppSettings["WeixinToken"];
            _token = WebConfigurationManager.AppSettings["WeixinAppSecret"];
        }


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAccessToken()
        {


            var result = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendMsgToWeChatUser()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var tpl = TemplateApi.GetPrivateTemplate(accessToken);
            var result = TemplateApi.SendTemplateMessage(accessToken, "lst0222", tpl.template_list.FirstOrDefault().template_id, "#173177", "url", new { NO = "您的电话卡号为123456789" });
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}