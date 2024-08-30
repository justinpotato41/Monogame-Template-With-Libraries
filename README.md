# Monogame-Template-With-Libraries
A Monogame template with different libraries I made for myself to reuse.
This template was made for learning purposes and to play around with.

## How to install
* Make a new Monogame project (assuming you have monogame installed on your machine).
  Open a terminal in the location you wish to make the project
  and run the following commands:
```
dotnet new sln -n MyGame
dotnet new mgdesktopgl -n MyGame
dotnet sln add MyGame
dotnet restore
```
* Now, with the monogame project created, enter the MyGame directory:
```
cd MyGame
```
* now clone this repository with git:
```

```
This will put the folders with the necessary code into the project.

## How to use
* You'll neet either .net 6 or .net 8

* The template comes with a working example with two scenes

* In the Game1 class you can add new scenes and change settings about the window and drawing

* The best thing is to play around and see how things work though

## Instructions
#### The "CODE" forlder has different files for the setup:
* Entity.cs handles the entity and its components
* Globals.cs is a useful class with stuff that can be accessed anywhere
* Scene.cs handles scenes. To make a new Scene make a new .cs script and inherit from "Scene"
* SceneManager.cs handles the scenes that are added in the Game1 class
* Tags.cs is where you put you tags that are used across the project

#### The "Components" folder holds two scripts:
* Component.cs is an abstract class that has the entity it is attached on. to create a new component inherit from this
* SpriteRendere.cs is a basic renderer to display a texture or a region of a texture. It has two constructors

## Fun Part
In the "Content" folder are three important folder:
* The "ART" folder holds the art that you make
* The "SCENES" folder holds all the scenes you make
* The "SCRIPTS" folder holds all the script you make
Of course you dont need to adapt to this conventions

## The following libraries were put together:
* https://github.com/Apostolique/Apos.Camera - camera used
* https://github.com/dotnet-ad/Humper - Humper for AABB collisions
* https://github.com/craftworkgames/MonoGame.Extended - Monogame Extended for other functionalities
