# 🎵 OneShotSFX
**OneShotSFX** is a Unity plugin designed for efficient audio source management. Instead of repeatedly creating and destroying temporary audio sources (e.g., for SFX, ambient sounds, UI interactions), Wave Pool reuses pooled sources — saving performance and making audio playback seamless.

# ⚡Features
- 🎛️ **ScriptableObject-Based Configuration** <br>
  Define audio behavior through reusable, configurable assets.
- 🎚️ **Drag & Drop Configuration** <br>
  Drop a real AudioSource into the asset inspector — auto-configures settings for you.

- 🌀 **Pooled Audio Resource System** <br>
Instantiates and reuses audio sources with zero garbage generation.

- 📈 **Dynamic Scaling** <br>
  Configure how many sources are created initially, max pool size, and how many to add when    more are needed.

- 🔄 **Scene-Aware Behavior** <br>
  Audio sources are non-persistent by default and get flushed on scene load — configurable     in a separate PoolConfig asset.

- 🎲 **Advanced Randomization** <br>
  Supports Unity’s new Audio Resources system:
    Random pitch, volume, and audio clip selection.

- 🧩 **Extensible & Lightweight** <br>
  Easily drop into any project and start using with minimal setup.
