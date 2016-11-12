using System.Runtime.Serialization.Formatters.Binary;

namespace FileSystem
{
    class IOFormatter
    {
        private static BinaryFormatter formatter = null;
        public static BinaryFormatter getInstance()
        {
            if(null == formatter)
            {
                formatter = new BinaryFormatter();
            }
            return formatter;
        }
    }
}
