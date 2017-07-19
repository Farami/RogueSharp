open System
open Types
open Graphics

let template = ["#####################";
                "#                   #";
                "#                   ##########################";
                "#                   +        +               #";
                "#                   ##########               #";
                "#                   #        #               #";
                "#                   #        #               #";
                "#####################        #################"]


let createTile y x char = { position = { x = x; y = y }; tile = char }
let parseTiles (list:string list) = 
   list
   |> Seq.mapi (fun y s -> Seq.mapi (createTile y) s)
   |> Seq.concat 
   |> Seq.toList

let rec getInput () =
  let input = Console.ReadKey(true)
  match input.Key with
  | ConsoleKey.UpArrow -> Up
  | ConsoleKey.DownArrow -> Down
  | ConsoleKey.LeftArrow -> Left
  | ConsoleKey.RightArrow -> Right
  | ConsoleKey.Spacebar -> OpenDoor
  | ConsoleKey.Q -> Quit
  | _ -> getInput ()

let getNextCoordinates player direction =
  match direction with
  | Up -> { x = player.currentPosition.x; y = player.currentPosition.y - 1 }
  | Down -> { x = player.currentPosition.x; y = player.currentPosition.y + 1 }
  | Left -> { x = player.currentPosition.x - 1; y = player.currentPosition.y }
  | Right ->  { x = player.currentPosition.x + 1; y = player.currentPosition.y }
  | OpenDoor -> player.currentPosition
  | Quit -> player.currentPosition

let openDoor world =
  let input = getInput() // first input sets state to openDoor, second input tells which direction the door is supposed to be in
  let coordinates = getNextCoordinates world.player input
  let tile = List.find (fun t -> t.position = coordinates) world.tiles
  if tile.tile = '+' then drawOpenDoor tile.position
  List.map (fun x -> match x.position = coordinates && x.tile = '+' with
                     | true -> { x with tile = '-' }
                     | false -> x) world.tiles
 
let move input world =
  let player = world.player
  let newCoordinates = getNextCoordinates player input
  let tile = List.find (fun t -> t.position = newCoordinates) world.tiles
  match tile.tile with
  | '#' -> player
  | '+' -> player
  | _ -> { player with currentPosition = newCoordinates; previousPosition = player.currentPosition }

let rec gameLoop world =
  Console.Clear()
  drawWorld world.tiles
  drawPlayer world.player
  resetCursor()
  let input = getInput()
  match input with
  | Quit -> ()
  | OpenDoor -> { world with tiles = (openDoor world) } |> gameLoop
  | _ -> { world with player = (move input world) } |> gameLoop
  
[<EntryPoint>]
let main argv = 
  let world = { tiles = parseTiles template; player = { currentPosition = { x = 2; y = 2 }; previousPosition = { x = 2; y = 2 } } }
  gameLoop world
  0 // return an integer exit code