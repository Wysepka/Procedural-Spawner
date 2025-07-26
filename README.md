# Procedural Spawner - Unity Grid & Terrain Generation

A modular Unity system for procedural grid and terrain generation, featuring customizable noise, mesh combining, and runtime configuration. This project is designed for extensibility, performance, and easy integration into larger Unity projects.

---

## 🎥 Demo Video

Watch a quick demo of the Procedural Spawner in action (starts at 5:00, ends at 5:30):  
[Procedural Spawner Demo (YouTube)](https://www.youtube.com/watch?v=lTo-A8Z9pzo&t=300s)

---

## 🏗️ Project Structure

```
wysepka-procedural-spawner/
├── Assets/
│   └── Scripts/
│       ├── Camera/
│       │   └── CameraMovement.cs
│       ├── Grid/
│       │   ├── Grid.cs
│       │   ├── GridCube.cs
│       │   ├── GridCubeMeshHandler.cs
│       │   ├── GridCubeSpawner.cs
│       │   ├── GridDataHandler.cs
│       │   ├── GridEventHandler.cs
│       │   ├── GridTransformHandler.cs
│       ├── Helper/
│       │   ├── ColorToTextureHelper.cs
│       │   └── MouseHelper.cs
│       ├── Initializers/
│       │   ├── ClassInitializer.cs
│       │   └── EnumInitializer.cs
│       ├── Input/
│       │   ├── PlayerEventHandler.cs
│       │   └── PlayerInputHandler.cs
│       ├── Noise/
│       │   └── NoiseSampler.cs
│       ├── Referencer/
│       │   └── SingletonReferencer.cs
│       ├── ScriptableObjects/
│       │   ├── GridScriptableObjects/
│       │   │   └── GridSettings.cs
│       │   └── ScriptableObjects/
│       │       ├── GridMasterManager.cs
│       │       └── ScriptableObjectSingleton.cs
│       ├── TestScripts/
│       │   ├── LevelUnlocker.cs
│       │   └── NextLevelUpdater.cs
│       └── UI/
│           └── UIVariablesHandler.cs
└── README.md
```

---

## ✨ Features

- **Procedural Grid Generation**: Create grids of any size with customizable noise for terrain variation.
- **Mesh Combining**: Optimize performance by combining meshes at the cube, face, or grid level.
- **Noise Sampling**: Uses Perlin noise for smooth, natural terrain features.
- **Runtime Configuration**: Adjust grid size, noise scale, and cube dimensions via UI sliders.
- **Color Customization**: Change grid colors using a dropdown menu.
- **Camera Auto-Positioning**: Camera automatically frames the generated grid.
- **Event-Driven Architecture**: Decoupled systems for grid creation, input, and UI.
- **Object Pooling & Optimization**: Efficient memory and performance management.
- **Testing Utilities**: Scripts for level unlocking and progression.

---

## 🧩 Core Systems

### Grid System
- **Grid.cs**: Manages grid data, generation, and destruction.
- **GridCube.cs**: Represents individual cubes, handles mesh and face creation.
- **GridCubeMeshHandler.cs**: Generates and combines cube face meshes.
- **GridCubeSpawner.cs**: Instantiates grid cubes based on noise.
- **GridTransformHandler.cs**: Positions cubes and handles grid rotation.

### Noise System
- **NoiseSampler.cs**: Generates Perlin noise arrays and cube vertex offsets.

### UI & Input
- **UIVariablesHandler.cs**: Connects UI sliders/dropdowns to grid parameters.
- **PlayerInputHandler.cs**: Handles mouse input for interaction.

### Camera
- **CameraMovement.cs**: Adjusts camera position and rotation to fit the grid.

### ScriptableObjects & Singleton
- **GridSettings.cs**: Stores grid parameters and color presets.
- **GridMasterManager.cs**: Singleton access to settings.

---

## 🛠️ Usage

### Requirements
- Unity 2021.3 or later
- .NET Framework 4.7.1
- MEC (More Effective Coroutines) package

### Setup
1. Clone the repository.
2. Open the project in Unity.
3. Import required packages (MEC, etc.).
4. Open the main scene and use the UI to configure grid parameters.
5. Click the "Spawn Grid" button to generate a new procedural grid.

---

## ⚙️ Customization
- **Grid Size**: Adjust via UI sliders or in `GridSettings` ScriptableObject.
- **Noise Scale**: Change for more/less terrain variation.
- **Cube Dimensions**: Set X, Y, Z size for each cube.
- **Colors**: Use the dropdown to pick from predefined color presets.
- **Mesh Combining**: Choose between per-face, per-cube, or per-grid combining for performance tuning.

---

## 🧪 Testing & Extensibility
- **LevelUnlocker.cs** and **NextLevelUpdater.cs**: Example scripts for level progression.
- **Modular Design**: Add new features or systems by extending existing classes or adding new modules.

---

## 📄 License
This project is part of the TripleEspresso development portfolio. All rights reserved.

---

*Procedural Spawner enables rapid prototyping and efficient runtime generation of complex grid-based terrains in Unity. Ideal for games, simulations, and creative tools.* 
