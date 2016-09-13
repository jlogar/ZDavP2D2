using NUnit.Framework;

namespace ZDavP2D2.Test
{
    public class HeaderFieldAsserter
    {
        private readonly string[] _headers;
        private readonly int _index;

        private HeaderFieldAsserter(string[] headers, int index)
        {
            _headers = headers;
            _index = index;
        }

        public static HeaderFieldAsserter From(string[] headers)
        {
            return new HeaderFieldAsserter(headers, 0);
        }

        public HeaderFieldAsserter AssertField(string title)
        {
            Assert.AreEqual(title, _headers[_index], "Expected \"" + title + "\" at index " + 0 + ", got \"" + _headers[_index] + "\" instead.");
            return new HeaderFieldAsserter(_headers, _index + 1);
        }
    }
}