# Endless Enemies
A top down shooter made using Unity in C#, where the player kills the enemies spawning around him to make it to the end to finally meet his demise.

To view the scripts for this game, click [here](https://github.com/sgarg15/The_Shooter/tree/master/Assets/Scripts)

The main audio is handled using the following files: <br />
[Audio Manager](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/AudioManager.cs)<br />
[Music Manager](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/MusicManager.cs)<br />
[Sound Library](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/SoundLibrary.cs)<br />
<br />
Guns and pickup items are handled using the following files:<br />
[Cross Hairs](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/CrossHairs.cs)<br />
[Grenade](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/GrenadeScript.cs)<br />
[Guns](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Gun.cs)<br />
[Gun Controller](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/GunController.cs)<br />
[Muzzle Flash](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/MuzzleFlash.cs)<br />
[Bullets](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Projectile.cs)<br />
[Shells](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Shell.cs)<br />
[Pick Up Items](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/PickUpSpawner.cs)<br />
<br />
Enemy and Player control is handled using the following files: <br />
[Enemy Control](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Enemy.cs)<br />
[Enemy Spawner](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Spawner.cs)<br />
  - The enemy spawner at its core gets any open tile (tile where there isn't a block) and pursues the player using the shortest path. It also has the capabilites to check whether the player is camping and if the player is, it spawns the enemies at the tile where the player is standing to encourage movement.

[Life handler](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/IDamageable.cs)<br />
[Living Entity](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/LivingEntity.cs)<br />
[Player Control](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Player.cs)<br />
[Player Control Functions](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/PlayerControl.cs)<br />
[Player Follower](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/playerFollower.cs)<br />
<br />
All the UI is handled using the following files:<br />
[In-Game UI](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/GameUI.cs)<br />
[Pause Menu](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/PauseMenu.cs)<br />
[Score Keeper](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/ScoreKeeper.cs)<br />

Finally, the Map Generation (the hardest part of this project) is handled by the following files:<br />
[Map Generation](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/MapGenerator.cs)<br />
[Utilities](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/Utility.cs)<br />
  - The way Map Generation works is during the game development stage, different pre-determined levels are created using the map generation script. Within the Unity ui, we are able to change different properties of the map such as, the size of the map, the density of the obstacles as well as the color of the map. 
  - The Map Generation script also consists of various helper functions such as `CoordToPosition`, `GetTileFromPosition`, `GetRandomCoord`, `GetRandomOpenTile`. These functions are used through out the program, to help the enemies and the user move around the map.
  - Additionally, the script also assures that every part of the map is full accessible. This works by the alogrithm going through each tile and checking its neighbours and assuring they lead to a path which is connected to other paths. Making sure that there aren't any isolated paths. This is done by this script here:
```c#
bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount) {
    bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
    Queue<Coord> queue = new Queue<Coord>();
    queue.Enqueue(currentMap.mapCenter);
    mapFlags[currentMap.mapCenter.x, currentMap.mapCenter.y] = true;

    int accessibleTileCount = 1;

    while (queue.Count > 0) {
        Coord tile = queue.Dequeue();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                int neighbourX = tile.x + x;
                int neighbourY = tile.y + y;
                if (x == 0 || y == 0) {
                    if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1)) {
                        if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY]) {
                            mapFlags[neighbourX, neighbourY] = true;
                            queue.Enqueue(new Coord(neighbourX, neighbourY));
                            accessibleTileCount++;
                        }
                    }
                }
            }
        }
    }

    int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
    return targetAccessibleTileCount == accessibleTileCount;
}
```
Visit Map is accessible algorithm [here](https://github.com/sgarg15/The_Shooter/blob/master/Assets/Scripts/MapGenerator.cs#L128).


# Give this game a try here:
https://www.dropbox.com/sh/61lio2yct258qir/AABnxZMSVWKQqM8x8dq4WSj0a?dl=1

# Credits
Big thanks to [Sebastian Lague](https://www.youtube.com/user/Cercopithecan) for his tutorials on Unity and game development. This game is based on his some of his tutorials with some modifications made by me.
