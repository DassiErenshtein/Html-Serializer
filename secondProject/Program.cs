using System.Diagnostics.Tracing;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using BLL_logic;
using DTO_classes;
var html = await Load(@"https://learn.malkabruk.co.il/practicode/assignment/");
new MakeTree(html);
THtml tree = MakeTree.tree;
//Selector treeRoot=Selector.changeQToObject("div#a.c div #a");
var b = tree.Childern[0];
var a=search.searchWithSelector("div label");
Console.ReadLine();
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();    
    return html;
}
