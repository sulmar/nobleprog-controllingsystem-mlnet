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
    public string CZAS_ID { get; set; }

    [LoadColumn(7)]
    public float Anomaly { get; set; }

    public override string ToString() => $"TransformedWaterData: ODBIORCA_ID = {ODBIORCA_ID}, OBSZAR_ID = {OBSZAR_ID}, ZRODLO_ID = {ZRODLO_ID}, SEZON_ID = {SEZON_ID}, OPIS_ID = {OPIS_ID}, POBOR = {POBOR}, CZAS_ID = {CZAS_ID}, Anomaly = {Anomaly}";

}


public class TransformedWaterData
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

    public override string ToString() => $"TransformedWaterData: ODBIORCA_ID = {ODBIORCA_ID}, OBSZAR_ID = {OBSZAR_ID}, ZRODLO_ID = {ZRODLO_ID}, SEZON_ID = {SEZON_ID}, OPIS_ID = {OPIS_ID}, POBOR = {POBOR}, CZAS_ID = {CZAS_ID}, Anomaly = {Anomaly}";


}


public class WaterResult
{
    public bool PredictedLabel { get; set; }
    public float Score { get; set; }

    public float ODBIORCA_ID { get; set; }
    public float OBSZAR_ID { get; set; }
    public float ZRODLO_ID { get; set; }
    public float SEZON_ID { get; set; }
    public float OPIS_ID { get; set; }
    public float POBOR { get; set; }
    public float CZAS_ID { get; set; }
    public float Anomaly { get; set; }

    public override string ToString() => $"TransformedWaterData: ODBIORCA_ID = {ODBIORCA_ID}, OBSZAR_ID = {OBSZAR_ID}, ZRODLO_ID = {ZRODLO_ID}, SEZON_ID = {SEZON_ID}, OPIS_ID = {OPIS_ID}, POBOR = {POBOR}, CZAS_ID = {CZAS_ID}, Anomaly = {Anomaly}";
}
