#  VR 1 Final Project

## Project Report

This is a magic sandbox game with a flexible spellcrafting system inspired by Noita.
Spells can be combined and arranged in unique ways to produce different results.

The main skill that I focused on in this project was programming, but I also learned a lot about
3D modeling and UV mapping with Blender. Most of the models in this game are made by me. While the models
themselves are fairly basic, I enjoyed making them and I have greater confidence in my abilities to create
models in the future.

The biggest challenge I faced during this project was the architecture of the spell system. Specifically, I had two major problems:
linking spell data with spell code, and the implementation of spell functionality.
I wanted to add many different spells to the game, so the system needed to be extensible and flexible.

To solve the first problem, I used scriptable objects to store data that is common to all spells, such as a name, icon, description,
and most importantly, a reference to another scriptable object that contains the code for that spell.

I solved the second problem by separating of spell grouping and spell casting in the codebase. To explain this effectively,
let me quickly explain the spell pipeline. We start with the raw list of spells on the wand, which needs to be broken up
into a number of groups. Each group then needs to be "compiled" into a list of projectiles and a list of modifiers that act upon those projectiles.
Finally, when the wand is cast, a group needs to instantiate those projectiles properly and apply the modifiers.

To do this, I created a Spell Factory system in which groups don't actually store a list of projectiles themselves, but instead
a list of factory objects that are capable of instantiating projectiles. Spell Factories are also scriptable objects, and they take
great advantage of C#'s inheritance features. As such, many spells are able to share a common Spell Factory with different parameters.
For example, the tuple spells (Double, Triple, Quadruple) are all instances of the MulticastFactory class. This is not only great for
avoiding code duplication, but it also makes it very easy to add new spells without writing any extra code.

I could go into **a lot** more detail regarding this system and its implementation, but I'll spare you since I've already written 4 paragraphs.

## Sources

### Materials

- https://freepbr.com/product/light-gold-pbr-metal-material/
- https://freepbr.com/product/rough-rockface-3-pbr-material/
- https://freepbr.com/product/forest-floor1/
- https://freepbr.com/product/dark-rough-rock1/
- https://polyhaven.com/a/farmland_overcast
- https://polyhaven.com/a/velour_velvet
- https://polyhaven.com/a/wood_table_worn
- https://freepbr.com/product/cloudy-veined-quartz/
- https://freepbr.com/product/obsidian-pbr-material/
- https://polyhaven.com/a/wood_planks
- https://polyhaven.com/a/rough_wood

### Unity Asset Store

- https://assetstore.unity.com/packages/p/magic-effects-free-247933
- https://assetstore.unity.com/packages/p/particle-pack-127325
- https://assetstore.unity.com/packages/3d/props/weapons/3d-items-free-wand-pack-46225
- https://assetstore.unity.com/packages/p/quick-outline-115488

### Models

- https://poly.pizza/m/vlVx279xut
