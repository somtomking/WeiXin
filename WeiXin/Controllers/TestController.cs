using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;
using System.Web.Configuration;
using Senparc.Weixin.MP.CommonAPIs;
using NLog;
namespace WeiXin.Controllers
{
    public class TestController : Controller
    {
        private static string _appId = string.Empty;
        private static string _appSecret = string.Empty;
        private static string _token = string.Empty;

        private Logger _log = LogManager.GetCurrentClassLogger();
        static TestController()
        {
            _appId = WebConfigurationManager.AppSettings["WeixinAppId"];
            _appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"]; 
             _token = WebConfigurationManager.AppSettings["WeixinToken"];
        }


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAccessToken()
        {

            try
            {
                var result = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                _log.Error( ex, "获取AccessToken发生错误！");

                return Json(new {error=ex.Message },JsonRequestBehavior.AllowGet);
            }

           

           
        }
        public ActionResult SendMsgToWeChatUser(string openId)
        {
            try
            {
                var accessToken = AccessTokenContainer.GetAccessToken(_appId);
                var tpl = TemplateApi.GetPrivateTemplate(accessToken);
                var result = TemplateApi.SendTemplateMessage(accessToken, openId, tpl.template_list.FirstOrDefault().template_id, "#173177", "url", new { NO = "你的卡密为58199887" });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "SendMsgToWeChatUser发生错误！");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }
    }
}