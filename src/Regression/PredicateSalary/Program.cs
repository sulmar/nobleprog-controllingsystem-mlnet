// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using PredicateSalary.Models;

Console.WriteLine("Hello, World!");

// TrainAndSaveModel();

for(float yearsExperience = 1.0f ; yearsExperience <= 65; yearsExperience = yearsExperience + 0.5F)
{ 
    UseTrainedModel(yearsExperience);

   // Thread.Sleep(1000);
}

static void TrainAndSaveModel()
{
    var dataPath = Path.Combine("Data", "salary-data.csv");

    // dotnet add package Microsoft.ML

    // 1. Utworzenie nowego kontekstu ML.NET
    var context = new MLContext(seed: 1);

    // 2. Wczytanie danych z pliku CSV
    IDataView dataView = context.Data.LoadFromTextFile<SalaryData>(dataPath, separatorChar: ',', hasHeader: true);

    var dataSplit = context.Data.TrainTestSplit(dataView, testFraction: 0.2);
    var trainingData = dataSplit.TrainSet;
    var testData = dataSplit.TestSet;


    // 3. Wybor algorytmu
    var trainer = context.Regression.Trainers.Sdca(labelColumnName: nameof(SalaryData.Salary));

    // 4. Utworzenie schematu przetwarzania
    var pipeline =
        context.Transforms.Concatenate("Features", nameof(SalaryData.YearsExperience))
        .Append(trainer);

    // 5. Trenowanie modelu na podstawie danych
    Console.WriteLine("Training...");
    var trainedModel = pipeline.Fit(trainingData);
    Console.WriteLine("Done.");

    // Ocena modelu na podstawie danych testowych
    var predictions = trainedModel.Transform(testData);

    var metrics = context.Regression.Evaluate(predictions, labelColumnName: nameof(SalaryData.Salary));

    // Wyswietlenie metryk
    Console.WriteLine($"Wspolczynnik determinacji (R^2): {metrics.RSquared}");

    // Zapisanie wytrenowanego modelu do pliku
    context.Model.Save(trainedModel, trainingData.Schema, "predicate-salary-model.zip");
}

static void UseTrainedModel(float yearsExperience)
{
    // 1. Tworzymy kontekst 
    var context = new MLContext();

    // 2. Ladujemy model
    var trainedModel = context.Model.Load("predicate-salary-model.zip", out _);

    // 6. Przykładowane dane do predykcji

    var sampleData = new SalaryData { YearsExperience = yearsExperience };

    // 7. Tworzymy funkcje predykcji 
    var predictionFunction = context.Model.CreatePredictionEngine<SalaryData, SalaryPrediction>(trainedModel);

    // 8. Wywołanie predykcji
    var prediction = predictionFunction.Predict(sampleData);

    // 9 Wyswietlenie wyniku predykcji
    Console.WriteLine($"Przewidywana pensja dla {sampleData.YearsExperience} lat doswiadczenia wynosi {prediction.Salary}");


    var prediction2 = predictionFunction.Predict(sampleData);

    // 9 Wyswietlenie wyniku predykcji
    Console.WriteLine($"Przewidywana pensja dla {sampleData.YearsExperience} lat doswiadczenia wynosi {prediction2.Salary}");
}