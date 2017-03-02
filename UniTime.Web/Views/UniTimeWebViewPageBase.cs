using Abp.Web.Mvc.Views;

namespace UniTime.Web.Views
{
    public abstract class UniTimeWebViewPageBase : UniTimeWebViewPageBase<dynamic>
    {

    }

    public abstract class UniTimeWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected UniTimeWebViewPageBase()
        {
            LocalizationSourceName = UniTimeConsts.LocalizationSourceName;
        }
    }
}