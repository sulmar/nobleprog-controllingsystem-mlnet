using Microsoft.ML.Data;

namespace PredicateWaterConsumption.Models
{
    public class WaterConsumptionData
    {
        [LoadColumn(0)]
        public float CustomerId { get; set; } // Identyfikator klienta
        [LoadColumn(1)] 
        public string DeliveryDate { get; set; } // Data dostawy
        [LoadColumn(2)] 
        public float WaterConsumption { get; set; } // Zużycie wody
    }

    public class TransformedWaterConsumptionData
    {
        public float CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public float WaterConsumption { get; set; }
    }

    public class WaterConsumptionDeliveryData
    {
        public float CustomerId { get; set; }
        public float DaysSinceLastDelivery { get; set; }
        public float WaterConsumption { get; set; }
    }

    // Definicja klasy na wynik predykcji
    public class WaterConsumptionPrediction
    {
        [ColumnName("Score")]
        public float WaterConsumption { get; set; }
    }

}
