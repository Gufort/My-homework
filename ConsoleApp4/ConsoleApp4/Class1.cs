using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Formula
    {
        public string Name { get; set; }
        public string Answer { get; set; }
        public string Topic { get; set; }
        public Formula(string Topic, string Name, string Answer)
        {
            this.Topic = Topic;
            this.Name = Name;
            this.Answer = Answer;
        }
    }
    public class SimulatorFormulaTraining
    {
        private Dictionary<string, List<Formula>> Bank = new Dictionary<string, List<Formula>>();
        private Dictionary<string, int> Statistics = new Dictionary<string, int>();
        public void LoadFormulaOnFile(string path = "Files\\Formulas.txt")
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ": " }, StringSplitOptions.None);
                if (parts.Length == 3)
                {
                    var category = parts[0].Trim();
                    var name = parts[1].Trim();
                    var answer = parts[2].Trim();

                    if (!Bank.ContainsKey(category))
                        Bank[category] = new List<Formula>();

                    Bank[category].Add(new Formula(category, name, answer));
                }
            }
        }

        public void Training(string topic)//по заголовку
        {
            if (!Bank.ContainsKey(topic))
            {
                Console.WriteLine("Такой темы нет");
                return;
            }

            var topic_form = Bank[topic];
            foreach (var form in topic_form)
            {
                Console.WriteLine($"Выбранная категория - {topic}");
                Console.WriteLine($"Название формулы - {form.Name}");
                Console.WriteLine("Для продолжения нажмите на любую кнопку");
                Console.ReadKey();
                Console.WriteLine();

                Console.WriteLine($"Сама формула: {form.Answer}");
                Console.WriteLine("Ответили ли вы правильно? (да или нет)");
                string user_answer = "";
                do
                {
                    user_answer = Console.ReadLine();
                    switch (user_answer)
                    {
                        case "да":
                            Console.WriteLine("Прекрасно!");
                            break;
                        case "нет":
                            Console.WriteLine("Ну что ж, теперь постарайтесь запомнить)");
                            if (Statistics.ContainsKey(form.Name))
                                Statistics[form.Name]++;
                            else Statistics.Add(form.Name, 1);
                            break;
                        default:
                            Console.WriteLine("Пожалуйста, напишите 'да' или 'нет'.");
                            break;
                    }
                } while (user_answer != "да" && user_answer != "нет");
            }
            Console.WriteLine("Хотите ли вы потренироваться еще?(да или нет)");
            var user_think = Console.ReadLine();
            do
            {
                user_think = Console.ReadLine();
                switch (user_think)
                {
                    case "да":
                        Console.WriteLine("Отлично! Выберете категорию: Физика, Математика");
                        var topic_2 = Console.ReadLine();
                        Training(topic_2);
                        break;
                    case "нет":
                        Console.WriteLine("Отлично! ");
                        ShowStat();
                        break;
                    default:
                        Console.WriteLine("Пожалуйста, напишите 'да' или 'нет'.");
                        break;
                }
            } while (user_think != "да" && user_think != "нет");
        }

        public void ShowStat()
        {
            Console.WriteLine();
            Console.WriteLine("Ваша статистика: ");
            if (Statistics.Count == 0)
                Console.WriteLine("Вы не разу не ошиблись!");
            else 
            {
                foreach (var stat in Statistics)
                Console.WriteLine($"Формула - {stat.Key}, Количество ошибок - {stat.Value}"); 
            }

        }
    }
    public class Class1
    {
        static void Main()
        {
            Console.WriteLine("Выберете категорию: Физика, Математика");
            var topic = Console.ReadLine();
            var sim = new SimulatorFormulaTraining();
            sim.LoadFormulaOnFile();
            sim.Training(topic);
        }
    }
}
