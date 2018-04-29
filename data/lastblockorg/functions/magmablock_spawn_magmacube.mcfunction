execute if entity @a[scores={mine.MagmaBlock=9}] run summon magma_cube ^ ^1 ^2 {Size:1}
execute if entity @a[scores={mine.MagmaBlock=9}] run scoreboard players set @s mine.MagmaBlock 0

## Scoreboard objectives
# /scoreboard objectives add mine.MagmaBlock minecraft.mined:minecraft.magma_block