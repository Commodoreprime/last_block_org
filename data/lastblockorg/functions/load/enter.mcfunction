function lastblockorg:load/clearscreen
tag @s add wiz_isOpened

execute if entity @s[tag=wiz_isOpened] run function lastblockorg:load/setupui