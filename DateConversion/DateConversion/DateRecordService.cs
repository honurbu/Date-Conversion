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
            var dateRecords = _context.DateRecords.ToList();

            foreach (var record in dateRecords)
            {
                var differences = CalculateDifferences(record.OriginalDate);
                record.ConvertedDate = differences;
            }

            _context.SaveChanges();
        }

        private string CalculateDifferences(DateTime originalDate)
        {
            // Statik tarihleri burada belirliyoruz
            var staticDateTime = new DateTime(2024, 1, 1, 12, 45, 43);
            var staticDateTimeOffset = new DateTimeOffset(2024, 1, 1, 12, 43, 34, TimeSpan.Zero);
            var staticUnixTimeStamp = DateTimeOffset.FromUnixTimeSeconds(1700000000).DateTime;

            // DateTime için fark hesaplama
            var dateTimeDifference = originalDate - staticDateTime;

            // DateTimeOffset için fark hesaplama
            var dateTimeOffsetDifference = new DateTimeOffset(originalDate).DateTime - staticDateTimeOffset.DateTime;

            // UnixTimestamp için fark hesaplama
            var unixTimestampDifference = originalDate - staticUnixTimeStamp;

            // OriginalDate'i DateTimeOffset ve UnixTimestamp olarak dönüştürme
            var originalDateTimeOffset = new DateTimeOffset(originalDate);
            var originalUnixTimestamp = ((DateTimeOffset)originalDate).ToUnixTimeSeconds();

            // DateTimeOffset türü için fark hesaplama
            var dateTimeOffsetOriginalDifference = originalDateTimeOffset.DateTime - staticDateTimeOffset.DateTime;

            // UnixTimestamp türü için fark hesaplama
            var unixTimestampOriginalDate = DateTimeOffset.FromUnixTimeSeconds(originalUnixTimestamp).DateTime;
            var unixTimestampOriginalDifference = unixTimestampOriginalDate - staticUnixTimeStamp;

            return $"DateTime: {dateTimeDifference}, DateTimeOffset: {dateTimeOffsetDifference}, UnixTimestamp: {unixTimestampDifference}, " +
                   $"OriginalDateTimeOffset: {dateTimeOffsetOriginalDifference}, OriginalUnixTimestamp: {unixTimestampOriginalDifference}";
        }
    }

}
