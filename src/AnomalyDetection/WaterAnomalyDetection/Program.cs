using Microsoft.ML;
using Microsoft.ML.Trainers;
using Water1Detection.Models;

Console.WriteLine("Hello, World!");


var dataPath = Path.Combine("Data", "water-data.csv");

// 1. Utworzenie nowego kontekstu ML.NET
var context = new MLContext();

// 2. Wczytanie danych z pliku CSV
IDataView data = context.Data.LoadFromTextFile<WaterData>(dataPath, separatorChar: ',', hasHeader: true);

// 3. Utwórz trenera
var options = new KMeansTrainer.Options { InitializationAlgorithm = KMeansTrainer.InitializationAlgorithm.KMeansPlusPlus, NumberOfClusters = 3 };

var trainer = context.Clustering.Trainers.KMeans(options);

// 4. Utwórz pipeline
var pipeline =
      context.Transforms.Concatenate("Features", "ODBIORCA_ID", "OBSZAR_ID", "ZRODLO_ID", "SEZON_ID", "POBOR", "CZAS_ID")
   // context.Transforms.Concatenate("Features", "ODBIORCA_ID", "POBOR")
    .Append(trainer);

var preview = data.Preview();

// 5. Trenujemy
var model = pipeline.Fit(data);

// 6. Uzyjemy modelu do predykcji
var predictions = model.Transform(data);

var predictionsResults = context.Data.CreateEnumerable<WaterPrediction>(predictions, reuseRowObject: false);

foreach (var prediction in predictionsResults)
{
    Console.WriteLine($"Predykowany klaster: {prediction.PredictedLabel}, Wynik: {string.Join(", ", prediction.Score)}");

    for(int i = 0; i < prediction.Score.Length; i++)
    {
        Console.WriteLine($"Odległość od centroidu {i}: {prediction.Score[i]}");
    }
}



