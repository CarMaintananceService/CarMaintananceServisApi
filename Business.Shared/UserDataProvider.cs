using Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Business.Shared;
using Newtonsoft.Json;

namespace Business.Shared
{
	public class UserDataProvider
    {
		Dictionary<int, Dictionary<int, int>> rights = null;
	

		private ClaimsPrincipal? User { get; set; }
		public UserDataProvider()
		{
		}

		public UserDataProvider Set(ClaimsPrincipal principal)
		{
			User = principal;
			return this;
		}


		public bool IsAvailable()
		{
			return this.User != null && this.User.Claims?.Count() > 0;
		}

		

		public int Get_Id()
		{
			string user_id = User.Claims?.FirstOrDefault(x => x.Type == ClaimType.UserId.ToString()).Value;
			return Convert.ToInt32(user_id ?? "0");
		}

	

		public string Get_UserFullName()
		{
			return Uri.UnescapeDataString(User?.Claims.FirstOrDefault(x => x.Type == ClaimType.UserName.ToString()).Value ?? "");
		}

		public int Get_RIGHT(int moduleId, int rightId)
		{
			if (this.rights == null)
				this.rights = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(User?.Claims.FirstOrDefault(x => x.Type == ClaimType.RightsData.ToString()).Value);

			return rights[moduleId][rightId];
		}

		public Dictionary<int, int> Get_RIGHTS(int moduleId)
		{
			if (this.rights == null)
				this.rights = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(User?.Claims.FirstOrDefault(x => x.Type == ClaimType.RightsData.ToString()).Value);

			return rights[moduleId];
		}

		public bool HavePermission(int rightName)
		{
			string yetkiler = User?.Claims.FirstOrDefault(x => x.Type == ClaimType.RightsData.ToString()).Value;
			return yetkiler.Contains($",{rightName},");
		}

	}
}
