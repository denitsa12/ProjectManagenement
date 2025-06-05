using _01.ProjectManagement.Models;

namespace _01.ProjectManagement.Services
{
    internal class Program
    {
        static List<Project> projects = new List<Project>();

        static void Main(string[] args)
        {
            SampleData();

            Console.WriteLine("Всички проекти, сортирани по стартова дата:");
            PrintProjectsSortedByStartDate();

            Console.WriteLine();
            Console.WriteLine("Проекти, които приключват след дадена дата:");
            Console.Write("Въведи дата (гггг-мм-дд): ");
            DateTime inputDate = DateTime.Parse(Console.ReadLine());
            PrintProjectsEndingAfterDate(inputDate);

            Console.WriteLine();
            Console.WriteLine("Най-дълъг проект:");
            PrintLongestProject();
        }

        static void PrintProjectsSortedByStartDate()
        {
            var sorted = projects
                .OrderBy(p => p.StartDate)
                .ThenBy(p => p.Name);

            foreach (var p in sorted)
            {
                Console.WriteLine();
                Console.WriteLine($"Проект: {p.Name}, Старт: {p.StartDate:yyyy-MM-dd}, Край: {p.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Лидер: {p.Leader.Name}, Описание: {p.Description}");
                Console.WriteLine("Разработчици:");
                foreach (var dev in p.Developers)
                {
                    Console.WriteLine($"  - {dev.Name}, {dev.Position}");
                }
            }
        }

        static void PrintProjectsEndingAfterDate(DateTime date)
        {
            var filtered = projects
                .Where(p => p.EndDate > date)
                .OrderBy(p => p.Leader.Name)
                .ThenBy(p => p.Name);
                

            if (!filtered.Any())
            {
                Console.WriteLine("Няма проекти, които приключват след зададената дата.");
            }
            else
            {
                foreach (var p in filtered)
                {
                    Console.WriteLine($"{p.Name} (Лидер: {p.Leader.Name}) - Край: {p.EndDate:yyyy-MM-dd}");
                }
            }
        }

        static void PrintLongestProject()
        {
            var longest = projects.OrderByDescending(p => p.DurationInDays).FirstOrDefault();
            if (longest != null)
            {
                Console.WriteLine($"Име: {longest.Name}");
                Console.WriteLine($"Старт: {longest.StartDate:yyyy-MM-dd}, Край: {longest.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Лидер: {longest.Leader.Name}");
                Console.WriteLine($"Разходи на месец: {longest.MonthlyCost:C}");
            }
        }

        static void SampleData()
        {
            Worker worker1 = new Worker { Name = "Петър", Position = "Младши програмист", Salary = 2000 };
            Worker worker2 = new Worker { Name = "Георги", Position = "Старши програмист", Salary = 3000 };
            Worker worker3 = new Worker { Name = "Виктор", Position = "QA", Salary = 4000 };
            Worker worker4 = new Worker { Name = "Мария", Position = "DevOps", Salary = 5000 };
            Worker worker5 = new Worker { Name = "Петя", Position = "Системен администратор", Salary = 6000 };
            Worker worker6 = new Worker { Name = "Виктория", Position = "Старши програмист", Salary = 3000 };
            Worker worker7 = new Worker { Name = "Димитър", Position = "QA", Salary = 4000 };

            projects.Add(new Project
            {
                Name = "Проект А",
                StartDate = new DateTime(2023, 3, 1),
                EndDate = new DateTime(2023, 9, 1),
                Leader = worker2,
                Description = "Модул за управление на данни",
                Developers = new List<Worker> { worker1, worker3, worker4 }
            });

            projects.Add(new Project
            {
                Name = "Проект Б",
                StartDate = new DateTime(2023, 1, 15),
                EndDate = new DateTime(2023, 12, 1),
                Leader = worker6,
                Description = "Финансова система",
                Developers = new List<Worker> { worker5, worker7 }
            });
        }
    }
}
