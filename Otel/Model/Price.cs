﻿namespace Otel.Model
{
    /// <summary>
    /// Класс, реализующий модель Price
    /// </summary>
    public class Price
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public int CurrencyID { get; set; }

        public Currency Currency { get; set; }
    }
}