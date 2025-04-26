using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_classes
{
    public class Selector
    {
        public string TagName;
        public string Id;
        public List<string> Classes=new List<string>();
        public Selector Parent;
        public Selector Child;
        public static Selector changeQToObject(string query)
        {
            string[] querys = query.Split(' ');
            Selector root = new Selector();
            Selector temp = root;
            for(int i=0;i<querys.Length;i++)
            {
                string[] classes=querys[i].Split(".");
                if (classes.Length > 1)
                    for (int j = 0; j < classes.Length; j++)
                    {                        
                        if (classes[j].Contains("#") == true)
                        {
                            string id = classes[j].Substring(classes[j].IndexOf("#") + 1);                            
                            string className = classes[j].Substring(0, classes[j].IndexOf("#"));
                            temp.Id = id;
                            if (j == 0)
                            {
                                if (className != "")
                                {
                                    if(HtmlHelper.helper.Tags.Contains(className))
                                        temp.TagName = className;
                                }
                            }
                            else
                            {
                                temp.Classes.Add(className);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                if (classes[j] != "")
                                {
                                    if (HtmlHelper.helper.Tags.Contains(classes[j]))
                                        temp.TagName = classes[j];
                                }
                            }
                            else
                                temp.Classes.Add(classes[j]);
                        }

                    }
                else if (querys[i].Contains("#") == true) 
                {
                    string[] ids = querys[i].Split("#");
                    if(ids.Length > 1)
                    {
                        temp.Id= ids[1];
                        if (ids[0]!="")
                        {
                            if (HtmlHelper.helper.Tags.Contains(ids[0]))
                                temp.TagName = ids[0];
                        }
                    }
                    else
                    {                        
                        temp.Id = ids[0];
                    }
                }
                else
                {
                    if (HtmlHelper.helper.Tags.Contains(querys[i]))
                        temp.TagName = querys[i];
                }
                temp.Child = new Selector();
                temp.Child.Parent = temp;
                temp = temp.Child;
            }
            temp.Parent.Child = null;
            return root;            
        }        
    }   
}

