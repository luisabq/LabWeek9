# Carnival Target Shooting - Save & Load

**Author:** Luisa Brizolim Quesada (Solo Project)  
**Assignment:** Lab Submission 10 (Week 10), UCF, Fall 2026

This is a simple 2D carnival shooting game made in Unity. The player can move left and right along the bottom of the screen and shoot at moving targets to earn points before the 60-second timer runs out.

For Week 9, I implemented three design patterns: Object Pool, Builder, and Observer. For Week 10, I added a Save & Load system that allows the player to save their score and the positions of the objects in the game.

**Gameplay Video:** https://youtu.be/KGUpQ6i9i08?si=q-ooMB3UO-MTwbyW

## How to Run

1. Open the project in Unity.
2. If starting with a new project, copy the Assets folder into it.
3. Open a scene and create an empty GameObject.
4. Add the GameBootstrap script to the GameObject.
5. Press Play.

**Controls:**
- A/D or Left/Right Arrows - Move
- Space - Shoot
- S - Save
- L - Load
- R - Restart after the timer runs out

## Save & Load (Week 10)

For this week, I added a system that saves the player's progress and allows them to load it back into the game.

The game saves two separate files:

- **score.dat:** A binary file that stores the player's score and total hits.
- **locations.json:** A JSON file that stores the player's position and the positions of all the targets currently on screen.

I didn't include bullets in the save system since the assignment doesn't require them. I also didn't need to save scenes because the game only uses one scene.

Both files are saved using Unity's `Application.persistentDataPath`. The location of the files is printed in the Console whenever the player saves.

### Files Used

| File | Purpose |
|---|---|
| ISaveable.cs | Interface used for objects that need to be saved and loaded. |
| TransformSaver.cs | Saves and restores the position, rotation, and scale of the player and targets. It also saves the type and movement direction of each target. |
| SaveRecord.cs | Stores the information for each saved object. |
| SavingService.cs | Handles saving and loading the JSON file using JsonUtility. |
| ScoreSaver.cs | Saves and loads the player's score and hits using a binary file. |
| SaveLoadManager.cs | Handles the S and L inputs and connects the save/load systems. |

I used Unity's built-in `JsonUtility` instead of LitJSON since the assignment didn't require LitJSON. For the binary file, I used `BinaryWriter` and `BinaryReader` instead of `BinaryFormatter` because it is outdated.

### How Saving Works

When the player presses S:

1. SaveLoadManager finds all objects with a TransformSaver component, including the player and targets.
2. SavingService collects their information and saves it to `locations.json`.
3. ScoreSaver saves the current score and hit count to `score.dat`.

### How Loading Works

When the player presses L:

1. The game checks if the save files exist. If they don't, it displays a "No save found" message.
2. The targets currently on screen are removed.
3. The player is moved back to their saved position.
4. The saved targets are recreated using the Builder pattern, and their positions and movement directions are restored.
5. The score and hit count are restored.

I also made it so the player can't save after the round has ended.

## Pattern 1: Object Pool

**Files:** BulletPool.cs, Bullet.cs, Shooter.cs

For the Object Pool pattern, I wanted to avoid constantly creating and destroying bullets every time the player shoots.

Instead, the BulletPool creates 8 bullets when the game starts and keeps them in a queue.

When the player shoots, a bullet is taken from the pool and activated. Once it hits a target or leaves the screen, it gets deactivated and returned to the pool so it can be used again.

If all 8 bullets are being used, the player has to wait until one becomes available.

I also added a counter to the HUD so the player can see how many bullets are available and how many are currently being used.

## Pattern 2: Builder

**Files:** TargetBuilder.cs, TargetDirector.cs, TargetSpawner.cs, Target.cs

I used the Builder pattern to create different types of targets without having to manually set up every property each time.

Each target has its own speed, size, and point value.

| Target | Speed | Size | Points |
|---|---|---|---|
| Duck | 3 | 0.9 | 10 |
| Fast Bird | 6 | 0.6 | 30 |
| Bear | 1.2 | 1.5 | 5 |

TargetBuilder handles the properties of each target, including its sprite and color. TargetDirector has the settings for each target type, and TargetSpawner uses those settings when spawning targets.

The spawn chances are:

- Duck: 50%
- Fast Bird: 30%
- Bear: 20%

I also reused this pattern for the Save & Load system. When loading a save, the game uses TargetDirector to recreate the targets before restoring their saved positions.

## Pattern 3: Observer

**Files:** Target.cs, ScoreManager.cs

I used the Observer pattern to handle scoring without having the targets directly communicate with ScoreManager.

When a bullet hits a target, the target triggers an `OnTargetHit` event and sends its point value.

ScoreManager listens for that event and updates the score whenever a target is hit.

The bullet then returns to the Object Pool, and the target is destroyed.

This keeps the scoring system separate from the targets and makes it easier to change either one without affecting the other.

## Other Files

- **GameBootstrap.cs:** Sets up the game and starts the 60-second round.
- **GameHUD.cs:** Displays the score, timer, bullet counts, and save/load messages.
- **GameInput.cs:** Handles player inputs and supports both Unity's old and new Input Systems.
- **SpriteFactory.cs:** Creates placeholder shapes if no sprite is assigned.

## Credits

Sprites used in this project are from [Kenney](https://kenney.nl/assets), released under the CC0 license.
