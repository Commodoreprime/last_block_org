tag @s remove inEnd
tag @s remove inOverworld
tag @s remove inNether

execute as @s[nbt={Dimension:1}] run tag @s add inEnd
execute as @s[nbt={Dimension:0}] run tag @s add inOverworld
execute as @s[nbt={Dimension:-1}] run tag @s add inNether