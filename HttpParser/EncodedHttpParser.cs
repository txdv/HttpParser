using System;
using System.Text;

namespace HttpParser
{
	public class EncodedHttpParser : EventedHttpParser
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

		string field = null;
		protected override int OnHeaderField(byte[] data, int start, int count)
		{
			field = Encoding.GetString(data, start, count);

			return base.OnHeaderField(data, start, count);
		}

		protected override int OnHeaderValue(byte[] data, int start, int count)
		{
			if (OnHeaderElementEvent != null) {
				OnHeaderElementEvent(field, Encoding.GetString(data, start, count));
			}

			return base.OnHeaderValue(data, start, count);
		}

		public void Execute(string str)
		{
			Execute(Encoding == null ? Encoding.Default : Encoding, str);
		}
	}
}

