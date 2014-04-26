using System;
using System.Runtime.InteropServices;

namespace HttpParser
{
	unsafe public partial class RawHttpParser : IDisposable
	{
		http_parser *parser;
		http_parser_settings *settings;

		Func<IntPtr, int>                 onMessageBegin;
		Func<IntPtr, IntPtr, IntPtr, int> onUrl;
		Func<IntPtr, IntPtr, IntPtr, int> onStatus;
		Func<IntPtr, IntPtr, IntPtr, int> onHeaderField;
		Func<IntPtr, IntPtr, IntPtr, int> onHeaderValue;
		Func<IntPtr, int>                 onHeadersComplete;
		Func<IntPtr, IntPtr, IntPtr, int> onBody;
		Func<IntPtr, int>                 onMessageComplete;

		public RawHttpParser()
			: this(http_parser_type.HTTP_BOTH)
		{
		}

		public RawHttpParser(http_parser_type type)
		{
			ParserPointer = Marshal.AllocHGlobal(sizeof(http_parser));
			SettingsPointer = Marshal.AllocHGlobal(sizeof(http_parser_settings));
			http_parser_init(ParserPointer, type);

			onMessageBegin    = OnMessageBegin;
			onUrl             = OnUrl;
			onStatus          = OnStatus;
			onHeaderField     = OnHeaderField;
			onHeaderValue     = OnHeaderValue;
			onHeadersComplete = OnHeadersComplete;
			onBody            = OnBody;
			onMessageComplete = OnMessageComplete;

			settings->on_message_begin    = Marshal.GetFunctionPointerForDelegate(onMessageBegin);
			settings->on_status           = Marshal.GetFunctionPointerForDelegate(onStatus);
			settings->on_url              = Marshal.GetFunctionPointerForDelegate(onUrl);
			settings->on_header_field     = Marshal.GetFunctionPointerForDelegate(onHeaderField);
			settings->on_header_value     = Marshal.GetFunctionPointerForDelegate(onHeaderValue);
			settings->on_headers_complete = Marshal.GetFunctionPointerForDelegate(onHeadersComplete);
			settings->on_body             = Marshal.GetFunctionPointerForDelegate(onBody);
			settings->on_message_complete = Marshal.GetFunctionPointerForDelegate(onMessageComplete);
		}

		~RawHttpParser()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			/*
			if (ParserPointer != IntPtr.Zero) {
				Marshal.FreeHGlobal(ParserPointer);
			}
			ParserPointer = IntPtr.Zero;

			if (SettingsPointer != IntPtr.Zero) {
				Marshal.FreeHGlobal(SettingsPointer);
			}
			SettingsPointer = IntPtr.Zero;
			*/
		}

		protected virtual int OnMessageBegin(IntPtr ptr)
		{
			return 0;
		}
		protected virtual int OnUrl(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return 0;
		}
		protected virtual int OnStatus(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return 0;
		}
		protected virtual int OnHeaderField(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return 0;
		}
		protected virtual int OnHeaderValue(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return 0;
		}
		protected virtual int OnHeadersComplete(IntPtr ptr)
		{
			return 0;
		}
		protected virtual int OnBody(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return 0;
		}
		protected virtual int OnMessageComplete(IntPtr ptr)
		{
			return 0;
		}

		public IntPtr ParserPointer {
			get {
				return (IntPtr)parser;
			}
			private set {
				parser = (http_parser *)value;
			}
		}

		public IntPtr SettingsPointer {
			get {
				return (IntPtr)settings;
			}
			private set {
				settings = (http_parser_settings *)value;
			}
		}

		public int HttpMajor {
			get {
				return parser->http_major;
			}
		}

		public int HttpMinor {
			get {
				return parser->http_minor;
			}
		}

		public int StatusCode {
			get {
				return parser->status_code;
			}
		}

		public http_method Method {
			get {
				return (http_method)parser->method;
			}
		}

		public string MethodString {
			get {
				return methodString[(int)Method];
			}
		}

		public bool Upgrade {
			get {
				return parser->Upgrade;
			}
		}

		public http_errno Errno {
			get {
				return parser->Error;
			}
		}

		public string ErrorName {
			get {
				return new string(http_errno_name(Errno));
			}
		}

		public string ErrorDescription {
			get {
				return new string(http_errno_description(Errno));
			}
		}

		public void YieldException()
		{
			if (Errno != http_errno.HPE_OK) {
				// TODO: create exceptions for every error
				throw new Exception(string.Format("{0}: {1}", ErrorName, ErrorDescription));
			}
		}

		public bool ShouldKeepAlive {
			get {
				return http_should_keep_alive(ParserPointer) != 0;
			}
		}

		public IntPtr Pointer { get; private set; }

		public void Execute(IntPtr data, int offset, int count)
		{
			Pointer = data + offset;
			http_parser_execute(ParserPointer, SettingsPointer, Pointer, (IntPtr)count);
		}

		public void Pause()
		{
			http_parser_pause(ParserPointer, 1);
		}

		public void Unpause()
		{
			http_parser_pause(ParserPointer, 0);
		}

		/// <summary>
		/// Shows if the currently parsed chunk file is final, works only within
		/// the the OnBody method
		/// </summary>
		/// <value><c>true</c> if http body is final; otherwise, <c>false</c>.</value>
		public bool HttpBodyIsFinal {
			get {
				return http_body_is_final(ParserPointer) != 0;
			}
		}

		[DllImport("http_parser")]
		private static extern void http_parser_init(IntPtr parser, http_parser_type type);

		[DllImport("http_parser")]
		private static extern IntPtr http_parser_execute(IntPtr parser, IntPtr settings, IntPtr data, IntPtr length);

		[DllImport("http_parser")]
		private static extern int http_should_keep_alive(IntPtr parser);

		[DllImport("http_parser")]
		private static extern sbyte *http_method_str(http_method m);

		[DllImport("http_parser")]
		private static extern sbyte *http_errno_name(http_errno err);

		[DllImport("http_parser")]
		private static extern sbyte *http_errno_description(http_errno err);

		[DllImport("http_parser")]
		internal static extern long http_parser_version();

		[DllImport("http_parser")]
		internal static extern void http_parser_pause(IntPtr parser, int paused);

		[DllImport("http_parser")]
		internal static extern int http_body_is_final(IntPtr parser);
	}
}

