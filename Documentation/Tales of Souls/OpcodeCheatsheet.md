## Soulcalibur III - Tales of Souls Opcode Documentation

Any absent opcodes are currently unknown. 

<br>

## Event Opcodes

| Opcode | Explanation  | 
| :----: |    :----:    |
| 0x04   | Title of the event. |
| 0x05   | Lore of the event.  |
| 0x06   | Enemy entry. One of each, 5 max. |
| 0x0A   | Declares the stage which the event takes place. |
| 0x11   | End of event. |
| 0x12   | Triggers the specified cutscene. |
| 0x15   | End-Game, triggers the ending. |
| 0x16   | Current position on map. |
| 0x17   | Destination on the map.  |
| 0x19   | Music of the event. |

<br>

## Enemy Opcodes

| Opcode | Explanation  | 
| :----: |    :----:    |
| 0x07   | Universal Character Modifier. |
| 0x08   | Costume Modifier.  |
| 0x09   | Weapon Modifier. |
| 0x1B   | Battle time. Infinite if absent. |
| 0x1D   | Health of the opponent. 0xF0 is 100%. |
| 0x21   | Awarded Gold. Value is multiplied by 50 in-game. |
| 0x23   | Opponent's Strength. 1000 by default. |
| 0x24   | Opponent's Defense. 1000 by default. |
| 0x27   | AI level. Multiple of 0x11. |
