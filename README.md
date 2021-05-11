# StarGenerator
Custom Unity Tool to generate star presets.
Made for Unity 2020.3.7f1



# How to use the Star Generator
1 - Expand the Tools menu and select Star Generator

2 - A default database will automatically be created in Asset/Databases.
You can create a new database by righclicking in the project view  Create > Stars Database
Or : Assets > Create > Stars Database

3 - Select the database you want to use in the Stars Database field.
(Once you created a custom database and selected it, it will stay selected in the tool)

4 - Input your parameters in the Create New Star Preset section.

5 - Press Create Star Preset, the star will be added to the database and listed in the section below.

6 - You can click on a star to select it, it will now be highlited in addition to the Spawn Selected Star Preset button.

7 - Press the newly highlighted button to spawn the selected star.
The star will spawn at the (0, 0, 0) cordinates.
If you select it, you will see a Wire Sphere gizmo representing it's Gravity Well.

8 - To remove a preset from the database you can press the dark and red X button next to it.




# How to edit an existing preset
1 - Look for your database in the project view and select it.

2 - In the Inspector, you can now edit each preset directly from the Stars Presets list.
You can also reorder the list freely. (The Star Generator will be automatically updated).

3 - You can also remove a preset from the database using the built-in "Remove selection form the list" button.




# How to modify an instantiated preset in the scene
1 - Once a star has been spawned in the scene, you can use the custom inspector "Star Object (Script)" to change the instance properties.
Note that modifying the database won't impact the already instatiated stars / modifying the instanciated star won't edit the database.




# How to Import/Export to JSON
1 - While inspecting a Stars Database, below the Star Presets list in the Inspector, you'll find buttons to Import/Export to JSON.
2 - To Export the database, press Export To JSON button and select the path to creat the JSON file in the dialog, then, press OK.
3 - To Import the data from another database, press the Import Form JSON button and select the previously exported JSON file, then, press OK.
The data will replace the content in the current database with the loaded one.
