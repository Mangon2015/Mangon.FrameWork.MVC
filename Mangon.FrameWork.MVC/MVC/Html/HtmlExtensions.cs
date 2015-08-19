using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Mangon.FrameWork.Package.Storage;
using System.IO;
using System.Net;

namespace Mangon.FrameWork.MVC
{
    public class SelectItem : SelectListItem
    {
        public string CssClass { get; set; }
    }
    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>()
        {
            return (from int value in Enum.GetValues(typeof(TEnum)) select new SelectListItem { Value = value.ToString(), Text = Enum.GetName(typeof(TEnum), value) }).ToList();
        }
        public static SelectList ToSelectList<TEnum>(object valuesSelected)
        {
            return new SelectList(ToSelectList<TEnum>(), "Value", "Text", valuesSelected);
        }

        #region Enums DropdownList
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, Type type)
        {
            return DropDownList(helper, name, type, null);
        }
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, Type type, object selected)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("Type is not a enum.");
                
            }
            if (selected !=null&&selected.GetType()!=type)
            {
                throw new ArgumentException("Selected object is not " + type);
            }

            var enums = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(type))
            {
                var item = new SelectListItem { Value = value.ToString(), Text = Enum.GetName(type, value).Replace("_", " ") };
                if (selected!=null)
                {
                    item.Selected = (int)selected == value;
                }
            }
            return helper.DropDownList(name, enums);
        }
        #endregion

        #region ToSelectList
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable,Func<T,string> value)
        {
            return enumerable.ToSelectList(value, value, null);
        }
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value, object selectValue)
        {
            return enumerable.ToSelectList(value, value, selectValue);
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text)
        {
            return enumerable.ToSelectList(value, text, null);
        }
        /// <summary>
        ///  Returns an IEnumerable<SelectListItem></SelectListItem>by using the specified items for data value field, the data text field, and a selected value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text, object selectedValue)
        {
            return enumerable.Select(f => new SelectListItem
            {
                Value = value(f),
                Text = text(f),
                Selected = value(f).Equals(selectedValue)
            });
        }


        #endregion


        public static IHtmlString CheckBox(this HtmlHelper helper, string name, bool isChecked, string lable, object htmlAttributes)
        {
            var check = new TagBuilder("input");
            if (isChecked)
            {
                check.MergeAttribute("checked", "checked");
            }
            check.MergeAttribute("id", name);
            check.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            check.MergeAttribute("name", name);
            check.MergeAttribute("type", "checkbox");
            var labelTag = new TagBuilder("lable");
            labelTag.MergeAttribute("for", name);
            return MvcHtmlString.Create(string.Format("{0}:{1}",check.ToString(TagRenderMode.SelfClosing),labelTag.ToString(TagRenderMode.SelfClosing)));

        }

        #region CheckBoxGroup

        public static MvcHtmlString CheckBoxGroup(this HtmlHelper helper, string name, IEnumerable<SelectItem> values, object htmlAttributes)
        {
            return CheckBoxGroup(helper, name, values, null, htmlAttributes);

        }


        public static MvcHtmlString CheckBoxGroup(this HtmlHelper helper, string name, IEnumerable<SelectItem> values, object valuesSelected, object htmlAttributes)
        {
            List<object> _selected = null;

            if (valuesSelected != null)
            {
                _selected = new List<object> { valuesSelected };
            }

            return CheckBoxGroup(helper, name, values, _selected, htmlAttributes);

        }

        public static MvcHtmlString CheckBoxGroup(this HtmlHelper helper, string name, IEnumerable<SelectItem> values, IEnumerable<object> valuesSelected, object htmlAttributes)
        {
            var sb = new StringBuilder();

            if (values != null)
            {
                // Create a radio button for each item in the list
                foreach (var item in values)
                {
                    // Generate an id to be given to the radio button field
                    var _id = string.Format("{0}_{1}", name, !string.IsNullOrWhiteSpace(item.Value) ? item.Value : DateTime.UtcNow.Ticks.ToString()).ToLower();

                    var _selected = valuesSelected != null && valuesSelected.Any(x => x.ToString().ToLower() == item.Value.ToLower());

                    var _checkbox = string.Format("<input type=\"checkbox\" name=\"{0}\" value=\"{1}\" id=\"{2}\"{3} />", name, HttpUtility.HtmlEncode(item.Value), _id, _selected ? " checked=\"checked\"" : string.Empty); // helper.CheckBox(name, _selected, new {id = _id, value = item.Value}).ToHtmlString();

                    var _label = helper.Label(_id, item.Text).ToHtmlString();

                    sb.AppendFormat("<li class=\"{2}\">{0}{1}</li>", _checkbox, _label, item.CssClass);

                }
            }

            var tag = new TagBuilder("ul");
            tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            tag.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));

        }


        #endregion


        #region RadioButtonGroup

        public static MvcHtmlString RadioButtonGroup(this HtmlHelper helper, string name, IEnumerable<SelectItem> values, object htmlAttributes)
        {
            return RadioButtonGroup(helper, name, values, null, htmlAttributes);
        }
        public static MvcHtmlString RadioButtonGroup(this HtmlHelper helper, string name, IEnumerable<SelectItem> values, object valueSelected, object htmlAttributes)
        {
            var sb = new StringBuilder();

            if (values != null)
            {
                // Create a radio button for each item in the list
                foreach (var item in values)
                {
                    // Generate an id to be given to the radio button field
                    var _id = string.Format("{0}_{1}", name, !string.IsNullOrWhiteSpace(item.Value) ? item.Value : DateTime.UtcNow.Ticks.ToString()).ToLower();

                    var _radioButton =
                        helper.RadioButton(name, item.Value,
                                           (valueSelected != null &&
                                            String.Compare(item.Value, valueSelected.ToString(),
                                                           StringComparison.OrdinalIgnoreCase) == 0), new { id = _id }).ToHtmlString();

                    var _label = helper.Label(_id, HttpUtility.HtmlEncode(item.Text)).ToHtmlString();

                    sb.AppendFormat(
                        "<li class=\"{2}\">{0}{1}</li>", _radioButton, _label, item.CssClass);

                }
            }

            var tag = new TagBuilder("ul");
            tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            tag.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString RadioButtonGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                // Create a radio button for each item in the list
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field
                    var id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);

                    // Create and populate a radio button using the existing html helpers
                    var label = htmlHelper.Label(id, item.Text);
                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id = id }).ToHtmlString();

                    // Create the html string that will be returned to the client
                    // e.g. <input data-val="true" data-val-required="You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label>
                    sb.AppendFormat("<div class=\"radio-button-group\">{0}{1}</div>", radio, label);
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion



        #region Link & Image Link

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string href, object hrefAttributes, string imageSrc, object imageAttributes)
        {
            var imgHtml = Image(helper, imageSrc, imageAttributes).ToHtmlString();
            var tag = new TagBuilder("a");
            tag.MergeAttributes(new RouteValueDictionary(hrefAttributes));
            tag.MergeAttribute("href", href, true);
            tag.InnerHtml = imgHtml;
            string html = tag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }


        public static MvcHtmlString Image(this HtmlHelper helper, string src, object attributes)
        {

            var tag = new TagBuilder("img");
            tag.MergeAttributes(new RouteValueDictionary(attributes));
            tag.MergeAttribute("src", src, true);
            string imgHtml = tag.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(imgHtml);
        }


        public static MvcHtmlString Link(this HtmlHelper helper, string href, string text, object attributes)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttributes(new RouteValueDictionary(attributes));
            tag.MergeAttribute("href", href, true);
            tag.InnerHtml = text;
            string html = tag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString Link(this HtmlHelper helper, string href, string text)
        {
            return Link(helper, href, text, new { @title = text });
        }




        #endregion

        public static IHtmlString Title(this HtmlHelper helper, string val)
        {
            return MvcHtmlString.Create(string.Format("<title>{0}</title>", helper.Encode(val)));
        }
            

        public static string IncludeFile(this HtmlHelper helper,string path,bool fromHttp=true)
    {
        string data = "";
        try
        {
            var st = (SimplyCacheStorage)Mangon.FrameWork.Package.Storage.StorageFactory.GetInstance("simplycache", "Include");
            var r = st.Get(path);
            if (!r.Bool)
            {
                String file = HttpContext.Current.Server.MapPath(path);
                if (!File.Exists(file))
                {
                    return "";
                }
                using (WebClient wc=new WebClient())
                {
                    if (fromHttp)
                    {
                        String header = helper.ViewContext.RequestContext.HttpContext.Request.Url.Scheme + "://" + helper.ViewContext.RequestContext.HttpContext.Request.Url.Host;
                        string url = "";
                        if (path.StartsWith("~"))
                        {
                            url = path.Substr(1);
                        }
                        else
                        {
                            url = path;
                        }
                        data = wc.DownloadString(header + url);
                    }
                    else
                    {
                        data = wc.DownloadString(file);
                    }
                    st.Set(path, data);
                }
            }
            else
            {
                data = r.Data.ToString();
            }
        }
        catch (Exception e)
        {

            data = e.Message;
        }
        return data;
    }


    }
}
