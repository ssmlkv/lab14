using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using lab12__2;

namespace lab14.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GenerateStationData_ShouldReturnNonEmptyDictionary()
        {
            var stationData = Program.GenerateStationData();
            Assert.IsNotNull(stationData);
            Assert.IsTrue(stationData.Count > 0);
        }

        [TestMethod]
        public void ShowAllTrains_ShouldOutputAllTrains()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.ShowAllTrains(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Станция1") && result.Contains("Станция2"));
            }
        }

        [TestMethod]
        public void PerformWhereQueries_ShouldReturnFastTrains()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                Program.PerformWhereQueries(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Скоростные поезда (LINQ):") && result.Contains("Скоростные поезда (методы):"));
            }
        }

        [TestMethod]
        public void PerformSetOperations_ShouldHandleUnionExceptIntersect()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.PerformSetOperations(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Объединение (LINQ):") && result.Contains("Объединение (методы):"));
                Assert.IsTrue(result.Contains("Разность (LINQ):") && result.Contains("Разность (методы):"));
                Assert.IsTrue(result.Contains("Пересечение (LINQ):") && result.Contains("Пересечение (методы):"));
            }
        }

        [TestMethod]
        public void PerformAggregation_ShouldReturnCorrectAggregates()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.PerformAggregation(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Общая скорость (LINQ):") && result.Contains("Максимальная скорость (LINQ):"));
                Assert.IsTrue(result.Contains("Минимальная скорость (LINQ):") && result.Contains("Средняя скорость (LINQ):"));
                Assert.IsTrue(result.Contains("Общая скорость (методы):") && result.Contains("Максимальная скорость (методы):"));
                Assert.IsTrue(result.Contains("Минимальная скорость (методы):") && result.Contains("Средняя скорость (методы):"));
            }
        }

        [TestMethod]
        public void PerformGrouping_ShouldGroupBySpeedCorrectly()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.PerformGrouping(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Группировка по скорости > 100 (LINQ):") && result.Contains("Количество:"));
                Assert.IsTrue(result.Contains("Группировка по скорости > 100 (методы):") && result.Contains("Количество:"));
            }
        }

        [TestMethod]
        public void PerformLetOperation_ShouldReturnNewTypeCorrectly()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.PerformLetOperation(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Новый тип (LINQ):") && result.Contains("Описание:"));
                Assert.IsTrue(result.Contains("Новый тип (методы):") && result.Contains("Описание:"));
            }
        }

        [TestMethod]
        public void PerformJoinOperation_ShouldJoinCorrectly()
        {
            var station = Program.GenerateStationData();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.PerformJoinOperation(station);
                var result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("Соединенные вагоны (LINQ):") && result.Contains("ID:"));
                Assert.IsTrue(result.Contains("Соединенные вагоны (методы):") && result.Contains("ID:"));
            }
        }

        [TestMethod]
        public void RandomInit_ShouldReturnValidCarriage()
        {
            var carriage = Program.RandomInit();
            Assert.IsNotNull(carriage);
            Assert.IsTrue(carriage.MaxSpeed > 0);
            Assert.IsTrue(carriage.Number > 0);
            Assert.IsTrue(carriage.id.Id > 0);
        }
    }
}
