(* The while loop represents the game. *)
(* Each iteration represents a turn of the game *)
(* where you are given inputs (the heights of the mountains) *)
(* and where you have to print an output (the index of the mountain to fire on) *)
(* The inputs you are given are automatically updated according to your last actions. *)

(* Lien : https://www.codingame.com/training/easy/the-descent
  Cliquer sur Resoudre, et l'énoncé se trouve à gauche.
  Choisir F# comme langage de programmation *)

open System

(* game loop *)
while true do
    let mutable maxi = int(Console.In.ReadLine())
    let mutable indice = 0
    for i in 1 .. 8 - 1 do
        let mountainH = int(Console.In.ReadLine()) (* represents the height of one mountain. *)
        if mountainH > maxi then
            maxi <- mountainH
            indice <- i

    
    (* Write an action using printfn *)
    (* To debug: eprintfn "Debug message" *)
    
    printfn "%A" indice (* The index of the mountain to fire on. *)
    ()
