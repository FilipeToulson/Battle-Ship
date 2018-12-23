# Battle-Ship
### About
Layout 5 ships on a 10 by 10 grid, and take out the AI's ships before it takes your's out.

Currently the AI only uses random numbers to pick coordinates to shoot at, but more advanced AI will be added in the future.

### Compiling and Running
#### Windows
Download [Visual Studio](https://visualstudio.microsoft.com/downloads/).

Using a developer command prompt, move to the directory containning the project files.

To compile, run the following command:
```
csc *.cs
```

To run the game, run the following command:
```
BattleShip
```

#### Mac and Linux
Download [Mono](https://www.mono-project.com/download/stable/).

Using terminal, move to the directory containning the project files.

To compile, run the following command:
```
msc *.cs -out:BattleShip
```

To run the game, run the following command:
```
mono BattleShip
```
