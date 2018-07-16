module Graphics
open System
open Types

let resetCursor() = Console.SetCursorPosition(0, 0)

let yOffset = 4

let drawTile tile =
  Console.SetCursorPosition(tile.position.x, tile.position.y + yOffset)
  Console.Write(tile.tile)

let drawWorld world = 
  List.iter drawTile world.tiles

let drawPlayer world =
  let player = world.player
  let tile = List.find (fun t -> t.position = player.previousPosition) world.tiles

  Console.SetCursorPosition(player.previousPosition.x, player.previousPosition.y + yOffset)
  Console.Write(tile.tile);
  Console.SetCursorPosition(player.currentPosition.x, player.currentPosition.y + yOffset)
  Console.Write('@')

let drawOpenDoor position =
  Console.SetCursorPosition(position.x, position.y + yOffset)
  Console.Write('-')

let clearRow row =
  let currentLineCursor = Console.CursorTop;
  Console.SetCursorPosition(0, row)
  Console.Write(new string(' ', Console.WindowWidth))
  Console.SetCursorPosition(0, currentLineCursor)

let clearRows rows =
  List.iter clearRow rows