namespace GameMain
{
    public class Random
    {
        public static void Initialize(int seed = -1)
        {
            if (seed == -1)
            {
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                return;
            }

            UnityEngine.Random.InitState(seed);
        }

        /// <summary>
        /// Returns a random integer number between min [inclusive] and max [exclusive] (ReadOnly).
        /// </summary>
        public static int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    public class XORRandom
    {
        public XORRandom(ulong seed = 521288629)
        {
            z = seed;
        }
        public int Range(int min, int max)
        {
            ulong randomNumber = xor128();

            int max_ = max >= min ? max : min;
            int min_ = max >= min ? min : max;
            

            int range = max_ - min_;
            int randomNumber32 = (int)randomNumber;
            if (randomNumber32 < 0)
                randomNumber32 *= -1;
            int rangedRandomNumber = randomNumber32 % range;

            return min + rangedRandomNumber;
        }

        private ulong x = 123456789, y = 362436069, z = 521288629, w = 88675123;
        private ulong xor128()
        {
            ulong t;
            t = (x ^ (x << 11)); x = y; y = z; z = w; return (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)));
        }
    }
}
