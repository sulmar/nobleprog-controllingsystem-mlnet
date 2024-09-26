﻿// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using PredicateSalary.Models;

Console.WriteLine("Hello, World!");

// Przykladowe dane
IEnumerable<SalaryData> salaryDatas = new List<SalaryData>
{
    new SalaryData { YearsExperience = 1.1f, Salary = 39343 },
    new SalaryData { YearsExperience = 1.3f, Salary = 46205 },
    new SalaryData { YearsExperience = 1.5f, Salary = 37731 },
    new SalaryData { YearsExperience = 2.0f, Salary = 43525 },
    new SalaryData { YearsExperience = 2.2f, Salary = 39891 },
    new SalaryData { YearsExperience = 2.9f, Salary = 56642 },
    new SalaryData { YearsExperience = 3.0f, Salary = 60150 },
    new SalaryData { YearsExperience = 3.2f, Salary = 54445 },
    new SalaryData { YearsExperience = 3.2f, Salary = 64445 },
    new SalaryData { YearsExperience = 3.7f, Salary = 57189 },
    new SalaryData { YearsExperience = 3.9f, Salary = 63218 },
    new SalaryData { YearsExperience = 4.0f, Salary = 55794 },
    new SalaryData { YearsExperience = 4.5f, Salary = 56957 },
    new SalaryData { YearsExperience = 4.9f, Salary = 57081 },
    new SalaryData { YearsExperience = 5.1f, Salary = 61111 },
    new SalaryData { YearsExperience = 5.3f, Salary = 67938 },
    new SalaryData { YearsExperience = 5.9f, Salary = 66029 },
    new SalaryData { YearsExperience = 6.0f, Salary = 83088 },
    new SalaryData { YearsExperience = 6.8f, Salary = 81363 },
    new SalaryData { YearsExperience = 7.1f, Salary = 93940 },
    new SalaryData { YearsExperience = 7.9f, Salary = 91738 },
    new SalaryData { YearsExperience = 8.2f, Salary = 98273 },
    new SalaryData { YearsExperience = 8.7f, Salary = 101302 },
    new SalaryData { YearsExperience = 9.0f, Salary = 113812 },
    new SalaryData { YearsExperience = 9.5f, Salary = 109431 },
    new SalaryData { YearsExperience = 9.6f, Salary = 105583 },
    new SalaryData { YearsExperience = 10.3f, Salary = 122391 },
    new SalaryData { YearsExperience = 10.5f, Salary = 121872 },
    new SalaryData { YearsExperience = 11.0f, Salary = 125000 },
    new SalaryData { YearsExperience = 11.5f, Salary = 126856 },
    new SalaryData { YearsExperience = 12.0f, Salary = 128765 },
    new SalaryData { YearsExperience = 12.3f, Salary = 135675 },
    new SalaryData { YearsExperience = 12.9f, Salary = 137875 },
    new SalaryData { YearsExperience = 13.5f, Salary = 139396 },
    new SalaryData { YearsExperience = 14.0f, Salary = 142000 },
    new SalaryData { YearsExperience = 14.5f, Salary = 145045 },
    new SalaryData { YearsExperience = 15.0f, Salary = 146788 }
};

// dotnet add package Microsoft.ML

// 1. Utworzenie nowego kontekstu ML.NET
MLContext context = new MLContext(seed: 1);

// 2. Wczytanie danych z kolekcji do DataView
IDataView dataView = context.Data.LoadFromEnumerable(salaryDatas);


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
