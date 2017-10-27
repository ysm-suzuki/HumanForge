namespace GameMain
{
    public class UInt4x2
    {
        private byte _byte;

        public UInt4x2(int number1, int number2)
        {
            if (number1 < 0)
                number1 = 0;
            if (number2 < 0)
                number2 = 0;
            
            UnityEngine.Debug.Assert(number1 <= 0x0f, "number1 is too large.");
            UnityEngine.Debug.Assert(number2 <= 0x0f, "number2 is too large.");

            int int8number1 = System.BitConverter.GetBytes(number1)[0] & (0x0f);
            int int8number2 = System.BitConverter.GetBytes(number2)[0] & (0x0f);

            _byte = System.BitConverter.GetBytes(
                int8number1 | (int8number2 << 4)
                )[0];
        }
        public UInt4x2(byte data)
        {
            _byte = data;
        }

        public int first
        {
            get
            {
                return _byte & (0x0f);
            }
        }

        public int second
        {
            get
            {
                return (_byte & (0xf0)) >> 4;
            }
        }


        public byte ToByte()
        {
            return _byte;
        }
    }
}
