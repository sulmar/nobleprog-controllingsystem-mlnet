// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using System.Data;
using System.Globalization;
using Water1Detection.Models;

Console.WriteLine("Hello, World!");

var dataPath = Path.Combine("Data", "wyniki_anomalie.csv");

// 1. Utworzenie nowego kontekstu ML.NET
var context = new MLContext();

// 2. Wczytanie danych z pliku CSV
IDataView data = context.Data.LoadFromTextFile<WaterData>(dataPath, separatorChar: ';', hasHeader: true);

var dataSplit = context.Data.TrainTestSplit(data, testFraction: 0.4);
var trainingData = dataSplit.TrainSet;
var testData = dataSplit.TestSet;

// 3. Utwórz trenera
var options = new Microsoft.ML.Trainers.RandomizedPcaTrainer.Options()
{
    Rank = 6
};

var trainer = context.AnomalyDetection.Trainers.RandomizedPca(options);

Action<WaterData, TransformedWaterData> mapping = (input, output) =>
{
    output.POBOR = input.POBOR;
    output.OBSZAR_ID = input.OBSZAR_ID;
    output.ODBIORCA_ID = input.ODBIORCA_ID;
    output.OPIS_ID = input.OPIS_ID;
    output.ZRODLO_ID = input.ZRODLO_ID;
    output.SEZON_ID = input.SEZON_ID;
    output.SEZON_ID = 0;
    //   output.Anomaly = input.Anomaly;
    output.CZAS_ID = DateTime.ParseExact(input.CZAS_ID, "yyyyMMdd", CultureInfo.InvariantCulture).Month;
};

var concatenate = context.Transforms.Concatenate("Features",
    "ODBIORCA_ID_Encoded", "OBSZAR_ID_Encoded", "ZRODLO_ID_Encoded", "SEZON_ID_Encoded", "POBOR", "CZAS_ID_Encoded", "Anomaly");

var customerId = context.Transforms.Categorical.OneHotEncoding(
    outputColumnName: "ODBIORCA_ID_Encoded",
    inputColumnName: "ODBIORCA_ID");

var areaId = context.Transforms.Categorical.OneHotEncoding(
    outputColumnName: "OBSZAR_ID_Encoded",
    inputColumnName: "OBSZAR_ID");


var sourceId = context.Transforms.Categorical.OneHotEncoding(
    outputColumnName: "ZRODLO_ID_Encoded",
    inputColumnName: "ZRODLO_ID");

var seasonId = context.Transforms.Categorical.OneHotEncoding(
    outputColumnName: "SEZON_ID_Encoded",
    inputColumnName: "SEZON_ID");

var timeId = context.Transforms.Categorical.OneHotEncoding(
    outputColumnName: "CZAS_ID_Encoded",
    inputColumnName: "CZAS_ID");

// 4. Transformacje
var pipeline = context.Transforms.CustomMapping(mapping, null)
    .Append(customerId)
    .Append(areaId)
    .Append(sourceId)
    .Append(seasonId)
    .Append(timeId)
     .Append(concatenate)
    .Append(trainer);

var model = pipeline.Fit(trainingData);

var transformed = model.Transform(trainingData);


var preview = transformed.Preview();

var results = context.Data.CreateEnumerable<WaterResult>(transformed,
                reuseRowObject: false).ToList();


var samples = context.Data.CreateEnumerable<WaterData>(trainingData, reuseRowObject: false).ToList();


//// Let's go through all predictions.
//for (int i = 0; i < samples.Count; ++i)
//{
//    // The i-th example's prediction result.
//    var result = results[i];

//    // The i-th example's feature vector in text format.
//    var sample = samples[i];

//    var featuresInText = sample.ToString();

//    if (result.PredictedLabel)
//    {
//        Console.ForegroundColor = ConsoleColor.Red;
//        // The i-th sample is predicted as an outlier.
//        Console.WriteLine("The {0}-th example with features [{1}] is" +
//            "an outlier with a score of being outlier {2}", i,
//            featuresInText, result.Score);

//        Console.ResetColor();
//    }
//    else
//        // The i-th sample is predicted as an inlier.
//        Console.WriteLine("The {0}-th example with features [{1}] is" +
//            "an inlier with a score of being outlier {2}",
//            i, featuresInText, result.Score);
//}

foreach (var result in results)
{
    if (result.PredictedLabel)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    Console.WriteLine($"{result} {result.PredictedLabel} {result.Score}");

    Console.ResetColor();
}


Console.WriteLine();
Console.ReadLine();