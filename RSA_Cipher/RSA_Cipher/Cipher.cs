using System;
using System.Linq;
using System.Numerics;

namespace RSA_Cipher
{
    public class Cipher
    {
        private ulong _n;
        private ulong _e;
        private ulong _d;

        public Cipher(ulong n, ulong e, ulong d)
        {
            _n = n;
            _e = e;
            _d = d;
        }

        public double[] Encrypt(int[] data)
        {
            return data.Select(d => Convert.ToDouble(d)).Select(d => Math.Pow(d, _e) % _n).ToArray();
        }

        public string Decrypt(double[] data)
        {
            return string.Concat(data.Select(d => (char)BigInteger.ModPow((int)d, _d, _n)).ToArray());
        }
    }
}
