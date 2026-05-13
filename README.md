# Stealth AI Sandbox - Ariverse Internship Test
**Kandidat:** Rega
**Posisi:** Game Programmer Intern

## 🎮 Informasi Game

**Kontrol Player & UI Navigation:**
- **W, A, S, D**: Bergerak (Camera-Relative Movement).
- **Hold L-Shift + W/A/S/D**: Mengendap-endap (Sneak Mode) - mengurangi kecepatan dan suara langkah.
- **Hold Right Click + Mouse Move**: Memutar sudut pandang kamera.
- **Tombol 'R'**: Restart level saat layar Game Over / Game Win muncul.
- **Tombol 'ESC'**: Keluar dari aplikasi.

**Kondisi Menang & Kalah:**
- **Win Condition**: Player berhasil mencapai *Finish Zone* di ujung map tanpa terdeteksi.
- **Lose Condition**: Guard berhasil mendekat dan menangkap player (jarak < 0.5f), memicu state "Caught".

**Daftar Fitur yang Berhasil Diimplementasikan:**
1. **Camera-Relative Movement**: Pergerakan player menyesuaikan arah pandangan kamera secara dinamis.
2. **Sneak Mechanic**: Modifikasi *walk speed* saat menahan tombol Shift.
3. **Data-Driven Guard AI**: Penggunaan `ScriptableObject` untuk mengatur parameter Guard tanpa *hardcode* di MonoBehaviour.
4. **Finite State Machine (FSM)**: Guard terstruktur menggunakan *Interface* `IGuardState` (Idle, Patrol, Suspicious, Alert, Searching).
5. **Vision Cone & Obstacle Raycast**: Kalkulasi jarak dan sudut pandang, lengkap dengan pengecekan tembok agar AI tidak tembus pandang.
6. **Hearing Mechanic (Opsional)**: AI bereaksi terhadap suara langkah kaki jika player berlari di dalam radius pendengaran.
7. **Last Known Position**: Guard akan mendatangi lokasi terakhir player terlihat/terdengar saat kehilangan jejak.

**Daftar Fitur yang Tidak Sempat Diselesaikan:**
1. **Komunikasi Antar Guard (Opsional)**.
   - *Alasan*: Saya memprioritaskan stabilitas FSM individu dan navigasi NavMesh agar bebas *bug* (Guard tidak saling tabrak). Mengingat keterbatasan waktu, saya memilih *core system* yang solid daripada menambah fitur komunikasi yang berisiko merusak *state* Guard yang sedang berjalan.
2. **Audio/SFX Layered (Poin Plus)**.
   - *Alasan*: Fokus utama saya adalah memastikan *core gameplay loop* berjalan sempurna. Mekanik "suara langkah" sudah direpresentasikan secara sistem logika.

## 🧠 Penjelasan Sistem AI

**1. State Machine (FSM):**
Dikelola oleh `GuardController` sebagai *context*.
- `PatrolState`: Bergerak mengelilingi *waypoints* menggunakan `NavMeshAgent`.
- `IdleState`: Berdiam diri sesaat di setiap titik *waypoint*.
- `AlertState`: Mengejar player secara aktif jika berada di dalam *Vision Cone*.
- `SearchingState`: Bergerak menuju *Last Known Position* jika kehilangan jejak player.
- `SuspiciousState`: Berdiam diri mengamati sekitar sebelum kembali ke patroli jika jejak hilang total.

**2. Kalkulasi Vision:**
Guard mengecek `Vector3.Distance` ke target. Jika masuk radius, Guard mengecek apakah sudut target berada di dalam batas toleransi menggunakan `Vector3.Angle(transform.forward, dirToPlayer)`. Jika lolos, `Physics.Raycast` ditembakkan untuk memastikan tidak ada objek penghalang (*obstacle*) di antara mereka.

**3. Kalkulasi Hearing:**
Guard mengecek jarak player. Jika player berada di dalam `hearingRadius`, Guard membaca `velocity.magnitude` dari komponen *Rigidbody* player. Jika kecepatan lebih besar dari kecepatan *sneak* (artinya player berlari/menimbulkan suara), Guard akan mendeteksi *noise* tersebut dan menuju sumber suara.

## 🪞 Refleksi

**Known Bugs / Limitasi:**
- NavMeshAgent terkadang bergesekan dengan sudut tembok (*cornering*) jika rotasi terlalu tajam.
- Posisi kamera dapat menembus tembok (*clipping*) jika diputar di area sempit karena belum menggunakan *Cinemachine Collider*.

**Hal yang Akan Diperbaiki Jika Ada Waktu Lebih:**
- Mengimplementasikan sistem *Observer Pattern* (Event System) untuk fitur komunikasi antar Guard.
- Memisahkan visual karakter dari *NavMeshAgent* ke *child object* agar rotasi karakter terlihat lebih mulus saat berbelok.

**Tantangan Terbesar:**
- Menyesuaikan input pergerakan player agar selalu sinkron dengan arah kamera (*Camera-Relative Movement*).
- Menangani isu fisika dan *pathfinding* ketika menduplikasi Guard, di mana mereka sempat saling mendorong keluar dari NavMesh. Hal ini diselesaikan dengan mengatur setelan *Kinematic* dan memisahkan rute *waypoints*.

## 🤖 AI Usage Policy
Sesuai dengan ketentuan, saya menggunakan **Gemini** (AI Assistant) selama pengerjaan proyek ini untuk:
- Membantu *debugging error* pada komponen `NavMeshAgent` di Unity.
- Berdiskusi mengenai optimasi struktur *Finite State Machine* di C# agar tidak terjadi *spaghetti code*.
- Mereferensikan rumus matematika vektor untuk pergerakan *camera-relative*.
- AI tidak digunakan untuk men-generate keseluruhan proyek secara otomatis.
