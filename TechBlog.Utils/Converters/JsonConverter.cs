using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TechBlog.Utils.Converters
{
    public static class JsonConverter
    {
        public static string ToJson(this object source)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(source);
        }

        public static T JsonTo<T>(this string source)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            return ser.Deserialize<T>(source);
        }
    }
}
