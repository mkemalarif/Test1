using Dapper;
using Microsoft.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
namespace Test3
{
    //pengaturan database saya
    //create database TestProject;
    //use TestProject;

    //create table Discount (TipeCustomer nvarchar(30),
    //PointReward int, TotalBelanja decimal, Diskon decimal,
    //TotalBayar decimal, TransaksiId nvarchar(50));

    internal class Program
    {
        public static void Main(string[] args)
        {
            CalculateDiscount("platinum", 200, 500);
            CalculateDiscount("gold", 396, 100);
        }

        static void CalculateDiscount(string tipeCustomer, int rewardPoints, decimal Total_Belanja)
        {
            //ganti pengaturan conn untuk servernya disini, saya menggunakan msql sebagai db saya
            using var connection = new SqlConnection("Server=DROIDS13;Database=TestProject;Trusted_Connection=True;TrustServerCertificate=True") ;
            decimal discount = 0;
            tipeCustomer = tipeCustomer.ToLower();
            if (tipeCustomer == "platinum")
            {
                if (rewardPoints >= 100 && rewardPoints <= 300)
                    discount = Total_Belanja * 0.5m + 35;
                else if (rewardPoints >= 301 && rewardPoints <= 500)
                    discount = Total_Belanja * 0.5m + 50;
                else if (rewardPoints > 500)
                    discount = Total_Belanja * 0.5m + 68;
            }
            else if (tipeCustomer == "gold")
            {
                if (rewardPoints >= 100 && rewardPoints <= 300)
                    discount = Total_Belanja * 0.25m + 25;
                else if (rewardPoints >= 301 && rewardPoints <= 500)
                    discount = Total_Belanja * 0.25m + 34;
                else if (rewardPoints > 500)
                    discount = Total_Belanja * 0.25m + 52;
            }
            else if (tipeCustomer == "silver")
            {
                if (rewardPoints >= 100 && rewardPoints <= 300)
                    discount = Total_Belanja * 0.1m + 12;
                else if (rewardPoints >= 301 && rewardPoints <= 500)
                    discount = Total_Belanja * 0.1m + 27;
                else if (rewardPoints > 500)
                    discount = Total_Belanja * 0.1m + 39;
            }

            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            decimal totalPayment = Total_Belanja - discount;

            //setiap kali data tertambah maka transaksi id akan naik 1 sesuai dengan running number
            var checkDataOnDb = connection.Query("Select * from Discount");
            var idTransaksi = checkDataOnDb.Count() + 1;
            var result = date + "_" +idTransaksi.ToString("D5");
            var data = new { tipeCustomer, rewardPoints, Total_Belanja, discount, totalPayment, result };
            var sql = connection.Execute("INSERT INTO Discount(TipeCustomer, PointReward," +
                "TotalBelanja, Diskon, TotalBayar, TransaksiId) VALUES (@tipeCustomer, @rewardPoints, @Total_Belanja, @discount, @totalPayment, @result)", data);


            Console.WriteLine($"Tipe Customer: {tipeCustomer}, Point Reward: {rewardPoints}, Total Belanja: {Total_Belanja}, Diskon: {discount}, Total Bayar: {totalPayment}, waktu: {date}, {result}");
        }
    }
}
