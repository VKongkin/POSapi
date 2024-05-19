namespace POSapi.Model.Service
{
    public class IDGeneratorService
    {
        public static string IDGenerator(string prefix, int id, string format)
        {
            try
            {
                string rv = prefix + (id + 1).ToString(format);
                return rv;
            }
            catch (Exception) { return null; }
        }
    }
}
