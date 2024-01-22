Feladatleírás
Készítsünk programot, amellyel a következő játékot játszhatjuk. 
Adott egy 𝑛 × 𝑛 elemből álló játékpálya, amely falakból és padlóból áll, valamint őrök járőröznek rajta. A játékos feladata, hogy a kiindulási pontból eljusson a kijáratig úgy, hogy közben az őrök nem
látják meg. Természetesen a játékos, illetve az őrök csak a padlón tudnak járni. 
Az őrök adott időközönként lépnek egy mezőt (vízszintesen, vagy függőlegesen) 
úgy, hogy folyamatosan előre haladnak egészen addig, amíg falba nem ütköznek. 
Ekkor véletlenszerűen választanak egy új irányt, és arra haladnak tovább. Az őr 
járőrözés közben egy 2 sugarú körben lát (azaz egy 5 × 5-ös négyzetet), ám a 
falon nem képes átlátni. 
A játékos a pálya előre megadott pontján kezd, és vízszintesen, illetve 
függőlegesen mozoghat (egyesével) a pályán. 
A pályák méretét, illetve felépítését (falak és kijárat helyzete, játékos és őrök 
kezdőpozíciója) tároljuk fájlban. A program legalább 3 különböző méretű pályát 
tartalmazzon. 
A program biztosítson lehetőséget új játék kezdésére a pálya kiválasztásával, 
valamint játék szüneteltetésére (ekkor nem telik az idő, és nem léphet a játékos). 
Továbbá ismerje fel, ha vége a játéknak. Ekkor jelenítse meg, hogy győzött, vagy 
veszített-e a játékos.


Elemzés:
•	A játékot három pályán játszhatjuk: kicsi (8 * 8-as) közepes (12 * 12-es), nagy (16 * 16-os). A program indításkor a nagy pálya az alapértelmezett.
•	A feladatot egyablakos asztali alkalmazásként Windows Presentation Foundation 
grafikus felülettel valósítjuk meg.
•	Az ablakban elhelyezünk egy menüt a következő menüpontokkal: Új játék (Kicsi pálya, Közepes pálya, Nagy pálya), Kilépés, Megállít, Újraindít.
•	A játéktáblát egy picture boksz-okból álló rács reprezentálja. A játékosnak arany színe van, míg az őrnek kék, a kijáratnak fehér és a falnak barna. A játékos a karaktert az „awsd” gombok segítségévek tudja fel le oldalra irányítani.
•	A felhasználói esetek az 1. ábrán láthatóak.
