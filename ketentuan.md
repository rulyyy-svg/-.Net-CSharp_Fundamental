parkir
tarif per jam, batas biaya maksimal, rekap pendapatan

Ketentuan:
•⁠  ⁠Input jumlah kendaraan (1-5), lalu plat nomor, jenis (1 = Motor, 2 = Mobil), jam masuk (0-23), 
dan jam keluar (lebih dari jam masuk, maksimal 24) tiap kendaraan. Ulang input yang tidak valid.
•⁠  ⁠Tarif per jam (const): motor 2.000, mobil 5.000.
•⁠  ⁠Biaya maksimal (const): motor 10.000, mobil 25.000. Jika biaya melebihi batas, yang dibayar adalah batasnya.
•⁠  ⁠Cetak karcis tiap kendaraan, lalu jumlah motor, jumlah mobil, dan total pendapatan.
•⁠  ⁠Method minimal: NamaJenis(jenis), HitungBiaya(jenis, durasi) dengan switch, dan CetakKarcis(...).