using DateConversion.Context;
using DateConversion.DTOs;
using DateConversion.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateConversion
{
    public class DateRecordService
    {
        private readonly MyDbContext _context; 

        public DateRecordService(MyDbContext context)
        {
            _context = context;
        }

        public void UpdateDateDifferences()
        {
            try
            {
                var dateRecords = _context.DateRecords.ToList();

                foreach (var record in dateRecords)
                {
                    var differences = CalculateDifferences(record.OriginalDate);
                    record.DiffDate = differences;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            _context.SaveChanges();
        }

        private string CalculateDifferences(DateTime originalDate)
        {

            var timeZoneOffset = new TimeSpan(3, 0, 0); // UTC+03:00 saat dilimi (TR)

            // Statik farklı türlerde tarihlerin manuel verilmesi
            var staticDateTime = new DateTime(2024, 1, 1, 12, 45, 43);                              // -> 2024-01-01 12:45:43
            var staticDateTimeOffset = new DateTimeOffset(2024, 1, 1, 12, 43, 34, timeZoneOffset);   // -> 2024-01-01 12:43:34 +00:00
            var staticTimeSpan = new TimeSpan(1, 2, 3, 4);  // -> 1 gün, 2 saat, 3 dakika, 4 saniye
            var staticTimeOnly = new TimeOnly(12, 43, 34);  // -> 12:43:34
            var staticDateOnly = new DateOnly(2024, 1, 1);  // -> 2024-01-01

            // DateTime ile v.t deki tarihten tarih çıkarma
            var dateTimeDifference = originalDate - staticDateTime;

            // DateTimeOffset ile v.t deki tarihten tarih çıkarma
            var dateTimeOffsetDifference = new DateTimeOffset(originalDate).DateTime - staticDateTimeOffset.DateTime;

            // TimeSpan hesaplama (DateTime ile TimeSpan arasında fark hesaplama)
            var timeSpanDifference = new TimeSpan(originalDate.Ticks - staticDateTime.Ticks);

            // TimeOnly hesaplama
            //Yalnızca saat, dakika ve saniye değerlerini içerir. Fonksiyon isminden de anlaşılacağı gibi bir date alanı içermiyor.
            //DateTime içindeki saat bilgisinin işlemi yapılıyor.
            var timeOnlyDifference = new TimeOnly(originalDate.Hour, originalDate.Minute, originalDate.Second) - staticTimeOnly;

            // DateOnly hesaplama
            var originalDateOnly = new DateOnly(originalDate.Year, originalDate.Month, originalDate.Day);
            var dateOnlyDifference = originalDateOnly.ToDateTime(TimeOnly.MinValue) - staticDateOnly.ToDateTime(TimeOnly.MinValue);

            // OriginalDate'i DateTimeOffset olarak dönüştürme
            var originalDateTimeOffset = new DateTimeOffset(originalDate);

            // DateTimeOffset türü için fark hesaplama
            var dateTimeOffsetOriginalDifference = originalDateTimeOffset.DateTime - staticDateTimeOffset.DateTime;

            return $"DateTime: {dateTimeDifference}, DateTimeOffset: {dateTimeOffsetDifference}, " +
                   $"TimeSpan: {timeSpanDifference}, TimeOnly: {timeOnlyDifference}, DateOnly: {dateOnlyDifference}, " +
                   $"OriginalDateTimeOffset: {dateTimeOffsetOriginalDifference}";
        }
            
        //DateOnly ile TimeOnly birbirinden çıkartılamaz. Bunların çıkartımı için 2 sinin de ortak noktalarını içeren DateTime türüne çevirmek gerekmektedir.

    }

}
