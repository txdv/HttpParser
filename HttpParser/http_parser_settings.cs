using System;
using System.Runtime.InteropServices;

namespace HttpParser
{
	[StructLayout(LayoutKind.Sequential)]
	struct http_parser_settings
	{
		public IntPtr on_message_begin;
		public IntPtr on_url;
		public IntPtr on_header_field;
		public IntPtr on_header_value;
		public IntPtr on_headers_complete;
		public IntPtr on_body;
		public IntPtr on_message_complete;
	}
}

