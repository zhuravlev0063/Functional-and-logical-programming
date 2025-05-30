% Факты о поле членов семьи
man(anatoliy).
man(dimitriy).
man(vlad).
man(kirill).
man(mefodiy).
woman(galya).
woman(sveta).
woman(vladina).
woman(zoya).
woman(katrin).

% Факты о родительских отношениях
parent(anatoliy, dimitriy).
parent(galya, dimitriy).
parent(anatoliy, vladina).
parent(galya, vladina).
parent(dimitriy, kirill).
parent(sveta, kirill).
parent(dimitriy, mefodiy).
parent(sveta, mefodiy).
parent(vlad, zoya).
parent(vladina, zoya).
parent(vlad, katrin).
parent(vladina, katrin).

% Предикат для вывода всех мужчин
men :- man(X), write(X), nl, fail.
men.

% Предикат для вывода всех женщин
women :- woman(X), write(X), nl, fail.
women.

% Предикат для вывода детей X
children(X) :- parent(X, Y), write(Y), nl, fail.
children(_).

% Предикат для проверки, является ли X матерью Y
mother(X, Y) :- woman(X), parent(X, Y).

% Предикат для вывода матери X
mother(X) :- parent(Mother, X), woman(Mother), write(Mother).

% Предикат для проверки, является ли X братом Y
brother(X, Y) :- man(X), X \= Y, parent(P, X), parent(P, Y).

% Предикат для вывода всех братьев X
brothers(X) :- parent(P, X), parent(P, Y), man(Y), X \= Y, write(Y), nl, fail.
brothers(_).

% Предикат для проверки, являются ли X и Y братьями/сёстрами
b_s(X, Y) :- parent(P, X), parent(P, Y), X \= Y.

% Предикат для вывода всех братьев и сестёр X
b_s(X) :- parent(P, X), parent(P, Y), X \= Y, write(Y), nl, fail.
b_s(_).

% --- Вариант 7 ---
% --- Задание 2 ---

% 1. Проверка, является ли X отцом Y
father(X, Y) :- man(X), parent(X, Y).

% 2. Вывод отца X
father(X) :- parent(Father, X), man(Father), write(Father).

% 3. Проверка, является ли X сестрой Y
sister(X, Y) :- woman(X), X \= Y, parent(P, X), parent(P, Y).

% 4. Вывод всех сестёр X
sisters(X) :- parent(P, X), parent(P, Y), woman(Y), X \= Y, write(Y), nl, fail.
sisters(_).

 % --- Задание 3 ---

% 1. Проверка, является ли X дедушкой Y
grand_pa(X, Y) :- 
    man(X),
    parent(X, Parent),
    parent(Parent, Y).

% 2. Вывод всех дедушек X
grand_pas(X) :- 
    parent(GrandPa, Parent),
    man(GrandPa),
    parent(Parent, X),
    write(GrandPa), nl,
    fail.
grand_pas(_).

% 3. Проверка связи "дедушка-внук" в любом порядке
grand_pa_and_son(X, Y) :- 
    grand_pa(X, Y); 
    grand_pa(Y, X).

% 4. Проверка, является ли X дядей Y
uncle(X, Y) :- 
    man(X),
    parent(Parent, Y),
    brother(X, Parent).

% 5. Вывод всех дядей X
uncles(X) :- 
    parent(Parent, X),
    brother(Uncle, Parent),
    write(Uncle), nl,
    fail.
uncles(_).