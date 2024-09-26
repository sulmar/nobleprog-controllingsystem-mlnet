using Microsoft.ML.Data;

namespace PredicateSalary.Models;

// Definicja klasy na dane wejściowe
public class SalaryData
{
    [LoadColumn(0)]
    public float YearsExperience { get; set; }

    [LoadColumn(1)]
    public float Salary { get; set; }
}


// Definicja klasy na dane wyjściowe
public class SalaryPrediction
{
    [ColumnName("Score")]
    public float Salary { get; set; }
}
