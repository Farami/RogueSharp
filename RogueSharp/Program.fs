open System
open Types
open Graphics
open Interface
open World
open Input
open Enemy

let rec gameLoop world =
  drawWorld world
  drawPlayer world
  drawInterface world
  match world |> incrementTurn |> moveEnemies |> handleInput with
  | World world -> gameLoop world
  | NextLevel -> () // todo
  | QuitGame -> ()

  
[<EntryPoint>]
let main argv = 
  Console.CursorVisible <- false
  gameLoop defaultWorld
  0 // return an integer exit code