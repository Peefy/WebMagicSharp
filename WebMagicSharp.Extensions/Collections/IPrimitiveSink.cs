using System.Text;

namespace WebMagicSharp.Collections
{
    public interface IPrimitiveSink
    {
        IPrimitiveSink PutByte(byte b);
        IPrimitiveSink PutBytes(byte[] bytes);
        IPrimitiveSink PutBytes(byte[] bytes, int off, int len);
        IPrimitiveSink PutShort(short s);
        IPrimitiveSink PutInt(int i);
        IPrimitiveSink PutLong(long l);
        IPrimitiveSink PutFloat(float f);
        IPrimitiveSink PutDouble(double d);
        IPrimitiveSink PutBoolean(bool b);
        IPrimitiveSink PutChar(char c);
        IPrimitiveSink PutString(string str);
        IPrimitiveSink PutString(string str, Encoding encoding);
        IPrimitiveSink PutUnencodeedChars(string str);
        
    }

}
