using System;

namespace HttpParser
{
	public class HttpParser : RawHttpParser
	{
		public HttpParser()
			: base()
		{
		}

		public HttpParser(http_parser_type type)
			: base(type)
		{
		}

		private int Start {
			get {
				return (int)Pointer;
			}
		}

		private int Position(IntPtr at)
		{
			return (int)at - Start;
		}

		protected override int OnMessageBegin(IntPtr ptr)
		{
			return OnMessageBegin();
		}
		protected override int OnUrl(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return OnUrl(Data, Position(at), (int)length);
		}
		protected override int OnStatus(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return OnStatus(Data, Position(at), (int)length);
		}
		protected override int OnHeaderField(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return OnHeaderField(Data, Position(at), (int)length);
		}
		protected override int OnHeaderValue(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return OnHeaderValue(Data, Position(at), (int)length);
		}
		protected override int OnHeadersComplete(IntPtr ptr)
		{
			return OnHeadersComplete();
		}
		protected override int OnBody(IntPtr ptr, IntPtr at, IntPtr length)
		{
			return OnBody(Data, Position(at), (int)length);
		}
		protected override int OnMessageComplete(IntPtr ptr)
		{
			return OnMessageComplete();
		}

		protected virtual int OnMessageBegin()
		{
			return 0;
		}
		protected virtual int OnUrl(byte[] data, int start, int count)
		{
			return 0;
		}
		protected virtual int OnStatus(byte[] data, int start, int count)
		{
			return 0;
		}
		protected virtual int OnHeaderField(byte[] data, int start, int count)
		{
			return 0;
		}
		protected virtual int OnHeaderValue(byte[] data, int start, int count)
		{
			return 0;
		}
		protected virtual int OnHeadersComplete()
		{
			return 0;
		}
		protected virtual int OnBody(byte[] data, int start, int count)
		{
			return 0;
		}
		protected virtual int OnMessageComplete()
		{
			return 0;
		}

		public ArraySegment<byte> Segment { get; private set; }

		public byte[] Data {
			get {
				return Segment.Array;
			}
		}

		public void Execute(byte[] data, int offset, int count)
		{
			Execute(new ArraySegment<byte>(data, offset, count));
		}

		unsafe public void Execute(ArraySegment<byte> segment)
		{
			Segment = segment;
			fixed (byte* ptr = segment.Array)
			{
				Execute((IntPtr)ptr, segment.Offset, segment.Count);
			}
		}

		public void Execute(byte[] data, int offset)
		{
			Execute(data, offset, data.Length - offset);
		}

		public void Execute(byte[] data)
		{
			Execute(data, 0, data.Length);
		}

		public void Execute(System.Text.Encoding enc, string str)
		{
			Execute(enc.GetBytes(str));
		}
	}
}

