using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Shared
{
    public static class ExtensionsMethods
    {
        public static string EnsureEndsWith(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : value.Insert(value.Length, suffix);
        }

		public static string GetDisplayName(this Enum enumValue, UserDataProvider userDataProvider)
		{
			string desc = enumValue.GetType()
			  .GetMember(enumValue.ToString())
			  .First()
			  .GetCustomAttribute<DisplayAttribute>()
			  ?.GetName();

			Match mat = Regex.Match(desc ?? "", @"(?<=\|\s*label:)[\w\s]{1,}(?=\|)");
			if (mat != null && mat.Success && string.IsNullOrEmpty(mat.Value?.Trim()))
				return mat.Value;
			else
				return enumValue.ToString();


			//Match mat = Regex.Match(desc, @"(?<=\|\s*no:)[\d]+?(?=\|)");
			//if(mat != null && mat.Success && string.IsNullOrEmpty(mat.Value?.Trim()) && mat.Value?.Trim() != "0") {

			//	//messageNo = Convert.ToInt32(mat.Value);
			//	//userDataProvider.Get_LangId();
			//	return enumValue.ToString();
			//}
			//else
			//{
			//	mat = Regex.Match(desc, @"(?<=\|\s*label:)[\w\s]{1,}(?=\|)");
			//	if (mat != null && mat.Success && string.IsNullOrEmpty(mat.Value?.Trim()))
			//		return mat.Value;
			//	else
			//		return enumValue.ToString();
			//}
		}


	}
}
