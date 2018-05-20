## Global scoreboards

#function lastblockorg:dimension_detection
function lastblockorg:soulsand_spawn_vex
function lastblockorg:magmablock_spawn_magmacube
# recipe give @s *

# execute as @e[type=skeleton,tag=smart_skeleton] if entity @e[type=player,distance=5..] run data merge entity @s {HandItems:[{id:"minecraft:bow",Count:1b},{}]}
# execute as @e[type=skeleton,tag=smart_skeleton] if entity @e[type=player,distance=..5] run data merge entity @s {HandItems:[{id:"minecraft:golden_sword",Count:1b,tag:{ench:[{id:10,lvl:1},{id:16,lvl:1}]}},{}],HandDropChances:[0.1F,0.85F]}

# Needed, do not delete. This should be the very last thing to execute
advancement revoke @s only lastblockorg:function_loader
##