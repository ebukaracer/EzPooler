# EzPooler
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blue)](http://makeapullrequest.com) [![License: MIT](https://img.shields.io/badge/License-MIT-blue)](https://ebukaracer.github.io/ebukaracer/md/LICENSE.html)

**EzPooler** is a Unity package that provides an efficient way to manage object pooling in your game.

 [View in DocFx](https://ebukaracer.github.io/EzPooler)
 
## Features  
- Efficient object pooling to improve performance.  
- Easy-to-use methods for spawning and despawning gameobjects.  
- Support for pre-instantiating and caching gameobjects in the Unity Editor for efficient use during gameplay.

## Installation
 *In unity editor inside package manager:*
- Hit `(+)`, choose `Add package from Git URL`(Unity 2019.4+)
- Paste the `URL` for this package inside the box: https://github.com/ebukaracer/EzPooler.git#upm
- Hit `Add`
- If you're using assembly definition in your project, be sure to add this package's reference under: `Assembly Definition References` or check out [this](https://ebukaracer.github.io/ebukaracer/md/SETUPGUIDE.html)

## Quick Usage
```csharp
using Racer.EzPooler.Core;
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private PoolManager _poolManager;

    private void Start()
    {
        _poolManager = GetComponent<PoolManager>();

        // Spawn an object
        var spawnedObject = _poolManager.SpawnObject(Vector3.zero, Quaternion.identity);

        // Despawn the object after 2 seconds
        spawnedObject.InvokeDespawn(2f);
    }
}
```

## Samples and Best Practices
Optionally import this package's demo from the package manager's `Samples` tab.

To remove this package completely(leaving no trace), navigate to: `Racer > EzPooler > Remove package`

## [Contributing](https://ebukaracer.github.io/ebukaracer/md/CONTRIBUTING.html) 
Contributions are welcome! Please open an issue or submit a pull request.