using Microsoft.ML.Data;

namespace PredicateSalary.Models;

// Definicja klasy na dane wejściowe
public class SalaryData
{
    public float YearsExperience { get; set; }
    public float Salary { get; set; }
}


// Definicja klasy na dane wyjściowe
public class SalaryPrediction
{
    [ColumnName("Score")]
    public float Salary { get; set; }
}
