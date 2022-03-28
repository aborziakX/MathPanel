using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Test1
{
    class Program
    {
        /// <summary>
        /// Начальные условия
        /// </summary>
        [Serializable]
        public class ModelConfiguration
        {


            /// <summary>
            /// Конфигурация генерации
            /// </summary>
            public Generator[] Generators { get; set; }

            /// <summary>
            /// Конфигурация запасов
            /// </summary>
            public FuelInfo[] FuelTotalReserves { get; set; }

            /// <summary>
            /// Уровень потребления
            /// TODO: преобразовать в зависимость от времени
            /// </summary>
            public double DemandLevel { get; set; }

            /// <summary>
            /// Показывать все варианты в логе
            /// </summary>
            public bool ShowVariants { get; set; }
        }

        /// <summary>
        /// Топливо, тип - резервы в условных единицах ( TODO: разделить типы и остатки )
        /// </summary>
        [Serializable]
        public class FuelInfo
        {
            /// <summary>
            /// id - топлива
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Сколько всего
            /// </summary>
            public double Fuel { get; set; }

            /// <summary>
            /// Название для графика
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Описание генератора
        /// </summary>

        [Serializable]
        public class Generator
        {
            /// <summary>
            /// id генератора
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Максимальный возраст
            /// </summary>
            public int MaxAge { get; set; }
            /// <summary>
            /// Максимальная мощность
            /// </summary>
            public double MaxCapacity { get; set; }
            /// <summary>
            /// Типа топлива
            /// </summary>
            public int FuelTypeId { get; set; }
            /// <summary>
            /// КПД конвертации топлива в энергию
            /// </summary>
            public double Fuel2EnergyConversionRate { get; set; }

            /// <summary>
            /// Название для графика
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Структура описывающая загрузку всех генераторов в момент времени
        /// </summary>
        [Serializable]
        public class GeneratorsLoad
        {
            /// <summary>
            /// Загрузка генератора id, в этот момент времени
            /// </summary>
            public Dictionary<int, double> Load;

            /// <summary>
            /// Сгенерированная при этой загрузке энергия
            /// </summary>
            public double GeneratedEnergy;

            /// <summary>
            /// Потребленные ресурсы
            /// </summary>
            public Dictionary<int, double> ConsumedFuel;
        }


        /// <summary>
        /// Один из вариантов решения - т.е. время и загрузка всех генераторов
        /// </summary>
        [Serializable]
        public class PartialSolution
        {
            /// <summary>
            /// Загрузки генераторов для всех моментов времени
            /// </summary>
            public Dictionary<int, GeneratorsLoad> State;

            /// <summary>
            /// Остатки топлива в соответствующий момент времени
            /// </summary>
            public Dictionary<int, double> FuelRemains;


            /// <summary>
            /// Индекс по топливу
            /// </summary>
            public double FuelIndex;

            /// <summary>
            /// Кол-во шагов
            /// </summary>
            public int TotalTime;

        }

        /// <summary>
        /// Модель - конфигурация плюс решения
        /// </summary>
        public class Model
        {
            /// <summary>
            /// Конфигурация
            /// </summary>
            ModelConfiguration Configuration;

            /// <summary>
            /// Все, возможные комбинации 
            /// </summary>
            IEnumerable<PartialSolution> AllSolutions;

            /// <summary>
            /// Оптимальное решение
            /// </summary>
            public List<GeneratorsLoad> GoodVariants;

            /// <summary>
            /// Получить все возможные варианты загрузки генераторов ( рекурсия )
            /// </summary>
            /// <param name="FuelSteps">дискретность по ресурсам</param>
            /// <param name="FuelRemains">Остатки ресурсов</param>
            /// <returns></returns>
            List<GeneratorsLoad> GetAllPossibleGeneratorsLoad(int FuelSteps, Dictionary<int, double> FuelRemains)
            {

                GoodVariants = new List<GeneratorsLoad>();
                GeneratorsLoad gls = new GeneratorsLoad();
                gls.Load = new Dictionary<int, double>();
                GetPossibleGeneratorsLoad(FuelSteps, 0, 0, gls, FuelRemains, GoodVariants);

                return GoodVariants;
            }




            public int iVariant = 1;

            void CheckLoadVariant(GeneratorsLoad gls2, Dictionary<int, double> FuelRemains, List<GeneratorsLoad> GoodVariants)
            {

                // Посчитаем употребленные ресурсы и сгененированную энергию для этой комбинации
                var gls3 = Clone<GeneratorsLoad>(gls2);
                gls3.ConsumedFuel = new Dictionary<int, double>();
                foreach (var v in gls3.Load)
                {
                    var g = Configuration.Generators.Where(x => x.Id == v.Key).FirstOrDefault();
                    var ft = g.FuelTypeId;
                    var fuelconsumed = v.Value / g.Fuel2EnergyConversionRate;

                    if (gls3.ConsumedFuel.ContainsKey(ft))
                        gls3.ConsumedFuel[ft] += fuelconsumed;
                    else
                        gls3.ConsumedFuel.Add(ft, fuelconsumed);

                    gls3.GeneratedEnergy += v.Value;
                }
                // выберем только те, которые удовлетворяют граничным условиям - требуемому уровню потребления и наличию остатков ресурсов

                if (gls3.GeneratedEnergy >= Configuration.DemandLevel)
                {
                    //проверить остатки по видам топлива - если минус не проходит

                    // Вычесть потребленные ресурсы из пула
                    bool EnoughResources = true;
                    foreach (var v in gls3.ConsumedFuel)
                    {
                        if (FuelRemains[v.Key] - v.Value < 0)
                        {
                            //
                            EnoughResources = false;
                            break;
                        };
                    }

                    if (EnoughResources == true)
                        GoodVariants.Add(gls3);



                }
                else
                {

                }

                //отобразить вариант решения если установлено в настройках модели
                if (Configuration.ShowVariants)
                {
                    Console.WriteLine("Вариант " + iVariant);

                    iVariant++;
                    Console.WriteLine("Загрузка генерации:");
                    for (int i = 0; i < Configuration.Generators.Length; i++)
                    {

                        Console.Write("\t" +
                            Configuration.Generators[i].Name + "\t max - " +
                            Configuration.Generators[i].MaxCapacity + "\t");

                        if (gls3.Load.ContainsKey(Configuration.Generators[i].Id))
                        {
                            Console.Write(Math.Round(gls3.Load[Configuration.Generators[i].Id], 3));
                        }
                        Console.Write("\n");
                    }

                    Console.WriteLine("Потреблено ресурсов:");
                    foreach (var ft in Configuration.FuelTotalReserves)
                    {
                        if (gls3.ConsumedFuel.ContainsKey(ft.Id))
                            Console.WriteLine("\t" + ft.Name + " " + Math.Round(gls3.ConsumedFuel[ft.Id], 3));
                    }
                    Console.WriteLine("Сгенерировано энергии:" + Math.Round(gls3.GeneratedEnergy, 3));
                }

            }
            List<GeneratorsLoad> GL;
            GeneratorsLoad GetPossibleGeneratorsLoad(int FuelSteps, int iGenerator, int iFuelStep, GeneratorsLoad gls, Dictionary<int, double> FuelRemains, List<GeneratorsLoad> GoodVarints)
            {
                GeneratorsLoad gls2 = new GeneratorsLoad();
                gls2.Load = new Dictionary<int, double>();
                //gls2.Load=gls.Load.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                //подготовить ветку кортежа
                foreach (var v in gls.Load)
                {
                    gls2.Load.Add(v.Key, v.Value);
                }
                for (int i1 = iGenerator; i1 < Configuration.Generators.Length; i1++)
                {
                    double FuelStepSize = Configuration.Generators[i1].MaxCapacity / FuelSteps;

                    for (int iLoadStep = iFuelStep; iLoadStep < FuelSteps + 1; iLoadStep++)
                    {
                        if (gls2.Load.ContainsKey(Configuration.Generators[i1].Id))
                            gls2.Load[Configuration.Generators[i1].Id] = FuelStepSize * iLoadStep;
                        else
                            gls2.Load.Add(Configuration.Generators[i1].Id, FuelStepSize * iLoadStep);

                        // только полные 
                        if (gls2.Load.Count == Configuration.Generators.Length)
                        {
                            // проверка
                            CheckLoadVariant(gls2, FuelRemains, GoodVarints);
                        }

                        GetPossibleGeneratorsLoad(FuelSteps, i1 + 1, 0, gls2, FuelRemains, GoodVarints);
                    }
                }
                return gls2;
            }

            List<PartialSolution> GetAllSolutions(int FuelSteps)
            {

                List<PartialSolution> p = new List<PartialSolution>();
                GeneratorsLoad gls = new GeneratorsLoad();
                gls.Load = new Dictionary<int, double>();
                Dictionary<int, double> FuelRemains = new Dictionary<int, double>();
                foreach (var v in Configuration.FuelTotalReserves)
                {
                    FuelRemains.Add(v.Id, v.Fuel);
                }

                PartialSolution ps = new PartialSolution();
                ps.State = new Dictionary<int, GeneratorsLoad>();
                GetSolution(FuelSteps, 0, FuelRemains, ps);
                return p;
            }


            PartialSolution LongestSolution;

            bool GetSolution(int FuelSteps, int iTimeStep, Dictionary<int, double> FuelRemains, PartialSolution ps)
            {
                //Console.WriteLine("Поиск на шаге "+iTimeStep+" с остатками { }");

                Dictionary<int, double> ft1 = new Dictionary<int, double>();
                //подготовить ветку кортежа
                foreach (var v in FuelRemains)
                {
                    ft1.Add(v.Key, v.Value);
                }


                for (int iTime = iTimeStep; iTime < 100; iTime++)
                {

                    var GV = GetAllPossibleGeneratorsLoad(FuelSteps, ft1);
                    //Проверить все варианты и ответвиться на каждый со сдвигом времени
                    if (GV.Count < 1)
                    {
                        // завершение работы TODO: здесь можно сохранить вариант в БД
                        //Console.WriteLine("На шаге "+iTime+" кончились варианты");
                        //return false;
                        return false;
                    }

                    foreach (var gvi in GV)
                    {
                        PartialSolution ps2 = Clone<PartialSolution>(ps);

                        // Вычесть потребленные ресурсы из пула
                        Dictionary<int, double> ft2 = new Dictionary<int, double>();
                        //подготовить ветку кортежа
                        foreach (var v in ft1)
                        {
                            ft2.Add(v.Key, v.Value);
                        }
                        //Отминусовать ресурсы варианта
                        foreach (var v in gvi.ConsumedFuel)
                        {
                            ft2[v.Key] = ft2[v.Key] - v.Value;
                        }

                        // Выполнить проверку и при необходимости записать в базу данных
                        /*
                         Console.WriteLine("Шаг "+iTime+", Вариантов "+GV.Count+", Остатки ресурсов:");                        
                        foreach (var v in ft2)
                        {
                            Console.WriteLine("\t"+Configuration.FuelTotalReserves.Where(x=>x.Id==v.Key).First().Name+" "+Math.Round(v.Value,3));
                        }
                        Console.WriteLine("Сгенерированная энергия "+gvi.GeneratedEnergy);
                        */

                        ps2.State.Add(iTime, gvi);
                        ps2.TotalTime = iTime;

                        if (LongestSolution.TotalTime < ps.TotalTime)
                        {
                            ps.FuelRemains = ft2;
                            LongestSolution = Clone<PartialSolution>(ps);
                            Console.WriteLine("Новый вариант долгожитель " + ps.TotalTime); //+" ("+iTotalSolutions+")");                            
                            foreach (var v in ft2)
                            {
                                Console.WriteLine("\t" + Configuration.FuelTotalReserves.Where(x => x.Id == v.Key).First().Name + " " + Math.Round(v.Value, 3));
                            }
                        }

                        // если необходимо сохранить вариант
                        //AllSolutions=AllSolutions.Append(ps);


                        if (!GetSolution(FuelSteps, iTime + 1, ft2, ps2))
                        {
                            return false;
                            //break;
                        }

                        //                        if(CheapestSolution.FuelIndex>ps.FuelIndex && LongestSolution.TotalTime<ps.TotalTime){
                        //CheapestSolution=Clone<PartialSolution>(ps);
                        //                      }                                                                                                                                        
                    } // вариант

                }
                return true;
            }

            void PrintState(GeneratorsLoad gls3)
            {
                Console.WriteLine("Загрузка генерации:");
                for (int i = 0; i < Configuration.Generators.Length; i++)
                {

                    Console.Write("\t" +
                        Configuration.Generators[i].Name + "\t max - " +
                        Configuration.Generators[i].MaxCapacity + "\t");

                    if (gls3.Load.ContainsKey(Configuration.Generators[i].Id))
                    {
                        Console.Write(Math.Round(gls3.Load[Configuration.Generators[i].Id], 3));
                    }
                    Console.Write("\n");
                }

                Console.WriteLine("Потреблено ресурсов:");
                foreach (var ft in Configuration.FuelTotalReserves)
                {
                    if (gls3.ConsumedFuel.ContainsKey(ft.Id))
                        Console.WriteLine("\t" + ft.Name + " " + Math.Round(gls3.ConsumedFuel[ft.Id], 3));
                }
                Console.WriteLine("Сгенерировано энергии:" + Math.Round(gls3.GeneratedEnergy, 3));
            }

            /// <summary>
            /// Точка входа
            /// </summary>
            /// <param name="args">Аргументы</param>
            /// <returns></returns>
            static int Main(string[] args)
            {
                // Загружаем конфигурацию из файла
                string defaultConfig = "default.json";
                string ConfigFileName = Path.Combine(Environment.CurrentDirectory, defaultConfig);

                if (args.Length > 0)
                {
                    //Console.WriteLine("не задан");
                    //return -1;
                }
                Console.WriteLine("Загружаем конфигурацию из файла " + ConfigFileName);

                ModelConfiguration MC = new ModelConfiguration();

                if (File.Exists(ConfigFileName) == false)
                {
                    //создать пример модели

                    MC.DemandLevel = 250;
                    MC.FuelTotalReserves = new FuelInfo[]{
                    new FuelInfo{Id=1, Name="Уголь", Fuel=500 },
                    new FuelInfo{Id=2, Name="Газ", Fuel=400 }
                };

                    MC.Generators = new Generator[]{
                    new Generator{Id=1, FuelTypeId=1, MaxAge=30, MaxCapacity=200, Fuel2EnergyConversionRate=20, Name="Генератор 1"},
                    new Generator{Id=1, FuelTypeId=2, MaxAge=500, MaxCapacity=100, Fuel2EnergyConversionRate=30, Name="Генератор 2"}
                };

                    //сохранить 
                    string js = JsonSerializer.Serialize<ModelConfiguration>(MC, new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                        WriteIndented = true
                    });
                    File.WriteAllText(ConfigFileName, js, System.Text.Encoding.UTF8);

                }
                else
                {
                    try
                    {
                        MC = JsonSerializer.Deserialize<ModelConfiguration>(File.ReadAllText(ConfigFileName, System.Text.Encoding.UTF8));

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ошибка при разборе конфиг файла:" + e.Message);
                        return -1;
                    }
                    if (MC.Generators == null)
                    {
                        Console.WriteLine("Ошибка при разборе конфиг файла - отсутствует информация по генераторам");
                        return -1;
                    }

                }
                Model M = new Model();
                M.Configuration = MC;

                M.AllSolutions = Enumerable.Empty<PartialSolution>();
                M.LongestSolution = new PartialSolution();
                M.LongestSolution.TotalTime = 0;
                int FuelSteps = 5;
                Console.WriteLine("*** Запускаем перебор *** ");
                Console.WriteLine("Число ступеней в топливной компоненте: " + FuelSteps);
                M.GetAllSolutions(FuelSteps);

                Console.WriteLine("** Максимальное по продолжительности поддержания жизни решение ** ");

                foreach (var v in M.LongestSolution.State.ToArray())
                {
                    Console.WriteLine("Шаг " + v.Key + " " + v.Value.GeneratedEnergy);

                    M.PrintState(v.Value);

                }
                Console.WriteLine("Остатки ресурсов:");

                foreach (var v in M.LongestSolution.FuelRemains)
                {
                    Console.WriteLine("\t" + M.Configuration.FuelTotalReserves.Where(x => x.Id == v.Key).First().Name + " " + Math.Round(v.Value, 3));
                }

                return 1;
            }

        }

        /// <summary>
        /// Вспомогательная функция для клонирования объектов
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }

}
