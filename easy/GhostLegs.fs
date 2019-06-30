(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)

(* Lien du puzzle : https://www.codingame.com/training/easy/ghost-legs *)

open System
open System.Collections.Generic

let token = (Console.In.ReadLine()).Split [|' '|]
let W = int(token.[0])
let H = int(token.[1])
eprintfn "%A %A" W H

(* Création de la liste des caractères de départ *)
let mutable line = Console.In.ReadLine()
let listStarts = new List<char>()
for k in 0 .. (W-1)/3 do
    listStarts.Add(line.[3*k])
eprintfn "%A" listStarts

(* Création de la liste des lignes du parcours *)
let listLines = new List<string>()
for i in 1 .. H - 2 do
    line <- Console.In.ReadLine()
    listLines.Add(line)
    eprintfn "%A" line
    ()

(* Création de la liste des caractères d'arrivée *)
let listEnds = new List<char>()
line <- Console.In.ReadLine()
for k in 0 .. (W-1)/3 do
    listEnds.Add(line.[3*k])
eprintfn "%A" listEnds

let nextPosition (nPos:int) (line:string) = 
    match nPos with
    |0 -> if (line.[1]<>' ') then 1 else 0
    |nPos when nPos = ((String.length line)-1)/3 -> if (line.[(String.length line)-2]<>' ') then nPos-1 else nPos
    |_ -> if (line.[3*nPos-1]='-') then nPos-1 elif (line.[3*nPos+1]='-') then nPos+1 else nPos

let findArrival nStart (carte:List<string>) =
    let mutable nArrival = nStart
    for i in 0 .. carte.Count-1 do
        nArrival <- nextPosition nArrival carte.[i]
    nArrival

for i in 0 .. listStarts.Count-1 do
    (* On cherche l'arrivée pour chaque caractère de départ et on affiche
    le mot au format demandé *)
    let j = findArrival i listLines
    printfn "%c%c" listStarts.[i] listEnds.[j]

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)
