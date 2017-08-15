module Graphics
open System
open Types

let addThree a = a + 3

let resetCursor() = Console.SetCursorPosition(0, 0)

let drawTile tile =
  Console.SetCursorPosition(tile.position.x, tile.position.y)
  Console.Write(tile.tile)

let drawWorld world = 
  List.iter drawTile world

let drawPlayer world =
  let player = world.player
  let tile = List.find (fun t -> t.position = player.previousPosition) world.tiles

  Console.SetCursorPosition(player.previousPosition.x, player.previousPosition.y)
  Console.Write(tile.tile);
  Console.SetCursorPosition(player.currentPosition.x, player.currentPosition.y)
  Console.Write('@')

let drawOpenDoor position =
  Console.SetCursorPosition(position.x, position.y)
  Console.Write('-')