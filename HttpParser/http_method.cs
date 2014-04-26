using System;

namespace HttpParser
{
	public enum http_method : int
	{
		HTTP_DELETE,
		HTTP_GET,
		HTTP_HEAD,
		HTTP_POST,
		HTTP_PUT,
		HTTP_CONNECT,
		HTTP_OPTIONS,
		HTTP_TRACE,
		HTTP_COPY,
		HTTP_LOCK,
		HTTP_MKCOL,
		HTTP_MOVE,
		HTTP_PROPFIND,
		HTTP_PROPPATCH,
		HTTP_SEARCH,
		HTTP_UNLOCK,
		HTTP_REPORT,
		HTTP_MKACTIVITY,
		HTTP_CHECKOUT,
		HTTP_MERGE,
		HTTP_MSEARCH,
		HTTP_NOTIFY,
		HTTP_SUBSCRIBE,
		HTTP_UNSUBSCRIBE,
		HTTP_PATCH,
		HTTP_PURGE,
	}

	public partial class RawHttpParser
	{
		private static string[] methodString = new string[] {
			"DELETE",
			"GET",
			"HEAD",
			"POST",
			"PUT",
			"CONNECT",
			"OPTIONS",
			"TRACE",
			"COPY",
			"LOCK",
			"MKCOL",
			"MOVE",
			"PROPFIND",
			"PROPPATCH",
			"SEARCH",
			"UNLOCK",
			"REPORT",
			"MKACTIVITY",
			"CHECKOUT",
			"MERGE",
			"MSEARCH",
			"NOTIFY",
			"SUBSCRIBE",
			"UNSUBSCRIBE",
			"PATCH",
			"PURGE",
		};
	}
}
