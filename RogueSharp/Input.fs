module Input

open System
open Types
open World

let rec getInput () =
  let input = Console.ReadKey(true)
  match input.Key with
  | ConsoleKey.UpArrow -> Up
  | ConsoleKey.DownArrow -> Down
  | ConsoleKey.LeftArrow -> Left
  | ConsoleKey.RightArrow -> Right
  | ConsoleKey.Spacebar -> OpenDoor
  | ConsoleKey.A -> Attack
  | ConsoleKey.Q -> Input.Quit
  | _ -> getInput ()

let handleInput world =
  let input = getInput ()
  match input with
  | Quit -> QuitGame
  | OpenDoor -> World(setPlayerState world State.Open)
  | Attack -> World(setPlayerState world State.Attack)
  | _ -> match world.player.state with
         | Open -> World(openDoor world input)
         | State.Attack -> World(setPlayerState world State.Idle) // todo
         | _ -> 
            let world = { world with player = (move world input) }
            match (getTile world.tiles world.player.currentPosition).tileType with
            | Exit -> NextLevel
            | _ -> World(world)