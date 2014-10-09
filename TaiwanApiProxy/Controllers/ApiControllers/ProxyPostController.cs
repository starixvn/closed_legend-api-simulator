namespace TaiwanApiProxy.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web;
    using System.Web.Http;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Services;
    using TaiwanApiProxy.Utilities;
    using TaiwanApiProxy.ViewModels;

    public class ProxyPostController : ApiController
    {
        private readonly IRuleService _ruleService;

        private readonly ILogService _logService; 

        public ProxyPostController()
        {
            _ruleService = new RuleService();
            _logService = new LogService();
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage CheckUserName([FromBody]CheckUsernameViewModel model)
        {
            return GetResult(GetRule<CheckUsernameViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage InsertNewMember([FromBody]InsertNewMemberViewModel model)
        {
            return GetResult(GetRule<InsertNewMemberViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            return GetResult(GetRule<ResetPasswordViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateMemberInfo([FromBody]UpdateMemberInfoViewModel model)
        {
            return GetResult(GetRule<UpdateMemberInfoViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateMemberStatus([FromBody]UpdateMemberStatusViewModel model)
        {
            return GetResult(GetRule<UpdateMemberStatusViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage GetMemberInfo([FromBody]MemberInfoViewModel model)
        {
            return GetResult(GetRule<MemberInfoViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        [QueryString(Help = 0)]
        public HttpResponseMessage GetMemberInfoList([FromBody]MemberInfoListViewModel model)
        {
            return GetResult(GetRule<MemberInfoListViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        [QueryString(Help = 1)]
        public HttpResponseMessage GetMemberInfoList([FromBody]MemberInfoListCustIdsViewModel model)
        {
            return GetResult(GetRule<MemberInfoListCustIdsViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateAgentId([FromBody]UpdateAgentIdViewModel model)
        {
            return GetResult(GetRule<UpdateAgentIdViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage GetDepositMemberInfoList([FromBody]DepositMemberInfoListViewModel model)
        {
            return GetResult(GetRule<DepositMemberInfoListViewModel>(model), model);
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage GetDepositMemberInfo([FromBody]DepositMemberInfoViewModel model)
        {
            return GetResult(GetRule<DepositMemberInfoViewModel>(model), model);
        }

        private T GetRule<T>(T model) where T : class
        {
            var rule = _ruleService.GetRule<T>(model);

            var log = new LogInformation
            {
                ActionName = GetActionName<T>(model),
                ActionModel = model.GetType().Name,
                Input = GetInput<T>(model),
                Ip = Request.GetClientIpAddress(),
                LogTime = DateTime.Now
            };

            _logService.Insert(log);

            return rule;
        }

        private string GetActionName<T>(T model)
        {
            return Convert.ToString(model.GetType().GetProperty("ActionName").GetValue(model, null));
        }

        private string GetInput<T>(T model)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            var memoryStream = new MemoryStream();

            jsonSerializer.WriteObject(memoryStream, model);

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        private HttpResponseMessage GetResult(ApiViewModel rule, ApiViewModel model)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            if (rule == null || !rule.RuleStatus)
            {
                response.Content = new StringContent(ServiceWrapper.GetResponse(model));
            }
            else
            {
                response.Content = new StringContent(rule.Response);
            }

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}
