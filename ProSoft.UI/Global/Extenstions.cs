using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProSoft.UI.Global
{
    public static class Extenstions
    {
        public static List<SelectListItem> ToSelectListItem<T>(this List<T> list,Func<T,string> textSelector,Func<T,string> valueSelector){
            var selectListItems = list.Select(item => new SelectListItem
            {
                Text = textSelector(item),
                Value = valueSelector(item)
            }).ToList();
            return selectListItems;

        }
    }
}
