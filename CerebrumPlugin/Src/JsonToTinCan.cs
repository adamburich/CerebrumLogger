using Newtonsoft.Json.Linq;
using TinCan;

namespace CerebrumLogger
{
    public class JsonToTinCan
    {

        /// <summary>
        /// Constructs a TinCan.Statement object given a string of input json.
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>TinCan.Statement object</returns>
        public static Statement toStatement(string jsonData)
        {
            JObject json = JObject.Parse(jsonData);
            return new Statement(json);
        }
    }
}
