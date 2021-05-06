# FallingBombsTestTask

**Task description**

Make a scene - a closed space in which some characters spawn randomly and bombs that can cause damage fall. The character can take a certain amount of damage, after which he dies.
Walls are randomly placed in the space.
Bombs can have different types, as well as some of them could damage through the walls.

It is not allowed to use ready assets for this task.

**Imlementation**

* Object Pooling, with support for different types of identical objects (different bombs, enemies, no need to change the code - you just need to create a ScriptableObject - a key for the object)
* Simple spawner for bombs and enemies (no collision checks with existing objects)
* Simplest wall generation (no space availability checks)
* Enemies have 100 health points
* 2 types of bombs
  * Small (red) - has an explosion radius of 1.5 meters and damage in the center of 100
  * Large (green) - has an explosion radius of 5 meters and damage in the center of 1000
* Bombs do not damage through walls - this can be controled in the prefab
* For the simplicity of implementation, bombs will not collide with walls
