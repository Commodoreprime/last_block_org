# Places the processor. An armor stand ment to be generated where spawnchucks are (which are whereever the first player spawns) (it should not need to be spawned again)
# This processor is ment to perform things such as function executions that's not specific to a single player and scoreboard increments since it is constant and does not leave the world (an example would be increments of a tick objective)
# Make sure to use 'as' keyword in execute commands

execute unless entity @e[type=armor_stand,tag=processor] run summon armor_stand ~ 0 ~ {NoGravity:1b,Invulnerable:1b,Small:1b,Invisible:1b,PersistenceRequired:1b,Tags:["processor"]}
execute as @e[type=armor_stand,tag=processor] run setblock ~ ~1 ~ minecraft:bedrock replace