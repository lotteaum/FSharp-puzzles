(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)

(* Lien du puzzle : https://www.codingame.com/training/medium/telephone-numbers *)

open System
open System.Collections.Generic

type Arbre =
    {chiffre : char;
    desc : List<Arbre>}

let arbreInit (c:char) =
    (* Crée un arbre sans descendant contenant le caractère donné en entrée *)
    {chiffre = c ;
    desc = new List<Arbre>()}

let rec insertNum (num:string) (a:byref<Arbre>) =
    (* Crée les noeuds nécessaires de l'arbre lorsqu'on ajoute un nouveau numéro de téléphone *)
    if (String.length num)>0 then
        let c = num.[0]
        let mutable chiffreAlreadyPresent = false
        for i in 0 .. a.desc.Count - 1 do
            let mutable branche = a.desc.Item i
            let bKey = branche.chiffre
            if (c=bKey) then
                chiffreAlreadyPresent <- true
                insertNum num.[1..] &branche
        if not chiffreAlreadyPresent then
            let mutable newBranche = arbreInit c
            insertNum num.[1..] &newBranche
            a.desc.Add(newBranche)

let rec tailleArbre (a:Arbre) = 
    (* Renvoie les nombres de noeuds d'un arbre *)
    if (a.desc.Count=0) then 1
    else
        let mutable total = 1
        for i in 0 .. a.desc.Count-1 do
            total <- total + (tailleArbre (a.desc.Item i))
        total

(* Nécessaire pour stocker tous les numéros de téléphone dans un seul arbre *)
let mutable arbre = arbreInit 'x'

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

(* Main *)

let N = int(Console.In.ReadLine())
for i in 0 .. N - 1 do
    let telephone = Console.In.ReadLine()
    insertNum telephone &arbre

(* Affiche la taille de la mémoire nécessaire pour stocker les numéros sous la forme décrite*)
printfn "%i" <| (tailleArbre arbre)-1

