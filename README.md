# Connect4Basketball

Packages Installed:

Soccer Ball: https://assetstore.unity.com/packages/3d/low-polygon-soccer-ball-84382

Environmental Assets :
    https://assetstore.unity.com/packages/3d/vegetation/trees/realistic-tree-9-rainbow-tree-54622
    https://assetstore.unity.com/packages/2d/textures-materials/nature/nature-materials-vol-1-21113

SkyBoxes: https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-skyboxes-pack-175525



# Sprint 1


**ROLES**

Ethan: Learn Unity Networking, Join/Start Room screen

Hannah: GitHub Manager, Start Screen, person model, collision detection

Will: Building the Connect4Board, Practice Mode, basic environment

John: Video Editor, Project Manager, Start Screen

Ben: GitHub Manager, PowerUps, Stats Screen, Make person model

# Sprint 2

[Sprint 2 Demo Video](https://youtu.be/PUdOWqQNR1M)

During Sprint 2, we focused on 3 main goals. We wanted to successfully add a basic form of multiplayer interation, create the game enviorment including the board, and create the start UI screen. In general, we were able to acomplish these goals. 

## List of Changes:
Created two new Scenes:
1. GameEnv
2. MainMenu

Game Env:
1. Created the Enviroment
2. Created the Board
3. Created a basketball to throw

Main Menu:
1. Created a Canvas with start screen buttons
2. Created Start Menu Background

Other:
1. Added basic networking through normcore
2. Added Player Collision Detection in the XR Origin
3. Added Locomotion system in the XR Origin

Ethan:
I was focused on adding networking and implemting player movement. There is now functionality to move with the left joystick and the player has collision detection with the enviorment. In addition, networking has been added to the ball and the enviroment, so there is basic multiplayer functionality. In order to do this, some scripting was required. 

Hannah:
I worked on the general environment of the game. This included improvements to the skybox, the ground the user stands on and its appearance, and the enviornmental objects like the trees. These enviornmenal objects work with Ethan's collision detection to behave like real environmental objects would.

Will:
I worked on the first version of the game board.  This included placing and sizing the "hoops" in a fashion that is consistent with connect 4 boards traditionally.  I also implemented a transparent front panel on the board in a manner that allows the player to see the ball move down the column. Next sprint I will look into using Blender to create a custom front panel with clear "holes" consistent with connect 4 boards traditionally.

Ben: I created the welcome screen main menu. There is now a world space with three buttons and a colored design with rotating objects. The first button, "New Game", takes you to our environment scene where we will eventually have a game mode. Also included are "Practice Mode" and "Quit Game" which will still need to be built out. The button logic is found in the MainMenu script.


# Sprint 3

Overview:
Becuase fall break was in the middle of sprint 3, almost all of us are still working on our own contributions. As a result, we have not decided to merge any branches because we want to make sure that each part is finished before we merge. 

Video: Since we have not merged, we have two seperate videos based off the work Hannah and Ben have done. See below for both of them. 

### List of Changes
1. Improving the Board and game functionality
2. Creating new UI menus
3. Creating pause funtionality
4. Starting to implement networking

### Location for Changes:

Board: hannah-objectDetection branch in the GameEnv Scene

Networking: ethan-network branch in the GameLobby Scene. The code is in the Assests/Scripts/Networking folder. It is linked to the Network Manager and Network Player game objects

Pause Screen: Will-PauseMenu branch in the GameEnv scene

Main Menu: Ben-MainMenu branch in the MainMenu Scene

### Individual Contributions

Hannah:
I worked to improve the board that the user will see. I created a series of "lights" that will represent the holes in the connect4 board that is seen in the board game version. I created a script that enables the lights to take on the material of the object that is thrown through the hoop. This gives the same effect as shooting the ball throught the hoop and watching it fall down the tube, but will make it easier for us to "read" when a player has successfully connected four. The "reading" of the connecting 4 will be done next sprint, but with the current implementation, players can shoot the red cube and the soccerball and watch the board fill in as the lights take on those materials. For now, this is staying on the brach hannah-objectDetection until next sprint when I can "read" the board, then all these board changes will be merged to master at once. Video: [Hannah Sprint 3 board demo video](https://drive.google.com/file/d/1GWrpCUP90t0tP0tmZYo40i6mPDE08K0C/view?usp=sharing)

Ben:
I integrated the home screen I had previously created into VR since it was not working in VR before. The world space now appears as the cube and menu which do not follow the headset. I fixed the controller interactions so that both are now able to click the "New Game" button and direct players to the game screen. Additionally, the main menu is now the default scene. For the next sprint, I will be integrating networking into the screen so that clicking "New Game" will take players to a lobby where they can enter a game code to join their friends. For now, this work in on the branch Ben-MainMenu.
Video: [Ben Sprint 3 Main Menu Vid](https://drive.google.com/file/d/13fSIdF5KsXIM0lBKBLEm4GI4YBszA6WO/view?usp=sharing)

Will:
I created a Pause Menu by adding a canvas to the environment.  Inside the canvas object, I used a panel to implement the menu.  I included a resume button that functions through a C# script.  This will enable the user to completely pause the game (time scales to 0) by pressing the escape button and resuming the game by pressing the prompted resume button.  For the next sprint, I will work on adding more button to the pause menu, and or work on the logic behind the board's winning and losing states.  The pause menu will also need to be disabled during multiplayer modes in future sprints.  

Ethan: 
I starting working on networking through Unity. I spent a while researching and testing a few things with norcore, but realized that it would not have enough funtionality for what I wanted to do. After being introduced to Photon, I have been spending time implementing that version of networking. We are successfully able to join a lobby and a room, as well as create new rooms and lobbies while running. I have also begun to create a basic avatar because we can't use the normcore one anymore using different packages. For the net sprint, I will continue to work on networking and build out full lobby create and join by code in game functionality. I will also work to integrate networking in all other parts of the project. 

# Sprint 4

[Sprint 4 Demo Video](https://youtu.be/9MT4h-moFdU)

Overview:
This sprint involved improving our progress from the last sprint, including implementing game features and more robust networking. We also started to fix some of the physics of the game, including how to throw the balls. 

### List of Changes:
1. Redesigned main menu of game (note: the old menu, MainMenu.unity, still exists for now but will be deleted)
2. Developed practice mode
3. Developed game lobby to input game code
4. Created alternate game environment
5. Improved Connect4Board
6. GAME OVER message displayed with win
7. Completed Basic Networking with the ability to join 3 different rooms
8. Created the ability to detect when someone makes a connect4 and wins the game


### Location for Changes:
Hannah's changes have all been merged into the main branch. New work was done on the board prefab (Assets/Prefabs) we created and the ReactionScript.cs (Assets/Scripts).

Ben's changes have all been merged into the main branch. New work for this sprint includes NewMainMenu.unity, PracticeMode.unity, NewGameLobby.unity, and SoccerFieldEnv.unity (all found in Assets/Scenes/).

Ethan's changes are still in the ethan-network branch, as they are not completly finished. Most of the work is in the Game Lobby scene and consists of making the network player pre-fab and the implementaion of the NetworkManager, NetworkPlayerSpawner, and LobbyMenu Scripts in Assets/Scripts/Netowrking. There are also new networking components added to various objects, including the SoccerBall in the GameEnv Scene. 

Will's changes are in the Will-Test Branch, since they are not entirely finished.  New work has been tested including updated ball physics and shooting mechanics.  Spontaneous generation of basketballs has also been tested.

### Individual Contributions:

Hannah:
I finished up a lot of the work on the board that continued over from last sprint. I improved the look of the board including making the "detectors" under the hoops transparent so the players won't see them, and shortening the dividers between the columns so that it is easier to see the whole board all at once. I added to the ReactionScript that I created last sprint so that we can now detect when a player gets a connect4. I initially used the light in the upper left hand corner of the board to change colors as an indicator of connect4, but by the end of the sprint I added a canvas with text that reads "GAME OVER" to pop up on screen when a player gets connect4. 

Ben:
In this sprint, I redesigned our game. Beginning with the main menu, I got rid of the old spinning cube world space in favor of a new opening in the middle of a basketball court (basketball arena was bought from Sketchfab). As of now, the three options from the main menu are 'New Game', 'Practice Mode', and 'Quit Game'. I made it so that 'New Game' takes you go a new screen with an input field where users will be able to type in a game code to join a friend (still need to implement keyboard). From there, they can hit "Go" to be taken to a game environment. I also implemented a practice mode which takes place in the arena. 'Quit Game' closes the application. Finally, I made an extra game environment which includes a soccer field in an outer-space skybox.

Will:
In this sprint I investigated possible methods of making the user experience more realistic with regards to shooting the basketball.  Currently, the default throwing mechanics make it difficult to reach the game board.  Different methods have been investigated in the test branch and will be finished and merged by next sprint.  Some techniques include a script for applying velocity to the ball once it crosses a plane that will follow the player on their shoulder as well as possibly including a script that will add vertical velocity to the ball upon release from the user's hand.  Goals for next sprint include determining whether or not spontaneous generation of balls will be feasible and merging final physics scripts into the current main branch.

Ethan:
I focused on on implementing a basic form of networking. Now, when players join the scene, they are put in a lobby where they can see a list of 3 rooms. When they join those rooms, they are reloaded into the GameEnv scene and can see the players that are with them. I also created a net NetworkPlayer pre-fab which is an avatar which consists of 2 hands that are animated and networked. The players can also leave their scene and go back to the lobby where they can rejoin the room they want. Future work will involve integrating this with the main branch and the connect4 game mechanics.


# Sprint 5
[Sprint 5 Demo Video](https://youtu.be/e547sY4rVKk)

Overview: This sprint we finished up integrating all of the netowrking with the rest of the code and finihsed the game logic. Now players can join a room and when two player are in it, a connect4 game automatically starts. 

### List of Changes:
1. Moving the ball back to a starting position when you make a basket
2. Detecting when you miss a basket.
3. Moving the ball back to a starting position when you miss a basket.
4. Bug fixes in the connect4 detection scripts
5. Updated and improved the game physics and throwing mechanics
6. Graphic improvemnets (finally rid of the soccerball)
7. Networking improvements
8. Game functioning in practice mode and actual game play.
9. Join room with friends


### Location for Changes:
All changes have been moved into the main branch. There are 3 main scenes. The NewMainMenu, NewGameLobby, and PracticeMode are in the main loop. They are the main menu, lobby listing, and then the main game enviornment. The Connect4Manager, NetworkManager, and the Reaction Script are where most of the logic is written. 


### Individual Contributions:

Hannah: Found and fixed bugs in the connect4 detection functions. Implemented functionality that allows for the 'Game Over' screen to display the team color of the winner. Made it possible to detect when a shot is missed. Implemented functionality such that after a turn is complete (shot taken, missed or made) the ball will spawn back to a designated place next to the player. Worked with other team members to complete networking and resolve functionality issues.

Ben: Implemented new UI for lobby screen to sync with networking and allow users to join/leave rooms. Moved assets from placeholder game environment screen into arena for our official game mode. Worked with other team members to complete networking and resolve functionality issues.

Ethan: I worked mostly on integrating networking with the rest of the project. Most of my work this sprint revolved around making sure players could see each other and that everything was networked. This involed integrating the join room and lobby funtionatlty, as well as netowrking all the objects and making sure the player characters were spawned correctly. It also involved making sure both players had consistent information locally on their clients for use in the game. I also helped with building out the UI and general Debugging. 

Will: Implemented ball physics by adjusting UI Grab / Throw settings, which include velocity and angular velocity adjustments. Helped design the game environemnt, which included placing the two team ball spawns and player spawns.

