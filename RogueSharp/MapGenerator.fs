module MapGenerator

type Tiles = Floor | Wall
type Direction = North | East | South | West

let createTile tile =
  match tile with
  | Floor -> ' '
  | Wall -> '#'

let generateTile x y xMax yMax =
  match (x=xMax || x=0, y=yMax || y=0) with
  | (true, false) | (false, true) | (true, true) -> createTile Wall
  | (false, false) -> createTile Floor

let generateRow row y width height = List.map (fun x -> generateTile x y width height) row
let createRoom width height = List.map (fun y -> generateRow [0..width] y width height) [0..height]

let randomDirection = 
  let random = System.Random()
  match random.Next(0,4) with
  | 0 -> North
  | 1 -> East
  | 2 -> South
  | 3 | _ -> West

