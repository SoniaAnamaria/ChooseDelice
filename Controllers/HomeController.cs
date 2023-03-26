using ChooseDelice.Data;
using ChooseDelice.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using System.Diagnostics;
using System.Reflection;

namespace ChooseDelice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<string> conclusions;
        private List<string> partialConclusions;
        private List<string> facts;
        private List<Rule> rules;
     
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ChooseDeliceContext deliceContext = new();
            var qdata = deliceContext.Questions.ToList();
            conclusions = deliceContext.Delices.Select(_ => _.Nume).ToList();
            partialConclusions = deliceContext.PartialDecisions.Select(_ => _.Conclusion).Distinct().ToList();            

            rules = deliceContext.Delices.ToList().ConvertAll(_ => new Rule { Premises = GetPremises(_), Conclusion = _.Nume });
            rules.AddRange(deliceContext.PartialDecisions.ToList().ConvertAll(_ => new Rule { Premises = GetPremises(_), Conclusion = _.Conclusion }));

            MainModel model = new()
            {
                Questions = qdata
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Submit(MainModel model)
        {
            facts = model.Questions.Where(_ => _.IsChecked).Select(_ => RemoveUnderscore(_.Attribute)).ToList();

            AnswerModel answer = new AnswerModel();
            answer.Answer = "Nu exista";

            return View(answer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static List<string> GetPremises(Object obj)
        {
            List<string> names = new();
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance |
                                                          BindingFlags.Static |
                                                          BindingFlags.NonPublic |
                                                          BindingFlags.Public);
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(int) && Convert.ToInt32(field.GetValue(obj)) == 1)
                {
                    var name = field.Name.Substring(field.Name.IndexOf("<") + 1, field.Name.IndexOf(">") - 1).ToLower();
                    if (name != "id")
                        names.Add(name);
                }
            }
            return names;
        }

        private static string RemoveUnderscore(string s)
        {
            var index = s.IndexOf("_");
            if (index > 0)
                return s.Remove(index, 1);
            return s;
        }
    }
}