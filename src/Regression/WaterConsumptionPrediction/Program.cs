// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using PredicateWaterConsumption.Models;
using System.Globalization;

Console.WriteLine("Hello, World!");

// dotnet add package Microsoft.ML

// 1. Utworzenie nowego kontekstu ML.NET
var context = new MLContext();

var dataPath = Path.Combine("Data", "water_consumption.csv");

// 2. Załadowanie danych z pliku CSV
IDataView dwaterConsumptionDataView = context.Data.LoadFromTextFile<WaterConsumptionData>(dataPath, hasHeader: false, separatorChar: ';');

// Utworzenie mapowania daty
Action<WaterConsumptionData, TransformedWaterConsumptionData> mapping = (input, output) =>
{
    output.CustomerId = input.CustomerId;
    output.DeliveryDate = DateTime.ParseExact(input.DeliveryDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    output.WaterConsumption = input.WaterConsumption;
};

// 4. Transformacje

// Konwersja daty na DateTime
var pipeline = context.Transforms.CustomMapping<WaterConsumptionData, TransformedWaterConsumptionData>(mapping, "MapWaterConsumption");

var model = pipeline.Fit(dwaterConsumptionDataView);
var transformed = model.Transform(dwaterConsumptionDataView);


// Przetworzenie danych do listy
List<TransformedWaterConsumptionData> deliveries = context.Data.CreateEnumerable<TransformedWaterConsumptionData>(transformed, reuseRowObject: false).ToList();


// Obliczanie różnicy dni między kolejnymi dostawami
var aggregatedWaterConsumptionData = deliveries
            .GroupBy(x => x.CustomerId)
            .SelectMany(group =>
            {
                // Sortowanie dostaw według daty
                var orderedDeliveries = group.OrderBy(x => x.DeliveryDate).ToList();

                // Obliczanie różnicy dni między kolejnymi dostawami z użyciem Linq i funkcji Zip
                return orderedDeliveries.Zip(orderedDeliveries.Skip(1), (first, second) => new WaterConsumptionDeliveryData
                {
                    CustomerId = first.CustomerId,
                    DaysSinceLastDelivery = (float)(second.DeliveryDate - first.DeliveryDate).Days,
                    WaterConsumption = second.WaterConsumption
                });
            });

// Konwersja przetworzonych danych do IDataView
var processedDataView = context.Data.LoadFromEnumerable(aggregatedWaterConsumptionData);

// Podgląd przetworzonych danych
var preview = processedDataView.Preview();


// Połączenie cech
var featureConcatenation = context.Transforms.Concatenate("Features", new[] { "CustomerId", "DaysSinceLastDelivery" });

// Tworzenie modelu FastTree dla regresji
// dotnet add package Microsoft.ML.FastTree
var trainer = context.Regression.Trainers.FastTree(labelColumnName: "WaterConsumption");


var trainingPipeline = context.Transforms.Categorical.OneHotEncoding("CustomerId")
    .Append(featureConcatenation)
    .Append(trainer);

// 3. Podział na zbiór treningowy i testowy
var splitData = context.Data.TrainTestSplit(processedDataView, testFraction: 0.2);
var trainData = splitData.TrainSet;
var testData = splitData.TestSet;

// Trenowanie modelu
var model2 = trainingPipeline.Fit(trainData);

// Przewidywanie przyszłego zużycia wody
var predictions = model2.Transform(testData);

// Ocena modelu na danych testowych
var metrics = context.Regression.Evaluate(predictions, labelColumnName: "WaterConsumption");

// Wyświetlanie wyników oceny modelu
Console.WriteLine($"R^2: {metrics.RSquared}");
Console.WriteLine($"RMSE: {metrics.RootMeanSquaredError}");

// 7. Predykcja zużycia wody na nowych danych
var predictionEngine = context.Model.CreatePredictionEngine<WaterConsumptionDeliveryData, WaterConsumptionPrediction>(model2);

// Nowe dane do predykcji zużycia wody na podstawie ilości dni od poprzedniej dostawy
var newSample = new WaterConsumptionDeliveryData
{
    CustomerId = 2, // przykładowe dane
    DaysSinceLastDelivery = 30
};

var result = predictionEngine.Predict(newSample);

Console.WriteLine($"Predykcja: Zużycie wody = {result.WaterConsumption}");

