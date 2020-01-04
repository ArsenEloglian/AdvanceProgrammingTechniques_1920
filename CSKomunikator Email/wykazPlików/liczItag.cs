namespace wykazPlików
{
    public partial class Program  {
        public static uint liczItag(byte[] całośćBajtów)
        {
            uint całość = uint.MaxValue;
            foreach (byte bajt in całośćBajtów) całość += bajt;
            return całość;
        }
    }
}
