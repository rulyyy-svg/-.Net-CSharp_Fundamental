
using System.Globalization;

namespace SeriesA
{
    internal class Program
    {
        const int tarifMotor = 2000;
        const int tarifMobil = 5000;
        const int maksMotor = 10000;
        const int maksMobil = 25000;

        static void Main(string[] args)
        {
            string[] plat = new string[5];
            int[] jenis = new int[5];
            DateTime[] masuk = new DateTime[5];
            DateTime[] keluar = new DateTime[5];
            int[] durasi = new int[5];
            int[] biaya = new int[5];
            int[] bayar = new int[5];
            int[] kembalian = new int[5];

            int jumlahMotor = 0, jumlahMobil = 0;
            int totalPendapatan = 0, totalKembalian = 0;
            int totalDurasi = 0, jumlah;

            Console.WriteLine("=== KALIPUT PARKIR ===");

            // Input jumlah kendaraan
            do
            {
                Console.Write("Jumlah kendaraan (1-5): ");
            }
            while (!int.TryParse(Console.ReadLine(), out jumlah)
                || jumlah < 1 || jumlah > 5);

            for (int i = 0; i < jumlah; i++)
            {
                Console.WriteLine($"\n--- Kendaraan ke-{i + 1} ---");

                // Input plat nomor
                do
                {
                    Console.Write("Plat nomor: ");
                    plat[i] = Console.ReadLine() ?? "";
                }
                while (string.IsNullOrWhiteSpace(plat[i]));

                // Input jenis kendaraan
                do
                {
                    Console.Write("Jenis (1=Motor, 2=Mobil): ");
                }
                while (!int.TryParse(Console.ReadLine(), out jenis[i])
                    || (jenis[i] != 1 && jenis[i] != 2));

                // Input tanggal dan jam masuk
                masuk[i] = InputWaktu("Masuk (dd/MM/yyyy HH:mm): ");

                // Input tanggal dan jam keluar
                do
                {
                    keluar[i] = InputWaktu(
                        "Keluar (dd/MM/yyyy HH:mm): ");

                    if (keluar[i] <= masuk[i])
                        Console.WriteLine(
                            "Waktu keluar harus setelah waktu masuk.");
                }
                while (keluar[i] <= masuk[i]);

                // Durasi dibulatkan ke atas per jam
                durasi[i] = (int)Math.Ceiling(
                    (keluar[i] - masuk[i]).TotalHours);

                // Menghitung biaya
                biaya[i] = HitungBiaya(jenis[i], durasi[i]);

                Console.WriteLine($"Durasi : {durasi[i]} jam");
                Console.WriteLine($"Biaya  : Rp{biaya[i]:N0}");

                // Input pembayaran
                do
                {
                    Console.Write("Uang bayar: Rp");
                }
                while (!int.TryParse(Console.ReadLine(), out bayar[i])
                    || bayar[i] < biaya[i]);

                kembalian[i] = bayar[i] - biaya[i];

                // Rekap transaksi
                if (jenis[i] == 1)
                    jumlahMotor++;
                else
                    jumlahMobil++;

                totalPendapatan += biaya[i];
                totalKembalian += kembalian[i];
                totalDurasi += durasi[i];

                // Cetak karcis
                Console.WriteLine("\n=== KARCIS PARKIR ===");
                Console.WriteLine($"Plat       : {plat[i]}");
                Console.WriteLine($"Jenis      : {NamaJenis(jenis[i])}");
                Console.WriteLine($"Masuk      : {masuk[i]:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Keluar     : {keluar[i]:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Durasi     : {durasi[i]} jam");
                Console.WriteLine($"Biaya      : Rp{biaya[i]:N0}");
                Console.WriteLine($"Uang bayar : Rp{bayar[i]:N0}");
                Console.WriteLine($"Kembalian  : Rp{kembalian[i]:N0}");
                Console.WriteLine("=====================");
            }

            // Rekap akhir
            Console.WriteLine("\n=== REKAP PARKIR ===");
            Console.WriteLine($"Jumlah kendaraan : {jumlah}");
            Console.WriteLine($"Jumlah motor     : {jumlahMotor}");
            Console.WriteLine($"Jumlah mobil     : {jumlahMobil}");
            Console.WriteLine($"Total durasi     : {totalDurasi} jam");
            Console.WriteLine($"Total pendapatan : Rp{totalPendapatan:N0}");
            Console.WriteLine($"Total kembalian  : Rp{totalKembalian:N0}");
            Console.WriteLine(
                $"Rata-rata biaya  : Rp{(decimal)totalPendapatan / jumlah:N0}");
            Console.WriteLine("=====================");
        }

        // Membaca tanggal dan jam
        static DateTime InputWaktu(string pesan)
        {
            DateTime waktu;

            while (true)
            {
                Console.Write(pesan);
                string input = Console.ReadLine() ?? "";

                if (DateTime.TryParseExact(
                    input,
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out waktu))
                {
                    return waktu;
                }

                Console.WriteLine(
                    "Format salah. Contoh: 12/10/2026 22:30");
            }
        }

        // Mengubah kode menjadi nama kendaraan
        static string NamaJenis(int jenis)
        {
            return jenis == 1 ? "Motor" : "Mobil";
        }

        // Menghitung biaya parkir
        static int HitungBiaya(int jenis, int durasi)
        {
            int tarif = jenis == 1 ? tarifMotor : tarifMobil;
            int maksimal = jenis == 1 ? maksMotor : maksMobil;

            return Math.Min(tarif * durasi, maksimal);
        }
    }
}
