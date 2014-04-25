using System;
using System.Runtime.InteropServices;

namespace HttpParser
{
	[StructLayout(LayoutKind.Sequential)]
	struct http_parser
	{
#pragma warning disable 0169
		// yeah I know that they are not used!
		byte typeFlags;
		byte state;
		byte header_state;
		byte index;

		uint nread;
		ulong content_length;
#pragma warning restore 0169

		// read only
		public readonly short http_major;
		public readonly short http_minor;
		public readonly short status_code;
		public readonly byte method;
		public readonly byte errorUpgrade;

		public IntPtr data;

		public bool Upgrade {
			get {
				return (errorUpgrade & 128) == 128;
			}
		}

		public http_errno Error {
			get {
				return (http_errno)(errorUpgrade & 127);
			}
		}
	}
}

