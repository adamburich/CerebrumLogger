using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebrumLogger
{
    public static class ContextExtensionPresets
    {
        /// <summary>
        /// This function builds an extension object with field list, options, and correct options.
        /// Use this function for building an extension for a Drag & Drop activity start xCall.  
        /// 
        /// Attach the extension with xCall.addExtensionToContext(Extension e)
        /// </summary>
        /// <param name="fieldList"></param>
        /// <param name="optionList"></param>
        /// <param name="fieldOptionDictList"></param>
        /// <returns>The Extension Object to be attached to an xCall.</returns>
        public static TinCan.Extensions FieldListOptionsCorrectOptionsExtension(Object[] fieldList, Object[] optionList, Object[] fieldOptionDictList)
        {
            Dictionary<String, Dictionary<String, Object[]>> ext = new Dictionary<string, Dictionary<string, object[]>>();
            Dictionary<String, Object[]> extensions = new Dictionary<String, Object[]>();
            extensions.Add("http://gamestrax.com/extensions/fields", fieldList);
            extensions.Add("http://gamestrax.com/extensions/options", optionList);
            extensions.Add("http://gamestrax.com/extensions/correct_options", fieldOptionDictList);
            ext.Add("extensions", extensions);
            JObject jobject = JObject.Parse(JsonConvert.SerializeObject(ext));
            return new TinCan.Extensions(jobject);
        }

        /// <summary>
        /// This function builds an extension object with field id only.
        /// Use this function for building an extension for a Drag & Drop selection xCall.  
        /// 
        /// Attach the extension with xCall.addExtensionToContext(Extension e)
        /// </summary>
        /// <param name="field_id"></param>
        /// <returns>The Extension Object to be attached to an xCall.</returns>
        public static TinCan.Extensions FieldIdExtension(string field_id)
        {
            Dictionary<String, Dictionary<string, string>> ext = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<String, String> extensions = new Dictionary<string, string>();
            extensions.Add("http://gamestrax.com/extensions/field", field_id);
            ext.Add("extensions", extensions);
            JObject jobject = JObject.Parse(JsonConvert.SerializeObject(ext));
            return new TinCan.Extensions(jobject);
        }

        /// <summary>
        /// This function builds an extension object given a list of available options and a list of the correct options.
        /// Use this function for building an extension for MCQ Questions and MCQ Questions with submit.  In both cases the extension should be attached only to the corresponding "started" xCall only.
        /// 
        /// Attach the extension with xCall.addExtensionToContext(Extension e)
        /// </summary>
        /// <param name="optionList"></param>
        /// <param name="fieldOptionDictList"></param>
        /// <returns>The Extension Object to be attached to an xCall.</returns>
        public static TinCan.Extensions OptionsCorrectOptionsExtension(Object[] optionList, Object[] fieldOptionDictList)
        {
            Dictionary<String, Dictionary<String, Object[]>> ext = new Dictionary<string, Dictionary<string, object[]>>();
            Dictionary<String, Object[]> extensions = new Dictionary<String, Object[]>();
            extensions.Add("http://gamestrax.com/extensions/options", optionList);
            extensions.Add("http://gamestrax.com/extensions/correct_options", fieldOptionDictList);
            ext.Add("extensions", extensions);
            JObject jobject = JObject.Parse(JsonConvert.SerializeObject(ext));
            return new TinCan.Extensions(jobject);
        }

        /// <summary>
        /// This function builds an extension object given a list of available values and a list of length 1 containing the correct value.
        /// Use this function for attaching an extension to the "started" xCall's context for a Set Value call.
        /// 
        /// Attach the extension with xCall.addExtensionToContext(Extension e)
        /// </summary>
        /// <param name="valueList"></param>
        /// <param name="correctValue"></param>
        /// <returns>The Extension Object to be attached to an xCall.</returns>
        public static TinCan.Extensions SetValueStartedExtension(Object[] valueList, Object[] correctValue)
        {
            Dictionary<String, Dictionary<String, Object[]>> ext = new Dictionary<string, Dictionary<string, object[]>>();
            Dictionary<String, Object[]> extensions = new Dictionary<String, Object[]>();
            extensions.Add("http://gamestrax.com/extensions/available_values", valueList);
            extensions.Add("http://gamestrax.com/extensions/correct_value", correctValue);
            ext.Add("extensions", extensions);
            JObject jobject = JObject.Parse(JsonConvert.SerializeObject(ext));
            return new TinCan.Extensions(jobject);
        }
    }
}
