using System;

namespace HttpParser
{
	public enum http_errno : int
	{
		HPE_OK,
		HPE_CB_message_begin,
		HPE_CB_url,
		HPE_CB_header_field,
		HPE_CB_header_value,
		HPE_CB_headers_complete,
		HPE_CB_body,
		HPE_CB_message_complete,
		HPE_CB_status,
		HPE_INVALID_EOF_STATE,
		HPE_HEADER_OVERFLOW,
		HPE_CLOSED_CONNECTION,
		HPE_INVALID_VERSION,
		HPE_INVALID_STATUS,
		HPE_INVALID_METHOD,
		HPE_INVALID_URL,
		HPE_INVALID_HOST,
		HPE_INVALID_PORT,
		HPE_INVALID_PATH,
		HPE_INVALID_QUERY_STRING,
		HPE_INVALID_FRAGMENT,
		HPE_LF_EXPECTED,
		HPE_INVALID_HEADER_TOKEN,
		HPE_INVALID_CONTENT_LENGTH,
		HPE_INVALID_CHUNK_SIZE,
		HPE_INVALID_CONSTANT,
		HPE_INVALID_INTERNAL_STATE,
		HPE_STRICT,
		HPE_PAUSED,
		HPE_UNKNOWN,
	}

	public partial class RawHttpParser
	{
		private static string[] errorString = new string[] {
			"success",
			"the on_message_begin callback failed",
			"the on_url callback failed",
			"the on_header_field callback failed",
			"the on_header_value callback failed",
			"the on_headers_complete callback failed",
			"the on_body callback failed",
			"the on_message_complete callback failed",
			"the on_status callback failed",
			"stream ended at an unexpected time",
			"too many header bytes seen; overflow detected",
			"data received after completed connection: close message",
			"invalid HTTP version",
			"invalid HTTP status code",
			"invalid HTTP method",
			"invalid URL",
			"invalid host",
			"invalid port",
			"invalid path",
			"invalid query string",
			"invalid fragment",
			"LF character expected",
			"invalid character in header",
			"invalid character in content-length header",
			"invalid character in chunk size header",
			"invalid constant string",
			"encountered unexpected internal state",
			"strict mode assertion failed",
			"parser is paused",
			"an unknown error occurred",
		};
	}
}
