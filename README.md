# An Old Ninja

The project must be a 2D storytelling action platformer. It appeared to be too ambitious and complicated.
Why?
* It uses A* algorithm with a Points Graph to make AI able to move and jump across any platform.
* It has dialogues and triggers for them.
* It has events (like creating a mobs spawner at some point of a level).
* It has checkpoints without reloading the scene.
* The state of the level changes with level progression.
* For example, to revive a player at the last checkpoint I have to reset or disable things.
Because of all of these, it became hard to manage these features.  

This is the reason why it has only the intro level. To implement more levels uncountable amount of time would be needed.  
I wanted to make dark dungeon-like levels with fancy post-processing light.  

The game has 4 states: Start menu, play, dialogue, death. And a scene with 'to be continued'.
