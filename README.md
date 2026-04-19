# Protocol: OVERRIDE

## 📖 Overview
**Protocol: OVERRIDE** is a fast-paced, top-down sci-fi arena survival game built in Unity 6. You play as Unit 734, a defense mechanism trapped within a highly volatile server environment. Your mission is to hold the line against endless swarms of relentless "Spam Bots" attempting to corrupt the system mainframe. 

The game combines twin-stick style combat with critical resource management. Every shot fired consumes RAM, forcing the player to balance aggressive crowd control with precise resource conservation.

---

## ⚙️ Core Gameplay Mechanics

### Combat & RAM Management
* **Weapon Compiler:** Your primary defense is a laser weapon that rapidly compiles and fires projectiles.
* **Resource Drain:** Firing is not free. Every shot consumes 2 units of **RAM**.
* **Tactical Firing:** The player must maintain an optimal firing rate (up to 8 bolts per second). Blindly firing will quickly deplete your RAM, leaving Unit 734 defenseless against incoming bot waves.

### Enemy Swarms: The Spam Bots
* **Endless Waves:** The Arena Manager continuously spawns Spam Bots using an adaptive spawning algorithm.
* **Behavior:** Spam Bots aggressively track Unit 734, attempting to deal contact damage and inflict "System Corruptions."

### Survival & Data Shards
* **Health System:** Unit 734 possesses a finite health pool. Taking damage risks immediate critical system failure.
* **Data Shard Loot Drops:** Destroying Spam Bots triggers a loot drop system. Bots have a calculated probability of dropping a glowing **Data Shard**.
* **System Repair:** Collecting Data Shards clears corruptions and restores 20 HP, allowing skilled players to sustain their uptime indefinitely. Shards are dynamically animated with a smooth sine-wave hover effect to ensure optimal 2D/3D visual integration.

---

## 🛠️ Technical Systems

### Auto-Reboot Sequence
To maintain an arcade-like flow, the traditional UI "Restart" button was stripped out in favor of an immersive **10-Second Auto-Reboot**. Upon critical failure (reaching 0 HP), the screen locks down, displays a "SYSTEM FAILURE" protocol, and securely reloads the scene using Unity's `Invoke` architecture.

### Global Audio Manager
The game utilizes a custom, persistent `AudioManager` leveraging the Singleton design pattern. 
* **Overlapping Audio:** Powered by `PlayOneShot`, the system allows multiple laser blasts and explosion sounds to trigger simultaneously without cutting each other off.
* **Event-Driven:** Audio cues (Game Start, Game Over, Player Shoot, Enemy Shoot, Enemy Death) are triggered remotely by external scripts (like the `WeaponCompiler` and `EnemyHealth`).

### URP Material Upgrades
The game environment was built using classic Unity Asset Store 3D props. Because the project utilizes Unity 6's **Universal Render Pipeline (URP)**, the legacy materials initially imported as broken (magenta). A complete project-wide rendering pipeline conversion was run to upgrade all metallic, emissive, and diffuse textures to the modern URP standard, ensuring high-fidelity dynamic lighting.

---

## 🎮 Controls
* **Movement:** `W` `A` `S` `D` or Arrow Keys
* **Aiming:** Mouse Cursor
* **Fire Weapon:** `Left Mouse Button` (Hold for rapid fire)

---

## 📜 Credits & Attributions

Developing **Protocol: OVERRIDE** was a massive learning experience. Huge thanks to the following creators and resources that made this project possible:

### 🎨 Visual Effects (VFX)
The advanced particle systems used in the game were heavily inspired by and built using techniques from **Gabriel Aguiar Prod.** on YouTube:
* **Player Spawn Effect:** Based on the tutorial [Unity 5 - Game Effects VFX - Teleport & Spawn](https://youtu.be/iMcGkgP0P-M).
* **Combat Hit Effects:** Based on the tutorial [Unity 5 - Game Effects VFX - Hit Effect](https://youtu.be/50rlfU-rOk8).

### 🔊 Audio & Sound Effects
All sound effects were sourced under a Royalty-Free commercial license from [Pixabay](https://pixabay.com/). 
* Includes all blaster shots, explosion impacts, start-up notifications, and critical failure sirens.

### 🏗️ 3D Models & Environments
Environment assets and level geometry were sourced from the **Unity Asset Store**:
* Includes packages like **"Sci-Fi Props"** and **"Sci-Fi Office Lite"**, which provide the foundational prefabs for the arena floors, animated lights, forcefield walls, and crates.

---

## ⚖️ License
This project is released under the **MIT License**. See the [LICENSE](LICENSE.txt) file in the root directory for more details.
