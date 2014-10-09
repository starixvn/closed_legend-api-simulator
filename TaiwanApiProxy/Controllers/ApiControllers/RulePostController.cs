namespace TaiwanApiProxy.Controllers
{
    using System;
    using System.Web;
    using System.Web.Http;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Services;
    using TaiwanApiProxy.ViewModels;
    using TaiwanApiProxy.ViewResults;

    public class RulePostController : ApiController
    {
        private readonly IRuleService _ruleService;

        public RulePostController()
        {
            _ruleService = new RuleService();
        }

        [AcceptVerbs("POST")]
        public JsonViewResult CheckUserName(CheckUsernameViewModel model)
        {
            return GetResult<CheckUsernameViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult InsertNewMember(InsertNewMemberViewModel model)
        {
            return GetResult<InsertNewMemberViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult ResetPassword(ResetPasswordViewModel model)
        {
            return GetResult<ResetPasswordViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult UpdateMemberInfo(UpdateMemberInfoViewModel model)
        {
            return GetResult<UpdateMemberInfoViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult UpdateMemberStatus(UpdateMemberStatusViewModel model)
        {
            return GetResult<UpdateMemberStatusViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult GetMemberInfo(MemberInfoViewModel model)
        {
            return GetResult<MemberInfoViewModel>(model);
        }

        [AcceptVerbs("POST")]
        [QueryString(Help = 0)]
        public JsonViewResult GetMemberInfoList(MemberInfoListViewModel model)
        {
            return GetResult<MemberInfoListViewModel>(model);
        }

        [AcceptVerbs("POST")]
        [QueryString(Help = 1)]
        public JsonViewResult GetMemberInfoList(MemberInfoListCustIdsViewModel model)
        {
            return GetResult<MemberInfoListCustIdsViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult UpdateAgentId(UpdateAgentIdViewModel model)
        {
            return GetResult<UpdateAgentIdViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult GetDepositMemberInfoList(DepositMemberInfoListViewModel model)
        {
            return GetResult<DepositMemberInfoListViewModel>(model);
        }

        [AcceptVerbs("POST")]
        public JsonViewResult GetDepositMemberInfo(DepositMemberInfoViewModel model)
        {
            return GetResult<DepositMemberInfoViewModel>(model);
        }

        private JsonViewResult GetResult<T>(T model)
        {
            try
            {
                var method = (Method)Enum.Parse(typeof(Method), HttpContext.Current.Request.QueryString["method"].ToString());

                switch (method)
                {
                    case Method.Add:
                        _ruleService.Insert<T>(model);
                        break;
                    case Method.Delete:
                        _ruleService.Delete<T>(model);
                        break;
                    case Method.ChangeStatus:
                        _ruleService.ChangeStatus<T>(model);
                        break;
                    case Method.Edit:
                        _ruleService.Edit<T>(model);
                        break;
                    case Method.Sort:
                        _ruleService.Sort<T>(model);
                        break;
                }

                return new JsonViewResult { ErrCode = (short)ErrCode.Success };
            }
            catch (Exception exception)
            {
                return new JsonViewResult { ErrCode = (short)ErrCode.Fail, ErrMsg = exception.Message };
            }
        }
    }
}
