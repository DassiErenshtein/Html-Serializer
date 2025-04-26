using DTO_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_logic
{
    public class search
    {
        public static HashSet<THtml> searchWithSelector(string selector)
        {
            Selector s = Selector.changeQToObject(selector);
            List<THtml> result = new List<THtml>();
            searchWithSelector(s, MakeTree.tree, result);
            var l=new HashSet<THtml>();
            l = result.ToHashSet();
            return l ;
        }

        private static void searchWithSelector(Selector s, THtml temp, List<THtml> result)
        {
            if (s == null)
            {
                return;
            }
            List<THtml> children = temp.Descendants();
            List<THtml> filter = new List<THtml> ();
            foreach (THtml child in children)
            {
                if ((s.Id==null || child.Id==s.Id )&& 
                    (s.TagName == null || child.name == s.TagName))
                {
                    bool flag = false;
                    foreach(var item in s.Classes)
                    {
                        if(child.Classes.Contains(item)==true)
                            flag = true;
                    }
                    if(flag==true||s.Classes.Count==0)
                    {
                        filter.Add(child);
                        searchWithSelector(s.Child, child, result);
                    }
                }
            }
            if (s.Child == null)
                result.AddRange(filter);
        }
    }
}
