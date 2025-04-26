using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace DTO_classes
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _helper = new HtmlHelper();

        public static HtmlHelper helper => _helper;

        public List<string> Tags;

        public List<string> withoutCloseTags;

        private HtmlHelper()
        {
            Tags = JsonConvert.DeserializeObject<List<string>>( File.ReadAllText("html-tags.json"));
            withoutCloseTags = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("html-tags-void.json"));
        }
    }
}
