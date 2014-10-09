namespace TaiwanApiProxy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Services;
    using TaiwanApiProxy.Utilities;
    using TaiwanApiProxy.ViewModels;

    public class RuleGetController : ApiController
    {
        private readonly IRuleService _ruleService;

        private readonly RuleExtension _ruleExtension;

        public RuleGetController()
        {
            _ruleService = new RuleService();
            _ruleExtension = new RuleExtension();
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage CheckUserName([FromUri]CheckUsernameViewModel model)
        {
            return GetResponse<CheckUsernameViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage InsertNewMember([FromUri]InsertNewMemberViewModel model)
        {
            return GetResponse<InsertNewMemberViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage ResetPassword([FromUri]ResetPasswordViewModel model)
        {
            return GetResponse<ResetPasswordViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage UpdateMemberInfo([FromUri]UpdateMemberInfoViewModel model)
        {
            return GetResponse<UpdateMemberInfoViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage UpdateMemberStatus([FromUri]UpdateMemberStatusViewModel model)
        {
            return GetResponse<UpdateMemberStatusViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetMemberInfo([FromUri]MemberInfoViewModel model)
        {
            return GetResponse<MemberInfoViewModel>(model);
        }

        [AcceptVerbs("GET")]
        [QueryString(Help = 0)]
        public HttpResponseMessage GetMemberInfoList([FromUri]MemberInfoListViewModel model)
        {
            return GetResponse<MemberInfoListViewModel>(model);
        }

        [AcceptVerbs("GET")]
        [QueryString(Help = 1)]
        public HttpResponseMessage GetMemberInfoList([FromUri]MemberInfoListCustIdsViewModel model)
        {
            return GetResponse<MemberInfoListCustIdsViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage UpdateAgentId([FromUri]UpdateAgentIdViewModel model)
        {
            return GetResponse<UpdateAgentIdViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetDepositMemberInfoList([FromUri]DepositMemberInfoListViewModel model)
        {
            return GetResponse<DepositMemberInfoListViewModel>(model);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetDepositMemberInfo([FromUri]DepositMemberInfoViewModel model)
        {
            return GetResponse<DepositMemberInfoViewModel>(model);
        }

        private HttpResponseMessage GetResponse<T>(T model) where T : class
        {
            var method = GetMethod();
            HttpResponseMessage httpResponse; 

            switch (method)
            {
                case Method.Add:
                    httpResponse = GetAddResponse<T>(model);
                    break;
                case Method.Edit:
                    httpResponse = GetEditResponse(model);
                    break;
                default:
                    httpResponse = GetRuleListResponse(model);
                    break;
            }

            return httpResponse;
        }

        private Method GetMethod()
        {
            var method = HttpContext.Current.Request.QueryString["method"];

            return string.IsNullOrEmpty(method) ? Method.None : (Method)Enum.Parse(typeof(Method), method);
        }

        private HttpResponseMessage GetAddResponse<T>(T model)
        {
            return ViewParser.Get(
                "Rule",
                "Add",
                new HelpActionViewModel
                {
                    Help = Convert.ToInt32(HttpContext.Current.Request.QueryString["help"]),
                    Action = _ruleExtension.GetActionName<T>(model),
                    Parameters = _ruleExtension.GetParametersValue<T>(model).ToList(),
                    Note = _ruleExtension.GetNote<T>(model)
                },
                new RuleController());
        }

        private HttpResponseMessage GetEditResponse<T>(T model) where T : class
        {
            var ruleId = _ruleExtension.GetRuleId<T>(model);
            var rule = _ruleService.GetRule<T>(ruleId);

            return ViewParser.Get(
                "Rule",
                "Edit",
                new RuleViewModel
                {
                    RuleId = ruleId,
                    Action = _ruleExtension.GetActionName<T>(rule),
                    Note = _ruleExtension.GetNote<T>(rule),
                    Help = Convert.ToInt32(HttpContext.Current.Request.QueryString["help"]),
                    Parameters = _ruleExtension.GetParametersValue<T>(rule).ToList()
                },
            new RuleController());
        }

        private HttpResponseMessage GetRuleListResponse<T>(T model)
        {
            return ViewParser.Get(
                "Rule",
                "Index",
                new RuleListViewModel
                {
                    Action = _ruleExtension.GetActionName<T>(model),
                    Header = GetHeader(model),
                    ApiViewModel = _ruleService.GetRules<T>().Select(r => r as ApiViewModel)
                },
                new RuleController());
        }

        private IEnumerable<string> GetHeader<T>(T model)
        {
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();

                if (attribute == null || attribute.ParamType != ParamType.None)
                {
                    yield return property.Name;
                }
            }
        }
    }
}
