# Stealth AI Sandbox - Ariverse Internship Test
**Kandidat:** Rega
**Posisi:** Game Programmer Intern

## 🎮 Informasi Game

**Catatan Pengembangan (Penting):**
Meskipun deskripsi pada dokumen teknis menyebutkan 2D, proyek ini dikembangkan menggunakan *environment* 3D dengan perspektif kamera *Top-Down*. Keputusan ini diambil untuk memanfaatkan stabilitas fitur `NavMesh` bawaan Unity yang sangat handal dalam menangani sistem navigasi dan *pathfinding* AI secara dinamis.

**Kontrol Player & UI Navigation:**
- **W, A, S, D**: Bergerak (Camera-Relative Movement - arah WASD mengikuti arah layar).
- **Hold L-Shift + W/A/S/D**: Mengendap-endap (Sneak Mode) - mengurangi kecepatan dan suara langkah agar tidak terdengar Guard.
- **Hold Klik Kanan + Geser Mouse**: Memutar sudut pandang kamera (Orbit Camera).
- **Tombol 'R'**: Restart level saat layar Game Over / Game Win muncul.
- **Tombol 'ESC'**: Keluar dari aplikasi.

**Kondisi Menang & Kalah:**
- **Win Condition**: Player berhasil mencapai area *Finish Zone* (objektif) tanpa terdeteksi.
- **Lose Condition**: Guard berhasil mendeteksi dan mendekati player (jarak < 0.5f), memicu kondisi tertangkap (Caught).

## ✨ Fitur yang Berhasil Diimplementasikan
1. **Camera-Relative Movement**: Pergerakan player disinkronkan dengan rotasi kamera agar kontrol tetap intuitif.
2. **Sneak Mechanic**: Sistem modifikasi kecepatan yang terintegrasi dengan radius pendengaran AI.
3. **Data-Driven Guard AI**: Penggunaan `ScriptableObject` (`GuardDataSO`) sehingga parameter seperti radius pandangan dan kecepatan bisa diubah tanpa menyentuh kode.
4. **Finite State Machine (FSM)**: Logika AI yang terstruktur menggunakan *Interface* (Idle, Patrol, Suspicious, Alert, Searching) untuk menghindari *spaghetti code*.
5. **Vision Cone & Obstacle Detection**: AI memiliki sudut pandang terbatas dan tidak bisa melihat menembus tembok (menggunakan Raycast).
6. **Hearing Radius**: AI akan bereaksi dan mendatangi sumber suara jika player berlari di dekatnya.
7. **Last Known Position**: Guard akan bergerak menuju lokasi terakhir player terlihat atau terdengar sebelum memulai pencarian.

## 🚧 Fitur yang Tidak Sempat Diselesaikan
1. **Komunikasi Antar Guard**: Tidak diimplementasikan karena keterbatasan waktu. Saya lebih memprioritaskan stabilitas sistem navigasi agar Guard tidak saling tabrak saat berpatroli.
2. **Audio/SFX Layered**: Fokus utama diletakkan pada penyelesaian *core logic* AI dan sistem *state machine*.

## 🧠 Penjelasan Sistem AI

**1. State Machine (FSM):**
AI dikelola melalui `GuardController` yang memindahkan *state* secara eksplisit:
- `PatrolState`: Berkeliling antar *waypoints*.
- `IdleState`: Berhenti sejenak untuk mengamati area.
- `AlertState`: Mengejar player secara agresif.
- `SearchingState`: Mencari di sekitar `lastKnownPosition`.

**2. Kalkulasi Vision & Hearing:**
- **Vision**: Menggunakan kalkulasi jarak (`Vector3.Distance`) dan sudut (`Vector3.Angle`). Jika player dalam jangkauan, sistem melakukan *Raycast* untuk mengecek halangan objek.
- **Hearing**: Mengambil data `velocity.magnitude` dari *Rigidbody* player. Jika player bergerak di atas kecepatan *sneak* di dalam radius pendengaran, AI akan terpicu.

## 🪞 Refleksi

**Tantangan Terbesar:**
Menangani sistem navigasi saat ada lebih dari satu Guard agar mereka tidak saling dorong keluar dari `NavMesh`. Selain itu, merancang pergerakan *camera-relative* yang mulus juga membutuhkan pemahaman vektor yang cukup dalam.

**Known Bugs / Limitasi:**
- Karena menggunakan *box collider* standar, terkadang kamera bisa sedikit *clipping* jika menempel tembok di sudut sempit.
- NavMeshAgent sesekali terlihat kaku saat melakukan rotasi tajam di tikungan.

## 🤖 AI Usage Policy
Dalam pengerjaan proyek ini, saya menggunakan **Gemini AI** sebagai asisten diskusi dan *troubleshooting* untuk:
- Memperbaiki *error* navigasi pada `NavMeshAgent`.
- Merumuskan logika matematika vektor untuk pergerakan *camera-relative*.
- Mengoptimalkan struktur *Finite State Machine* agar sesuai dengan standar kode yang mudah di-*extend*.
