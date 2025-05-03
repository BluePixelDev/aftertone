# SoundSnap

[![GitHub Repo stars](https://img.shields.io/github/stars/BluePixelDev/sound-snap?style=flat-square)](https://github.com/BluePixelDev/sound-snap/stargazers)
[![GitHub last commit](https://img.shields.io/github/last-commit/BluePixelDev/sound-snap?style=flat-square)](https://github.com/BluePixelDev/sound-snap/commits/main)

**SoundSnap** is a modular and efficient audio playback system for Unity. It avoids the performance cost of instantiating and destroying audio sources by pooling reusable instances, making it ideal for sound effects, UI sounds, ambient audio, and more.

## Features
### ScriptableObject-Based Configuration
Define reusable audio settings and playback parameters through `SnapAsset` objects. These assets support automatic configuration based on existing `AudioSource` components.

### Intuitive Editor Integration
Drag and drop a real `AudioSource` component into a `SnapAsset` to auto-fill all settings. Easily preview and test playback directly in the editor.

### Pooled Audio Source System
Avoid runtime garbage collection and minimize CPU overhead by pooling `AudioSource` instances. No more `GameObject.Instantiate` or `Destroy` calls for quick sounds.

### Configurable Scaling
Adjust initial pool size, maximum limits, and overflow handling using a centralized configuration asset (`SnapConfig`). Fine-tune performance to match your project's needs.

### Scene Awareness
By default, pooled objects are destroyed and recreated with each scene load. Optional settings allow persistent or scene-specific behavior.

### Lightweight & Extensible
SoundSnap is designed to be unobtrusive and modular. Drop it into your project and start using it immediately — or extend it with custom behavior.

## Support
This plugin supports **Unity 6 and above**

## Getting Started
1. Add the `SoundSnap` folder to your Unity project.
2. Configure `SnapConfig` in `Resources`
3. Create one or more `SnapAsset`s to define how each sound should behave.
4. Use `SoundSnap.Play()` in your code, or add the `SnapPlayer` component to your GameObjects.
5. Optional: Add `SnapTrigger3D` or `SnapTrigger2D` to automatically trigger sounds based on collisions or triggers.


## Example
```csharp
using BP.SoundSnap;
...
SoundSnap.Play(mySnapAsset, transform.position);
```

Or, attach a `SnapPlayer` component to a GameObject and call:

```csharp
mySnapPlayer.Play();
```
