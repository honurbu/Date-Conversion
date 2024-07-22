using DateConversion.Context;
using DateConversion;
using System;

class Program
{
    static void Main()
    {
        using (var context = new MyDbContext())
        {
            var service = new DateRecordService(context);
            service.UpdateDateDifferences();
        }

        Console.WriteLine("Veritabanı güncellendi.");
    }
}
