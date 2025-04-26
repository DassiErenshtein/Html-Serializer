using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO_classes;
namespace BLL_logic
{
    public class MakeTree
    {
        
       public static THtml tree=new THtml();
       public MakeTree(String html)
       {
            List<string> elementsOfHtml = HtmlHelper.helper.Tags;
            List<string> elementAlone = HtmlHelper.helper.withoutCloseTags;
            var cleanHtml = new Regex("[\\n\\r\\t]").Replace(html, "");
            //var cleanHtml1 = new Regex("([\\s]?){1}").Replace(cleanHtml,"");
            //var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where((s) => {
            //בהתחלה עשיתי ביטוי שמוציא את הסוגריים של האלמנט, אבל אח"כ הבנתי שזו הדרך היחידה שלי להבין שזה קומפוננטה, ולכן בצעתי שאילתות תואמות...
            var htmlLines = new Regex("(<.*?>)").Split(cleanHtml).Where((s) => {

                var a = Regex.Match(s, "<(.*?)>");
                return a.Length > 0 && s.Replace(" ", "").Length > 0;
            });
            List<string> htmlTags = htmlLines.ToList();
            var root = new THtml();
            int i = 0;
            while (htmlTags[i].StartsWith("<!"))
                i++;
            if (htmlTags[i].IndexOf(" ") != -1)
                root.name = htmlTags[i].Substring(1, htmlTags[i].IndexOf(" "));
            else
                root.name = Regex.Match(htmlTags[i], "<(.*?)>").Groups[1].Value;
            root.Parent = null;
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlTags[i]);
            root.addAtributes(attributes);
            THtml thisElem = root;
            i++;
            for (; i < htmlTags.Count() && !htmlTags[i].Equals("/html"); i++)
            {
                if (!htmlTags[i].StartsWith("<!"))
                {
                    string firstWord = Regex.Match(htmlTags[i], "<(.*?)>").Groups[1].Value;
                    if (htmlTags[i].StartsWith("<"))
                    {

                        if (firstWord.IndexOf(" ") != -1 && firstWord.IndexOf(" ") != 0)
                            firstWord = firstWord.Substring(0, firstWord.IndexOf(" "));
                        if (firstWord[0] == '/' && !thisElem.name.Equals("html") && elementsOfHtml.Contains(firstWord.Substring(1)))
                            thisElem = thisElem.Parent;

                        else if (elementAlone.Contains(firstWord) || htmlTags[i][htmlTags[i].Length - 1] == '/')
                        {
                            THtml temp = new THtml();
                            thisElem.Childern.Add(temp);
                            temp.name = firstWord;
                            temp.Parent = thisElem;
                            attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlTags[i]);
                            temp.addAtributes(attributes);
                        }
                        else if (elementsOfHtml.Contains(firstWord))
                        {
                            THtml temp = new THtml();
                            thisElem.Childern.Add(temp);
                            temp.name = firstWord;
                            temp.Parent = thisElem;
                            attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlTags[i]);
                            temp.addAtributes(attributes);
                            thisElem = temp;
                        }


                    }
                    else
                    {
                        if (htmlTags[i] is string)
                            thisElem.InnerHtml += htmlTags[i];
                    }
                }
            }
            tree= root;
        }
        
    }
}
