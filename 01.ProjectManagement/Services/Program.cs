using _01.ProjectManagement.Models;

namespace _01.ProjectManagement.Services
{
    internal class Program
    {
        static List<Project> projects = new List<Project>();

        static void Main(string[] args)
        {
            //SeedData();
            Console.WriteLine("Създаване на проект:");
            AddProject();
            int selection = 0;
            while (true)
            {
                Console.WriteLine("1. Създаване на проект:");
                Console.WriteLine("2. Покажи информация за всички проекти");
                Console.WriteLine("Избери: ");
                selection = int.Parse(Console.ReadLine());

                if (selection == 1)
                {
                    AddProject();
                }
                else if(selection == 2)
                {
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
                    break;
                }        
            }


            /*Console.WriteLine("Всички проекти, сортирани по стартова дата:");
            PrintProjectsSortedByStartDate();

            Console.WriteLine();
            Console.WriteLine("Проекти, които приключват след дадена дата:");
            Console.Write("Въведи дата (гггг-мм-дд): ");
            DateTime inputDate = DateTime.Parse(Console.ReadLine());
            PrintProjectsEndingAfterDate(inputDate);

            Console.WriteLine();
            Console.WriteLine("Най-дълъг проект:");
            PrintLongestProject();*/
        }

        static void AddProject()
        {
            Console.Write("Име на проекта: ");
            string name = Console.ReadLine();

            Console.Write("Стартова дата (гггг-мм-дд): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Крайна дата (гггг-мм-дд): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(" Въведи данни за Лидера:");
            Worker leader = ReadWorker();

            Console.Write("Описание на проекта: ");
            string description = Console.ReadLine();

            List<Worker> developers = AddWorkers();

            projects.Add(new Project
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Leader = leader,
                Description = description,
                Developers = developers
            });

            Console.WriteLine("Проектът е добавен успешно!");
        }

        static List<Worker> AddWorkers()
        {
            Console.WriteLine("1. Добавяне на работник");
            Console.WriteLine("2. Да не се добавя работник");

            List<Worker> workers = new List<Worker>();

            int selectedOption = 0;

            while(true) 
            {
                Console.WriteLine("Изберете опция:");
                selectedOption = int.Parse(Console.ReadLine());
                if(selectedOption == 1) 
                {
                    if(workers.Count < 5)
                    {
                        workers.Add(ReadWorker());
                    }
                    else
                    {
                        Console.WriteLine("Максималният брой работници е 5. Не могат да се добавят повече.");
                        break;
                    }
                }
                else if(selectedOption == 2)
                {
                    break;
                }
            }
            return workers;
        }


        static Worker ReadWorker()
        {
            Console.Write("Име: ");
            string name = Console.ReadLine();

            Console.Write("Заплата: ");
            int salary = int.Parse(Console.ReadLine());

            Console.Write("Позиция: ");
            string position = Console.ReadLine();

            return new Worker
            {
                Name = name,
                Salary = salary,
                Position = position
            };
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

        static int CalculateProjectDuration(Project p)
        {
            return (p.EndDate - p.StartDate).Days;
        }

        static decimal CalculateMonthlyCost(Project p)
        {
            decimal total = p.Developers.Sum(w => w.Salary);
            total += p.Leader.Salary;
            return total;
        }

        static void PrintLongestProject()
        {
            var longest = projects.OrderByDescending(p => CalculateProjectDuration(p)).FirstOrDefault();
            if (longest != null)
            {
                Console.WriteLine($"Име: {longest.Name}");
                Console.WriteLine($"Старт: {longest.StartDate:yyyy-MM-dd}, Край: {longest.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Лидер: {longest.Leader.Name}");
                Console.WriteLine($"Разходи на месец: {CalculateMonthlyCost(longest):C}");
            }
        }

        static void SeedData()
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
