Carnival Target Shooting
Luisa Brizolim Quesada
Lab 9 – UCF, Fall 2026
For this lab, I made a simple 2D carnival shooting game in Unity. You move left and right and shoot at targets to get as many points as you can in 60 seconds. The main goal of the lab was to use three design patterns: Object Pool, Builder, and Observer.
Gameplay video: Watch here
How to Run
1. Open the project in Unity (or copy the Assets folder into a new project).
2. In a scene, create an empty GameObject and add the GameBootstrap script.
3. Press Play. Everything else gets set up when the game starts.
Controls: A/D or arrow keys to move, Space to shoot, and R to restart when the timer ends.
Diagram
This diagram shows how the three patterns work in my game.
  

1. Object Pool
Files: BulletPool.cs, Bullet.cs, Shooter.cs
I used Object Pool for the bullets so the game doesn't have to make a new bullet every time you shoot. When the game starts, it creates 8 bullets and keeps them ready to use.
When you press Space, the shooter takes a bullet from the pool. After it hits a target or leaves the screen, it goes back into the pool to be used again. If all 8 bullets are already on screen, you have to wait until one comes back. I also showed the bullet counts on the screen so you can see it working.
2. Builder
Files: TargetBuilder.cs, TargetDirector.cs, TargetSpawner.cs, Target.cs
I used Builder because I wanted a few different types of targets without setting each one up from scratch. The targets have different speeds, sizes, and points.
TargetBuilder puts the target together, and TargetDirector keeps the settings for each type. These are the three targets I used:
Target
	Speed
	Size
	Points
	Duck
	3
	0.9
	10
	Fast Bird
	6
	0.6
	30
	Bear
	1.2
	1.5
	5
	The game randomly spawns ducks 50% of the time, fast birds 30%, and bears 20%. Having the settings in one place makes it easier to change them later. They're all made from the same Target class, just with different settings.
3. Observer
Files: Target.cs, ScoreManager.cs
I used Observer to handle scoring when a target gets hit. Instead of having each target update the score itself, the target sends out an event saying it got hit and how many points it's worth.
ScoreManager listens for that event and adds the points to the score. The bullet goes back into the pool, and the target disappears. I liked this approach because the target doesn't need to know how the scoring system works, and the same event could also be used for sounds or effects later.
Other Scripts
* GameBootstrap.cs – Sets up the game and the 60-second timer.
* GameHUD.cs – Shows the score, timer, and bullet counts.
* GameInput.cs – Handles the controls.
* SpriteFactory.cs – Makes placeholder shapes if a sprite is missing.
Credits
I used sprites from Kenney's Space Shooter Remastered pack (CC0).