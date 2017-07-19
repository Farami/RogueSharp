module Types

type Position = { x:int; y:int }
type Player = { currentPosition:Position; previousPosition:Position }
type Tile = 
  { position:Position; tile:char }
  override this.ToString() = sprintf "%c" this.tile
type World = { tiles:Tile list; player:Player }
type Input = Up | Down | Left | Right | OpenDoor | Quit