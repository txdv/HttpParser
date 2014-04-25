using System;

namespace HttpParser
{
	public class EventedHttpParser : HttpParser
	{
		public EventedHttpParser()
			: base()
		{
		}

		public EventedHttpParser(http_parser_type type)
			: base(type)
		{
		}

		public event Action OnMessageBeginEvent;
		protected override int OnMessageBegin()
		{
			if (OnMessageBeginEvent != null) {
				OnMessageBeginEvent();
			}
			return base.OnMessageBegin();
		}

		public event Action<byte[], int, int> OnUrlEvent;
		protected override int OnUrl(byte[] data, int start, int count)
		{
			if (OnUrlEvent != null) {
				OnUrlEvent(data, start, count);
			}
			return base.OnUrl (data, start, count);
		}

		public event Action<byte[], int, int> OnHeaderFieldEvent;
		protected override int OnHeaderField(byte[] data, int start, int count)
		{
			if (OnHeaderFieldEvent != null) {
				OnHeaderFieldEvent(data, start, count);
			}

			return base.OnHeaderField(data, start, count);
		}

		public event Action<byte[], int, int> OnHeaderValueEvent;
		protected override int OnHeaderValue(byte[] data, int start, int count)
		{
			if (OnHeaderValueEvent != null) {
				OnHeaderValueEvent(data, start, count);
			}

			return base.OnHeaderValue(data, start, count);
		}

		public event Action OnHeadersCompleteEvent;
		protected override int OnHeadersComplete()
		{
			if (OnHeadersCompleteEvent != null) {
				OnHeadersCompleteEvent();
			}

			return base.OnHeadersComplete ();
		}

		public event Action<byte[], int, int> OnBodyEvent;
		protected override int OnBody(byte[] data, int start, int count)
		{
			if (OnBodyEvent != null) {
				OnBodyEvent(data, start, count);
			}
			return base.OnBody(data, start, count);
		}

		public event Action OnMessageCompleteEvent;
		protected override int OnMessageComplete()
		{
			if (OnMessageCompleteEvent != null) {
				OnMessageCompleteEvent();
			}

			return base.OnMessageComplete();
		}
	}
}

