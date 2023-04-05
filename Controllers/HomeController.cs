using ChooseDelice.Data;
using ChooseDelice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace ChooseDelice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<string> conclusions;
        private static List<Rule> rules;
     
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ChooseDeliceContext deliceContext = new();
            var qdata = deliceContext.Questions.ToList();
            conclusions = deliceContext.Delices.Select(_ => _.Nume).ToList();          

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
            var facts = model.Questions.Where(_ => _.IsChecked).Select(_ => _.Attribute).ToList();

            AnswerModel answer = new()
            {
                Answer = GetResult(facts)
            };

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
                    var name = field.Name.Substring(field.Name.IndexOf("<") + 1, field.Name.IndexOf(">") - 1);
                    if (name != "Id")
                        names.Add(name);
                }
            }
            return names;
        }

        private static string GetResult(List<string> facts)
        {
            bool activated;
            while (true)
            {
                activated = false;
                foreach(var rule in rules)
                {  
                    if (rule.Premises.All(_ => facts.Contains(_)))
                    {
                        activated = true;
                        if (conclusions.Contains(rule.Conclusion))
                            if (facts.All(_ => rule.Premises.Contains(_)))
                                return "Desertul corespunzator cerintelor dumneavoastra este " + rule.Conclusion;
                            else
                                return "Ne cerem scuze, momentan nu avem un desert corespunzator cerintelor dumneavoastra.";
                        else
                        {
                            facts.Add(rule.Conclusion);
                            facts.RemoveAll(_ => rule.Premises.Contains(_));
                        }
                    }
                }
                if(!activated)
                   return "Ne cerem scuze, momentan nu avem o sugestie corespunzatoare cerintelor dumneavoastra.";
            }
        }
    }
}