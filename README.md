# Stealth AI Sandbox - Ariverse Internship Test
**Kandidat:** Rega
[cite_start]**Posisi:** Game Programmer Intern [cite: 97]

## [cite_start]🎮 Informasi Game [cite: 102]

[cite_start]**Kontrol Player & UI Navigation:** [cite: 103]
- **W, A, S, D**: Bergerak (Camera-Relative Movement).
- [cite_start]**Hold L-Shift + W/A/S/D**: Mengendap-endap (Sneak Mode) - mengurangi kecepatan dan suara langkah[cite: 78].
- **Hold Right Click + Mouse Move**: Memutar sudut pandang kamera.
- [cite_start]**Tombol 'R'**: Restart level saat layar Game Over / Game Win muncul[cite: 85].
- **Tombol 'ESC'**: Keluar dari aplikasi.

[cite_start]**Kondisi Menang & Kalah:** [cite: 104]
- [cite_start]**Win Condition**: Player berhasil mencapai *Finish Zone* di ujung map tanpa terdeteksi[cite: 85].
- [cite_start]**Lose Condition**: Guard berhasil mendekat dan menangkap player (jarak < 0.5f), memicu state "Caught"[cite: 85].

[cite_start]**Daftar Fitur yang Berhasil Diimplementasikan:** [cite: 105]
1. **Camera-Relative Movement**: Pergerakan player menyesuaikan arah pandangan kamera secara dinamis.
2. [cite_start]**Sneak Mechanic**: Modifikasi *walk speed* saat menahan tombol Shift[cite: 78].
3. [cite_start]**Data-Driven Guard AI**: Penggunaan `ScriptableObject` untuk mengatur parameter Guard tanpa *hardcode* di MonoBehaviour[cite: 87].
4. [cite_start]**Finite State Machine (FSM)**: Guard terstruktur menggunakan *Interface* `IGuardState` (Idle, Patrol, Suspicious, Alert, Searching)[cite: 81].
5. [cite_start]**Vision Cone & Obstacle Raycast**: Kalkulasi jarak dan sudut pandang, lengkap dengan pengecekan tembok agar AI tidak tembus pandang[cite: 80].
6. [cite_start]**Hearing Mechanic (Opsional)**: AI bereaksi terhadap suara langkah kaki jika player berlari di dalam radius pendengaran[cite: 82].
7. [cite_start]**Last Known Position**: Guard akan mendatangi lokasi terakhir player terlihat/terdengar saat kehilangan jejak[cite: 84].

[cite_start]**Daftar Fitur yang Tidak Sempat Diselesaikan:** [cite: 106]
1. [cite_start]**Komunikasi Antar Guard (Opsional)**[cite: 83].
   - *Alasan*: Saya memprioritaskan stabilitas FSM individu dan navigasi NavMesh agar bebas *bug* (Guard tidak saling tabrak). [cite_start]Mengingat keterbatasan waktu, saya memilih *core system* yang solid daripada menambah fitur komunikasi yang berisiko merusak *state* Guard yang sedang berjalan[cite: 93].
2. [cite_start]**Audio/SFX Layered (Poin Plus)**[cite: 94].
   - [cite_start]*Alasan*: Fokus utama saya adalah memastikan *core gameplay loop* berjalan sempurna[cite: 93]. Mekanik "suara langkah" sudah direpresentasikan secara sistem logika.

## [cite_start]🧠 Penjelasan Sistem AI [cite: 89]

**1. [cite_start]State Machine (FSM):** [cite: 89]
Dikelola oleh `GuardController` sebagai *context*.
- [cite_start]`PatrolState`: Bergerak mengelilingi *waypoints* menggunakan `NavMeshAgent`[cite: 79].
- [cite_start]`IdleState`: Berdiam diri sesaat di setiap titik *waypoint*[cite: 81].
- [cite_start]`AlertState`: Mengejar player secara aktif jika berada di dalam *Vision Cone*[cite: 81].
- [cite_start]`SearchingState`: Bergerak menuju *Last Known Position* jika kehilangan jejak player[cite: 81].
- [cite_start]`SuspiciousState`: Berdiam diri mengamati sekitar sebelum kembali ke patroli jika jejak hilang total[cite: 81].

**2. [cite_start]Kalkulasi Vision:** [cite: 89]
Guard mengecek `Vector3.Distance` ke target. Jika masuk radius, Guard mengecek apakah sudut target berada di dalam batas toleransi menggunakan `Vector3.Angle(transform.forward, dirToPlayer)`. [cite_start]Jika lolos, `Physics.Raycast` ditembakkan untuk memastikan tidak ada objek penghalang (*obstacle*) di antara mereka[cite: 80].

**3. [cite_start]Kalkulasi Hearing:** [cite: 89]
Guard mengecek jarak player. Jika player berada di dalam `hearingRadius`, Guard membaca `velocity.magnitude` dari komponen *Rigidbody* player. [cite_start]Jika kecepatan lebih besar dari kecepatan *sneak* (artinya player berlari/menimbulkan suara), Guard akan mendeteksi *noise* tersebut dan menuju sumber suara[cite: 82].

## [cite_start]🪞 Refleksi [cite: 107]

[cite_start]**Known Bugs / Limitasi:** [cite: 108]
- NavMeshAgent terkadang bergesekan dengan sudut tembok (*cornering*) jika rotasi terlalu tajam.
- Posisi kamera dapat menembus tembok (*clipping*) jika diputar di area sempit karena belum menggunakan *Cinemachine Collider*.

[cite_start]**Hal yang Akan Diperbaiki Jika Ada Waktu Lebih:** [cite: 109]
- [cite_start]Mengimplementasikan sistem *Observer Pattern* (Event System) untuk fitur komunikasi antar Guard[cite: 83].
- Memisahkan visual karakter dari *NavMeshAgent* ke *child object* agar rotasi karakter terlihat lebih mulus saat berbelok.

[cite_start]**Tantangan Terbesar:** [cite: 110]
- Menyesuaikan input pergerakan player agar selalu sinkron dengan arah kamera (*Camera-Relative Movement*).
- Menangani isu fisika dan *pathfinding* ketika menduplikasi Guard, di mana mereka sempat saling mendorong keluar dari NavMesh. Hal ini diselesaikan dengan mengatur setelan *Kinematic* dan memisahkan rute *waypoints*.

## [cite_start]🤖 AI Usage Policy [cite: 15]
[cite_start]Sesuai dengan ketentuan, saya menggunakan **Gemini** (AI Assistant) [cite: 16, 17] selama pengerjaan proyek ini untuk:
- Membantu *debugging error* pada komponen `NavMeshAgent` di Unity.
- Berdiskusi mengenai optimasi struktur *Finite State Machine* di C# agar tidak terjadi *spaghetti code*.
- Mereferensikan rumus matematika vektor untuk pergerakan *camera-relative*.
- [cite_start]AI tidak digunakan untuk men-generate keseluruhan proyek secara otomatis[cite: 18].
