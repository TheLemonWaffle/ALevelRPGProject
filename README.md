# ALevelRPGProject
Was an A Level AQA Computer science project back in 2019-2020
The idea was to create a console based RPG game to encompass majority of the skills i had learn during the course. Although, I missed out certain OOP concepts like overloading/overriding and aggregation.

The world were 10x10 grids where the player could move around a 'P' as the player. Additionally, they could walk onto tiles with a 'E' as enemies to fight enemies, 'I' as Inn to visit a smaller 10x5 INN with a merchant('M') and NPC questgiver('N') and 'D' as dungeon which would put the player into a dungeon where the quest would take place. 
The Player would kill enemies, kill bosses and interact with 'C' on the grid which would let them loot chests to get potions, money and skill points.

I Implemented a stack to keep track of the previous worlds people had visited (allowed to back track through worlds and place players in the right points upon Exitting/Entering worlds).

The project also had a saving system which used the System.Data.OleDb database library and the ADOX Catalog class to create a database which stored my user data. I planned out the database beforehand to make sure it was up to 3rd normal form which was the highest we'd been taught at the time. All SQL statements are custom made for my database structure.
