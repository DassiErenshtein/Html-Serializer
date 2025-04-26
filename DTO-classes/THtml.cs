using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO_classes
{
    public class THtml
    {
        ////my mishtane/////
        //public string tagName { get; set; } = null;
        ////////////////////
        public string Id {  get; set; } = null;
        public string name {  get; set; } = null;
        public Dictionary<string, string> Attributes1 = new Dictionary<string, string>();
        //public List<string> Attributes { get; set; } 
        public List<string> Classes { get; set; } 
        public string InnerHtml {  get; set; } = null;
        public THtml Parent { get; set; } = null;
        public List<THtml> Childern { get; set; } 
        public THtml() {
           // Attributes = new List<string>();
            Classes = new List<string>();
            Childern= new List<THtml>();
            InnerHtml = "";
        }
        public void addAtributes(MatchCollection a)
        {
            foreach(var item1 in a)
            {
                string item = item1.ToString();
                item=item.Replace("\\", "");
                item = item.Replace("\"", "");
                string[] keyValue = item.Split("=");
               // Attributes.Add(item);
                Attributes1.Add(keyValue[0], keyValue[1]);
                string nameA = keyValue[0]; 
                    //item.Substring(0, item.IndexOf("="));
                if (nameA.Equals("id"))
                    Id = item.Substring(item.IndexOf("=") + 1);
                else if (nameA.Equals("class"))
                    Classes = item.Substring(item.IndexOf("=") + 1).Split(" ").ToList();

            }
        }
        public override string ToString()
        {
            string str = Id + " " + name + " attributes:";
            for (int i = 0; i < Attributes1.Count(); i++)
                str += Attributes1.Keys.ElementAt(i)+": "+ Attributes1.Values.ElementAt(i);
            str += " classes:";
            for (int i = 0; i < Classes.Count(); i++)
                str += Classes[i];
            str+=" " + InnerHtml + " ";
            return str;
        }
        public List<THtml> Descendants()
        {
            Queue<THtml> childrens = new Queue<THtml>();
            List<THtml> childrensHtml = ChildrenDesc(childrens).ToList();
            childrensHtml.Remove(this);
            return childrensHtml;
        }
        public IEnumerable<THtml> ChildrenDesc(Queue<THtml> childrens)
        {
            childrens.Enqueue(this);
            while (childrens.Count > 0)
            {
                THtml temp = childrens.Dequeue();
                List<THtml> tempChildren=new List<THtml> ();
                int i = 0;
                while(tempChildren.Count !=temp.Childern.Count) 
                {
                    tempChildren.Add(temp.Childern[i++]);
                }
                while (tempChildren.Count > 0)
                {
                    THtml child = tempChildren[0];
                    IEnumerable<THtml> t = child.ChildrenDesc(childrens);
                    foreach (var item in t)
                    {
                        yield return item;
                    }
                    tempChildren.Remove(child);
                }
                yield return temp;
            }
        }
        public List<THtml> Ancestors()
        {
            THtml temp = this.Parent;
            List<THtml> result = new List<THtml>();
            while (temp != null)
            {
                result.Add(temp);
                temp = temp.Parent;
            }
            return result;
        }

    }
}
