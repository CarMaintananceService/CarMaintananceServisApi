
using DocumentFormat.OpenXml.EMMA;
using System.Text.RegularExpressions;

namespace Api.Lib
{
	public class DtoRegexHelper
	{
		//https://www.regular-expressions.info/lookaround.html
		//https://www.programiz.com/csharp-programming/regex
		/// <summary>
		/// 
		/// </summary>
		/// <param name="className"></param>
		/// <param name="moduleName"></param>
		/// <returns></returns>
		public int FindClassName(string className, string moduleName)
		{
			string code = _getClass();


			 
			var mac = Regex.Matches(code, @"^((\s*///\s*<summary>[\s\S]+?/// </summary>[\s\t\r\n]*)public[\s\t]+(string|int|byte|bool|DateTime|double|\w*)\??)([\s\t]+)\w*([\s\t]+)(?={)", RegexOptions.Multiline);
			//code = Regex.Replace(code, pattern, "", RegexOptions.Multiline);


			string pattern = @"((public)?[\s\r\n\t]+)+class[\s\S]+?(?=" + className + @")\s?\w?(\s\n|\r|\r\n)?(?={)";
			//pattern = @"((public)?[\s\r\n\t]+)+class[\s\S]+?(" + className + @")[\s\S]*?({)";
			pattern = @"((public)?[\s\r\n\t]+)+class[\s\S]+?(" + className + @")[\s\S]*?(?={)";
			//var matches = Regex.Matches(Code, pattern, RegexOptions.Multiline);
			//var classes = matches.Cast<Match>().Select(x => x.Value.Trim());


			code = Regex.Replace(code, @"((namespace)[\s\r\n\t]+)Core.[\s\S]*?(?={)", $"\r\nnamespace Business.Shared.{moduleName}.{className}.Dtos\r\n", RegexOptions.Multiline);

			//attributes of class
			var ma = Regex.Matches(code, @"(\[){1}[\s\S\r\n\t]*?(\]){1}[\t\r\n\s]*(?=public class)", RegexOptions.Multiline);
			code = Regex.Replace(code, @"(\[){1}[\s\S\r\n\t]*?(\]){1}[\t\r\n\s]*(?=public class)", $"", RegexOptions.Multiline);



			//attributes
			ma = Regex.Matches(code, @"(\[){1}[\s\S\r\n\t]*?(\]){1}[\t\r\n\s]*", RegexOptions.Multiline);
			code = Regex.Replace(code, @"(\[){1}[\s\S\r\n\t]*?(\]){1}[\t\r\n\s]*", $"", RegexOptions.Multiline);


			//dto name
			pattern = @"(public){1}[\s\r\n\t]+(class){1}[\s\S]+?(" + className + @"){1}[\s\S]+?(?={)";
			//pattern = @"((public)?[\s\r\n\t]+)+class[\s\S]+?(" + className + @")[\s\S]*?(?={)";
			ma = Regex.Matches(code, pattern, RegexOptions.Multiline);
			code = Regex.Replace(code, pattern, "\r\npublic class UserGroupOutput : BaseDto\r\n", RegexOptions.Multiline);


			//remove enumerables
			pattern = @"[\s\r\n\t]*(public (IEnumerable|Collection|List|IQueryable)){1}[\s\S]*?(\}){1}";
			ma = Regex.Matches(code, pattern, RegexOptions.Multiline);
			code = Regex.Replace(code, pattern, "", RegexOptions.Multiline);


			//summary documentation
			pattern = @"^\s*public[\s\t]+(string|int|byte|bool)\??\s+[^\s]+\s+\{[^\}\n]+\}";
			ma = Regex.Matches(code, pattern, RegexOptions.Multiline);
			//code = Regex.Replace(code, pattern, "", RegexOptions.Multiline);


			return 1;
		}

		public void FindPropsTypes()
		{
			string code = _getClass();
			var matces = Regex.Matches(code, @"^(([\s\t\r\n]*)public[\s\t]+(string|int|byte|bool|DateTime|double|\w*)\??)([\s\t]+)\w*([\s\t]+)(?={)", RegexOptions.Multiline);

			Dictionary<string, string> props = new Dictionary<string, string>();

			foreach (Capture mat in matces)
			{
				var matchProp = Regex.Match(mat.Value, @"(?<=public[\s\t]+(string|int|byte|bool|DateTime|double|\w*)\??[\s\t]+)(\w*)[\s\S]+?(?=[\s\t]+)", RegexOptions.Multiline);
				string prop = matchProp.Value;

				var matchPropType = Regex.Match(mat.Value, @"(?<=public[\s\t]+)(string|int|byte|bool|DateTime|double|\w*)\??(?=[\s\t]+(\w*)[\s\S]+?[\s\t]+)", RegexOptions.Multiline);
				string propType = matchPropType.Value;

				props.Add(prop, propType);
			}


		}



		public void FindPropsDocumentation()
		{
			string code = _getClass();
			var matces = Regex.Matches(code, @"^((\s*///\s*<summary>[\s\S]+?/// </summary>[\s\t\r\n]*)public[\s\t]+(string|int|byte|bool|DateTime|double|\w*)\??)([\s\t]+)\w*([\s\t]+)(?={)", RegexOptions.Multiline);

			Dictionary<string,string> props = new Dictionary<string,string>();	

			foreach(Capture mat in matces) {
				
				var matchProp = Regex.Match(mat.Value, @"(?<=[\s\t\r\n]+///\s*<summary>[\s\t\r\n]+///)[\s\S]+?(?=[\s\t\r\n]+/// </summary>)", RegexOptions.Multiline);
				string documentation = matchProp.Value;

				matchProp = Regex.Match(mat.Value, @"(?<=public[\s\t]+(string|int|byte|bool|DateTime|double|\w*)\??[\s\t]+)(\w*)[\s\S]+?(?=[\s\t]+)", RegexOptions.Multiline);
				string prop = matchProp.Value;

				var matchPropType = Regex.Match(mat.Value, @"(?<=public[\s\t]+)(string|int|byte|bool|DateTime|double|\w*)\??(?=[\s\t]+(\w*)[\s\S]+?[\s\t]+)", RegexOptions.Multiline);
				string propType = matchPropType.Value;

				props.Add(prop, documentation);
			}
		

		}


		private string _getClass()
		{

			return @"
				[Table(""SEC_UserRight"")]
				public class UserRight : BaseRight
				{
					public int UserId { get; set; }
					public int RightDomainId { get; set; }


					public User? User { get; set; }
					public RightDomain? RightDomain { get; set; }
        
        
					public bool IsTakeValueFromGroup { get; set; }

        
				}
			";


			return @"
using Core.Entities;
using Core.Shared;
using Core.Shared.Entities;
using Core.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LawRule
{

	[Table(""LAW_ParametersLaw"")]
	public class LawParameter : AuditedEntity, IHasCompany
	{
		public int CompanyId { get; set; }
		public long? IntegrationAppId { get; set; }


		public Company? Company { get; set; }


		


		/// <summary>
		/// (12)Geçerlilik Başlangıç Tarihi
		/// </summary>
		public DateTime BeginDate { get; set; }

		/// <summary>
		/// Geçerlilik Bitiş Tarihi
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Engelli Vergi İndirimi 1.Derece
		/// </summary>
		public double TaxDiscountsInjuryLevel1Amount { get; set; }

		/// <summary>
		/// Engelli indirimi 1. derece hesaplama birimi
		/// </summary>
		public InjuryCalculationType TaxDiscountsInjuryLevel1Typ { get; set; }

		/// <summary>
		/// Engelli Vergi İndirimi 2.Derece
		/// </summary>
		public double TaxDiscountsInjuryLevel2Amount { get; set; }

		/// <summary>
		/// Engelli indirimi 2. derece hesaplama birimi
		/// </summary>
		public InjuryCalculationType TaxDiscountsInjuryLevel2Typ { get; set; }

		/// <summary>
		/// Engelli Vergi İndirimi 3.Derece
		/// </summary>
		public double TaxDiscountsInjuryLevel3Amount { get; set; }

		/// <summary>
		/// Engelli indirimi 3. derece hesaplama birimi
		/// </summary>
		public InjuryCalculationType TaxDiscountsInjuryLevel3Typ { get; set; }

		/// <summary>
		/// Damga Vergisi Kesinti Oranı
		/// </summary>
		public double StampTaxBaseRatio { get; set; }

		/// <summary>
		/// Çalışan SGK Prime Esas Kazanç Günlük Minumum Tutar
		/// </summary>
		public double EmployeeSocialSecurityDailyMinBase { get; set; }

		/// <summary>
		/// Çırak SGK Prime Esas Kazanç Günlük Minumum Tutar
		/// </summary>
		public double ApprenticeSocialSecurityDailyMinBase { get; set; }

		/// <summary>
		/// Çalışan SGK Prime Esas Kazanç Günlük Maximum Tutar
		/// </summary>
		public double EmployeeSocialSecurityDailyMaxBase { get; set; }

		/// <summary>
		/// Çırak SGK Prime Esas Kazanç Günlük Maximum Tutar
		/// </summary>
		public double ApprenticeSocialSecurityDailyMaxBase { get; set; }

		/// <summary>
		/// Çalışan Asgari Ücret
		/// </summary>
		public double EmployeeMinWage { get; set; }

		/// <summary>
		/// Çırak Asgari Ücret
		/// </summary>
		public double ApprenticeMinWage { get; set; }

		/// <summary>
		/// Vergiden Muaf Çocuk Sayısı
		/// </summary>
		public int ChildirenTaxExceptionMaxCount { get; set; }

		/// <summary>
		/// 6 Yaşından küçükler için muafiyet çocuk başı
		/// </summary>
		public double ChildirenTaxExceptionUnitAmountAgeMinThan { get; set; }

		/// <summary>
		/// 6 Yaşından büyükler için muafiyet çocuk başı
		/// </summary>
		public double ChildirenTaxExceptionUnitAmountAgeMoreThan { get; set; }

		/// <summary>
		/// Damga Vergisi Muafiyet çocuk sayısı
		/// </summary>
		public int ChildirenStampTaxExceptionMaxCount { get; set; }

		/// <summary>
		/// 1=Gruptan Oku 2=Birebir Hesapla
		/// </summary>
		public SeverancePayType SeverancePayType { get; set; }

		/// <summary>
		/// Yıllık Kıdem Tazminatı Hakedişi
		/// </summary>
		public double SeverancePayEarnDayForEachYear { get; set; }

		/// <summary>
		/// Kıdem Tazminat Tavanı
		/// </summary>
		public double SeverancePayMaxBase { get; set; }

		/// <summary>
		/// Özel Sağlık sigortası Maximum tavan oranı
		/// </summary>
		public double HealthInsuaranceControlBaseRate { get; set; }

		/// <summary>
		/// Hayat sigortası maximum tavan oranı
		/// </summary>
		public double LifeInsuaranceControlBaseRate { get; set; }

		/// <summary>
		/// Kıdem tazminatında kullanılacak yılın gün sayısı
		/// </summary>
		public int SeverancePayDayOfYear { get; set; }

		/// <summary>
		/// Aile Yardımı SGK İstisna Oranı | Baz
		/// </summary>
		public double FamilyPaidSocialSecurityExceptionBaseRate { get; set; }

		/// <summary>
		/// Yemek Yardımı SGK İstisna Oranı | Baz
		/// </summary>
		public double MealPaidSocialSecurityBaseExceptionBaseRate { get; set; }

		/// <summary>
		/// Yemek Yardımı SGK İstisna Oranı | Günlük
		/// </summary>
		public double MealPaidTaxBaseExceptionDailyUnitAmount { get; set; }

		/// <summary>
		/// Otomatik katılım Bireysel Emeklilik Kesinti Oranı
		/// </summary>
		public double AutomaticJoinInsuaranceMinBaseRate { get; set; }

		/// <summary>
		/// Ulaşım Yardımı Günlük Vergi İstisna Tutarı
		/// </summary>
		public double TransportationPaidTaxBaseExceptionDailyBaseUnitAmount { get; set; }







		public IEnumerable<LawParameterNoticePayDay>? LawParameterNoticePayDays { get; set; } 
		public IEnumerable<LawParameterVacationPayDay>? LawParameterVacationPayDays { get; set; } 
		public IEnumerable<LawParameterTaxRate>? LawParameterTaxRates { get; set; }
		public IEnumerable<LawParameterSocialSecurityRate>? LawParameterSocialSecurityRates { get; set; }



	}
}

";
		}

	}


		 
}
