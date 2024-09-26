﻿// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using PredicateSalary.Models;

Console.WriteLine("Hello, World!");

var dataPath = Path.Combine("Data", "salary-data.csv");

// dotnet add package Microsoft.ML

// 1. Utworzenie nowego kontekstu ML.NET
MLContext context = new MLContext(seed: 1);

// 2. Wczytanie danych z pliku CSV
IDataView dataView = context.Data.LoadFromTextFile<SalaryData>(dataPath, separatorChar: ',', hasHeader: true);


// 3. Wybor algorytmu
var trainer = context.Regression.Trainers.Sdca(labelColumnName: nameof(SalaryData.Salary));

// 4. Utworzenie schematu przetwarzania
var pipeline = 
    context.Transforms.Concatenate("Features", nameof(SalaryData.YearsExperience))
    .Append(trainer);

// 5. Trenowanie modelu na podstawie danych
Console.WriteLine("Training...");
var trainedModel = pipeline.Fit(dataView);

Console.WriteLine("Done.");

// 6. Przykładowane dane do predykcji

var sampleData = new SalaryData { YearsExperience = 5.5f };

// 7. Tworzymy funkcje predykcji 
var predictionFunction = context.Model.CreatePredictionEngine<SalaryData, SalaryPrediction>(trainedModel);

// 8. Wywołanie predykcji
var prediction = predictionFunction.Predict(sampleData);

// 9 Wyswietlenie wyniku predykcji
Console.WriteLine($"Przewidywana pensja dla {sampleData.YearsExperience} lat doswiadczenia wynosi {prediction.Salary}");


var prediction2 = predictionFunction.Predict(sampleData);

// 9 Wyswietlenie wyniku predykcji
Console.WriteLine($"Przewidywana pensja dla {sampleData.YearsExperience} lat doswiadczenia wynosi {prediction2.Salary}");
