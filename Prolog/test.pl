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