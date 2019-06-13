(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)

(* Lien : https://www.codingame.com/training/medium/conway-sequence
  Cliquer sur Resoudre, et l'énoncé se trouve à gauche.
  Choisir F# comme langage de programmation *)

open System
open System.Collections.Generic

let R = int(Console.In.ReadLine())
let L = int(Console.In.ReadLine())

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

let mutable arrayI = [| R |]

let nextArray (arrayInt:array<int>) = 
    let mutable nombre = 1
    let mutable lastInt = arrayInt.[0]
    let lTemp = new List<int32>()
    for i = 1 to (arrayInt.Length-1) do
        if arrayInt.[i]<>lastInt then
            lTemp.Add(nombre)
            lTemp.Add(lastInt)
            nombre <- 1
            lastInt<- arrayInt.[i]
        else
            nombre <- nombre+1
    lTemp.Add(nombre)
    lTemp.Add(lastInt)
    lTemp.ToArray()

let arrayToString (a:array<int>) =
    let mutable s = ""
    s <- sprintf "%i" a.[0]
    for i = 1 to a.Length-1 do
        s <- sprintf "%s %i" s a.[i]
    (*eprintfn "%s" s*)
    s

eprintfn "%i" R

for i = 2 to L do
    arrayI <- nextArray arrayI
    let s = arrayToString arrayI
    eprintfn "%s" s

eprintfn "%i" arrayI.Length
printfn "%s" <| arrayToString arrayI
