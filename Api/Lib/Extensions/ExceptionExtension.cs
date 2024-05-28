using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Lib.Extensions
{
	public static class ExceptionExtension
	{
		public static string GetMessage(this Exception exception)
		{
			string message = exception.Message;
			if (exception.InnerException != null)
				message = $"{message}{Environment.NewLine}{exception.InnerException.Message}";

			return message;
		}
	}
}
