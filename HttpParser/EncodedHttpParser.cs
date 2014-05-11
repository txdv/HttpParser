using System;
using System.Text;

namespace HttpParser
{
	public class EncodedHttpParser : HttpParser
	{
		public Encoding Encoding { get; protected set; }

		public EncodedHttpParser()
			: this(Encoding.Default)
		{
		}

		public EncodedHttpParser(Encoding enc)
			: this(http_parser_type.HTTP_BOTH, enc)
		{
		}

		public EncodedHttpParser(http_parser_type type)
			: this(type, Encoding.Default)
		{
		}

		public EncodedHttpParser(http_parser_type type, Encoding enc)
			: base(type)
		{
			Encoding = enc;
		}

		public Action<string, string> OnHeaderElementEvent;


		void YieldHeaderElement()
		{
			if (OnHeaderElementEvent != null) {
				OnHeaderElementEvent(field, value);
			}

			// maybe the gc wants to collects those strings someday, but
			// won't since the HttpParser is not freed
			field = string.Empty;
			value = string.Empty;
		}

		bool fieldParsed = false;

		string field = string.Empty;
		protected override int OnHeaderField(byte[] data, int start, int count)
		{
			if (fieldParsed) {
				YieldHeaderElement();
				fieldParsed = false;
			}

			field += Encoding.GetString(data, start, count);

			return base.OnHeaderField(data, start, count);
		}

		string value = string.Empty;
		protected override int OnHeaderValue(byte[] data, int start, int count)
		{
			fieldParsed = true;

			value += Encoding.GetString(data, start, count);

			return base.OnHeaderValue(data, start, count);
		}

		protected override int OnHeadersComplete()
		{
			YieldHeaderElement();

			return base.OnHeadersComplete();
		}

		public void Execute(string str)
		{
			Execute(Encoding == null ? Encoding.Default : Encoding, str);
		}
	}
}

