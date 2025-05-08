# EzPooler
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blue)](http://makeapullrequest.com) [![License: MIT](https://img.shields.io/badge/License-MIT-blue)](https://ebukaracer.github.io/ebukaracer/md/LICENSE.html)

**EzPooler** is a Unity package that provides an efficient way to manage object pooling in your game.

 [View in DocFx](https://ebukaracer.github.io/EzPooler)
 
## Features  
- Efficient object pooling to improve performance.  
- Easy-to-use methods for spawning and despawning GameObjects. 
- Support for pre-instantiating and caching GameObjects in the Unity Editor for efficient use during gameplay.

## Installation
_Inside the Unity Editor using the Package Manager:_
- Click the **(+)** button in the Package Manager and select **"Add package from Git URL"** (requires Unity 2019.4 or later).
-  Paste the Git URL of this package into the input box:  https://github.com/ebukaracer/EzPooler.git#upm
-  Click **Add** to install the package.
-  If your project uses **Assembly Definitions**, make sure to add a reference to this package under **Assembly Definition References**. 
    - For more help, see [this guide](https://ebukaracer.github.io/ebukaracer/md/SETUPGUIDE.html).

## Quick Usage
Attach `PoolManager.cs` to the GameObject that will be responsible for spawning objects, then in you `ExampleUsage.cs` script:
```csharp
using Racer.EzPooler.Core;
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private PoolManager _poolManager;

    private void Start()
    {    
	    // Assuming this script is sitting on the gameobject
        _poolManager = GetComponent<PoolManager>();

        // Spawn an object
        var spawnedObject = _poolManager.SpawnObject(Vector3.zero, Quaternion.identity);

        // Despawn the object after 2 seconds
        spawnedObject.InvokeDespawn(2f);
    }
}
```

---
### Example Use Case
Suppose you intend to spawn objects like missiles, barrels, crates, and bottles. You can organize your setup by creating separate GameObjects to hold the respective spawner scripts:

- `MissileSpawner` → `MissileSpawner.cs`
- `BarrelSpawner` → `BarrelSpawner.cs`
- `CrateSpawner` → `CrateSpawner.cs`
- `BottleSpawner` → `BottleSpawner.cs`

Attach the `PoolManager.cs` script to each of these GameObjects. Then, implement the specific spawning logic within each corresponding script, as shown above.

## Samples and Best Practices
- Optionally import this package's demo from the package manager's `Samples` tab.
- To remove this package completely(leaving no trace), navigate to: `Racer > EzPooler > Remove package`

## [Contributing](https://ebukaracer.github.io/ebukaracer/md/CONTRIBUTING.html) 
Contributions are welcome! Please open an issue or submit a pull request.