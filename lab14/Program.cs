using lab10;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab14
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Stack<Carriage>> station = GenerateStationData();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Показать все поезда");
                Console.WriteLine("2. Выборка данных (Where)");
                Console.WriteLine("3. Операции над множествами (Union, Except, Intersect)");
                Console.WriteLine("4. Агрегирование данных (Sum, Max, Min, Average)");
                Console.WriteLine("5. Группировка данных (Group by)");
                Console.WriteLine("6. Получение нового типа (Let)");
                Console.WriteLine("7. Соединение (Join)");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAllTrains(station);
                        break;
                    case "2":
                        PerformWhereQueries(station);
                        break;
                    case "3":
                        PerformSetOperations(station);
                        break;
                    case "4":
                        PerformAggregation(station);
                        break;
                    case "5":
                        PerformGrouping(station);
                        break;
                    case "6":
                        PerformLetOperation(station);
                        break;
                    case "7":
                        PerformJoinOperation(station);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                        break;
                }
            }
        }

        public static Dictionary<string, Stack<Carriage>> GenerateStationData()
        {
            Dictionary<string, Stack<Carriage>> station = new Dictionary<string, Stack<Carriage>>();

            for (int i = 1; i <= 2; i++)
            {
                Stack<Carriage> train = new Stack<Carriage>();
                for (int j = 1; j <= 3; j++)
                {
                    var carriage = RandomInit();
                    train.Push(carriage);
                }
                station.Add($"Станция{i}", train);
            }
            return station;
        }

        public static Carriage RandomInit()
        {
            Random rand = new Random();
            int type = rand.Next(1, 4);

            switch (type)
            {
                case 1:
                    var freightWagon = new FreightWagon();
                    freightWagon.RandomInit();
                    return freightWagon;
                case 2:
                    var passengerWagon = new PassengerWagon();
                    passengerWagon.RandomInit();
                    return passengerWagon;
                case 3:
                    var diningWagon = new DiningWagon();
                    diningWagon.RandomInit();
                    return diningWagon;
                default:
                    return null; // Этот случай никогда не должен происходить
            }
        }

        public static void ShowAllTrains(Dictionary<string, Stack<Carriage>> station)
        {
            foreach (var s in station)
            {
                Console.WriteLine($"\n{s.Key}:");
                foreach (var train in s.Value)
                {
                    Console.WriteLine(train);
                }
            }
        }

        public static void PerformWhereQueries(Dictionary<string, Stack<Carriage>> station)
        {
            // LINQ
            var fastTrainsLinq = from s in station
                                 from t in s.Value
                                 where t.MaxSpeed > 100
                                 select t;

            Console.WriteLine("\nСкоростные поезда (LINQ):");
            foreach (var train in fastTrainsLinq)
            {
                Console.WriteLine(train);
            }

            // Methods
            var fastTrainsMethods = station.SelectMany(s => s.Value).Where(t => t.MaxSpeed > 100);

            Console.WriteLine("\nСкоростные поезда (методы):");
            foreach (var train in fastTrainsMethods)
            {
                Console.WriteLine(train);
            }
        }

        public static void PerformSetOperations(Dictionary<string, Stack<Carriage>> station)
        {
            Stack<Carriage> train1 = station["Станция1"];
            Stack<Carriage> train3 = new Stack<Carriage>();
            train3.Push(new FreightWagon(new IdNumber(7), 107, 95, "Алмаз", 20));
            train3.Push(new PassengerWagon(new IdNumber(2), 102, 120, 50, 100)); // Совпадает с train1
            train3.Push(new DiningWagon(new IdNumber(8), 108, 105, "10:00-20:00", 15, 40));

            // Union
            var unionLinq = (from t in train1 select t).Union(from t in train3 select t);
            var unionMethods = train1.Union(train3);

            Console.WriteLine("\nОбъединение (LINQ):");
            foreach (var train in unionLinq)
            {
                Console.WriteLine(train);
            }

            Console.WriteLine("\nОбъединение (методы):");
            foreach (var train in unionMethods)
            {
                Console.WriteLine(train);
            }

            // Except
            var exceptLinq = (from t in train1 select t).Except(from t in train3 select t);
            var exceptMethods = train1.Except(train3);

            Console.WriteLine("\nРазность (LINQ):");
            foreach (var train in exceptLinq)
            {
                Console.WriteLine(train);
            }

            Console.WriteLine("\nРазность (методы):");
            foreach (var train in exceptMethods)
            {
                Console.WriteLine(train);
            }

            // Intersect
            var intersectLinq = (from t in train1 select t).Intersect(from t in train3 select t);
            var intersectMethods = train1.Intersect(train3);

            Console.WriteLine("\nПересечение (LINQ):");
            foreach (var train in intersectLinq)
            {
                Console.WriteLine(train);
            }

            Console.WriteLine("\nПересечение (методы):");
            foreach (var train in intersectMethods)
            {
                Console.WriteLine(train);
            }
        }

        public static void PerformAggregation(Dictionary<string, Stack<Carriage>> station)
        {
            // LINQ
            var totalSpeedLinq = (from s in station from t in s.Value select t.MaxSpeed).Sum();
            var maxSpeedLinq = (from s in station from t in s.Value select t.MaxSpeed).Max();
            var minSpeedLinq = (from s in station from t in s.Value select t.MaxSpeed).Min();
            var averageSpeedLinq = (from s in station from t in s.Value select t.MaxSpeed).Average();

            Console.WriteLine($"\nОбщая скорость (LINQ): {totalSpeedLinq}, Максимальная скорость (LINQ): {maxSpeedLinq}, Минимальная скорость (LINQ): {minSpeedLinq}, Средняя скорость (LINQ): {averageSpeedLinq}");

            // Methods
            var totalSpeedMethods = station.SelectMany(s => s.Value).Sum(t => t.MaxSpeed);
            var maxSpeedMethods = station.SelectMany(s => s.Value).Max(t => t.MaxSpeed);
            var minSpeedMethods = station.SelectMany(s => s.Value).Min(t => t.MaxSpeed);
            var averageSpeedMethods = station.SelectMany(s => s.Value).Average(t => t.MaxSpeed);

            Console.WriteLine($"Общая скорость (методы): {totalSpeedMethods}, Максимальная скорость (методы): {maxSpeedMethods}, Минимальная скорость (методы): {minSpeedMethods}, Средняя скорость (методы): {averageSpeedMethods}");
        }

        public static void PerformGrouping(Dictionary<string, Stack<Carriage>> station)
        {
            // LINQ
            var groupedBySpeedLinq = from s in station
                                     from t in s.Value
                                     group t by t.MaxSpeed > 100 into g
                                     select new { Key = g.Key, Count = g.Count(), Items = g };

            Console.WriteLine("\nГруппировка по скорости > 100 (LINQ):");
            foreach (var group in groupedBySpeedLinq)
            {
                Console.WriteLine(group.Key ? "Скоростные поезда:" : "Медленные поезда:");
                Console.WriteLine($"Количество: {group.Count}");
                foreach (var train in group.Items)
                {
                    Console.WriteLine(train);
                }
            }

            // Methods
            var groupedBySpeedMethods = station.SelectMany(s => s.Value)
                                               .GroupBy(t => t.MaxSpeed > 100)
                                               .Select(g => new { Key = g.Key, Count = g.Count(), Items = g });

            Console.WriteLine("\nГруппировка по скорости > 100 (методы):");
            foreach (var group in groupedBySpeedMethods)
            {
                Console.WriteLine(group.Key ? "Скоростные поезда:" : "Медленные поезда:");
                Console.WriteLine($"Количество: {group.Count}");
                foreach (var train in group.Items)
                {
                    Console.WriteLine(train);
                }
            }
        }

        public static void PerformLetOperation(Dictionary<string, Stack<Carriage>> station)
        {
            // LINQ
            var newTypeLinq = from s in station
                              from t in s.Value
                              let description = t is PassengerWagon ? "Пассажирский вагон" : t is FreightWagon ? "Грузовой вагон" : "Вагон-ресторан"
                              select new { t.id, t.MaxSpeed, Description = description };

            Console.WriteLine("\nНовый тип (LINQ):");
            foreach (var item in newTypeLinq)
            {
                Console.WriteLine($"ID: {item.id.Id}, Скорость: {item.MaxSpeed}, Описание: {item.Description}");
            }

            // Methods
            var newTypeMethods = station.SelectMany(s => s.Value)
                                        .Select(t => new { t.id, t.MaxSpeed, Description = t is PassengerWagon ? "Пассажирский вагон" : t is FreightWagon ? "Грузовой вагон" : "Вагон-ресторан" });

            Console.WriteLine("\nНовый тип (методы):");
            foreach (var item in newTypeMethods)
            {
                Console.WriteLine($"ID: {item.id.Id}, Скорость: {item.MaxSpeed}, Описание: {item.Description}");
            }
        }

        public static void PerformJoinOperation(Dictionary<string, Stack<Carriage>> station)
        {
            // Создаем дополнительную коллекцию для соединения
            List<int> wagonIds = new List<int> { 1, 4, 7 };

            // LINQ
            var joinLinq = from s in station
                           from t in s.Value
                           join id in wagonIds on t.id.Id equals id
                           select t;

            Console.WriteLine("\nСоединенные вагоны (LINQ):");
            foreach (var wagon in joinLinq)
            {
                Console.WriteLine(wagon);
            }

            // Methods
            var joinMethods = station.SelectMany(s => s.Value).Join(wagonIds, t => t.id.Id, id => id, (t, id) => t);

            Console.WriteLine("\nСоединенные вагоны (методы):");
            foreach (var wagon in joinMethods)
            {
                Console.WriteLine(wagon);
            }
        }
    }
}