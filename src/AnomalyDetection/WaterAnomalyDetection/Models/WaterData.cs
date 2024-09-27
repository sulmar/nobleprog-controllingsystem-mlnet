using Microsoft.ML.Data;

namespace Water1Detection.Models;

public class WaterData
{
    [LoadColumn(0)]
    public float ODBIORCA_ID { get; set; }

    [LoadColumn(1)]
    public float OBSZAR_ID { get; set; }

    [LoadColumn(2)]
    public float ZRODLO_ID { get; set; }

    [LoadColumn(3)]
    public float SEZON_ID { get; set; }

    [LoadColumn(4)]
    public float OPIS_ID { get; set; }

    [LoadColumn(5)]
    public float POBOR { get; set; }

    [LoadColumn(6)]
    public float CZAS_ID { get; set; }

    [LoadColumn(7)]
    public float Anomaly { get; set; }
} 

public class WaterPrediction
{
    public uint PredictedLabel { get; set; }
    public float[] Score { get; set; }
}
