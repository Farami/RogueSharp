module World

open Types
open Enemy

let template = ["#####################";
                "#                   #";
                "#                   ##########################";
                "#                   +        +           b   #";
                "#                   ##########               #";
                "#                   #        #            >  #";
                "#                   #        #               #";
                "#####################        #################"]


let replaceTiles world tiles = { world with tiles = tiles }

let createTile y x char = 
  { 
  position = { x = x; y = y } 
  tile = char
  tileType = match char with
             | '#' -> Wall
             | '+' -> Door
             | '>' -> Exit
             | 'b' -> Enemy(Bat)
             | _ -> Floor
  }
let parseTiles (list:string list) = 
   list
   |> Seq.mapi (fun y s -> Seq.mapi (createTile y) s)
   |> Seq.concat
   |> Seq.toList

let getNewPosition position direction =
  match direction with
  | Up -> { x = position.x; y = position.y - 1 }
  | Down -> { x = position.x; y = position.y + 1 }
  | Left -> { x = position.x - 1; y = position.y }
  | Right ->  { x = position.x + 1; y = position.y }
  | _ -> position

let getNextCoordinates player direction = getNewPosition player.currentPosition direction

//let getAdjacentTiles world position =
//  let adjacentCoordinates = List.map (fun x -> getNewPosition position x) [Up; Down; Left; Right] 

let getTile tiles coordinates = List.find (fun t -> t.position = coordinates) tiles

let openDoor world input =
  let coordinates = getNextCoordinates world.player input
  let replaceDoor = List.map (fun x -> if x.position = coordinates && x.tile = '+' then { x with tile = '-'; tileType = Floor } else x)
  replaceDoor world.tiles
  |> replaceTiles world
  |> setPlayerIdle

let move world input =
  let world = setPlayerState world State.Idle
  let player = world.player
  let newCoordinates = getNextCoordinates player input
  let tile = getTile world.tiles newCoordinates
  match tile.tileType with
  | Wall -> player
  | Door -> player
  | _ -> setPlayerPosition player newCoordinates

let incrementTurn world = { world with turns = world.turns + 1 }

let defaultWorld = 
  { 
    tiles = parseTiles template
    turns = 0
    enemies = [createEnemy Bat { x = 20; y = 20 }]
    player = { 
             Player.currentPosition = { x = 2; y = 2 }
             previousPosition = { x = 2; y = 2 }
             health = 100
             attack = 1
             state = State.Idle 
             } 
  }