:- dynamic high/2, decl/2, interpret/2, oop/2, cross/2, visual/2, mobile/2, static_typed/2, gc/2.

% --- Вопросы ---
question1(X1) :- write("Язык высокого уровня? (1. Да, 0. Нет)"), nl, read(X1).
question2(X2) :- write("Декларативный язык? (1. Да, 0. Нет)"), nl, read(X2).
question3(X3) :- write("Интерпретируемый язык? (1. Да, 0. Нет)"), nl, read(X3).
question4(X4) :- write("Поддержка ООП? (3. Чисто ООП, 2. Да, 1. Минимальная, 0. Нет)"), nl, read(X4).
question5(X5) :- write("Кроссплатформенный? (1. Да, 0. Нет)"), nl, read(X5).
question6(X6) :- write("Поддержка визуального интерфейса? (3. Да, 1. Через библиотеки, 0. Нет)"), nl, read(X6).
question7(X7) :- write("Используется для мобильной разработки? (1. Да, 0. Нет)"), nl, read(X7).
question8(X8) :- write("Статическая типизация? (1. Да, 0. Нет)"), nl, read(X8).          % Новый вопрос
question9(X9) :- write("Есть сборщик мусора (GC)? (1. Да, 0. Нет)"), nl, read(X9).      % Новый вопрос

% --- Характеристики языков (оригинальные + новые) ---
% Ruby
high(ruby, 1). decl(ruby, 0). interpret(ruby, 1). oop(ruby, 3). cross(ruby, 1). visual(ruby, 2). mobile(ruby, 0). static_typed(ruby, 0). gc(ruby, 1).
% Python
high(python, 1). decl(python, 0). interpret(python, 1). oop(python, 2). cross(python, 1). visual(python, 2). mobile(python, 0). static_typed(python, 0). gc(python, 1).
% C#
high(c_sharp, 1). decl(c_sharp, 0). interpret(c_sharp, 0). oop(c_sharp, 3). cross(c_sharp, 0). visual(c_sharp, 3). mobile(c_sharp, 0). static_typed(c_sharp, 1). gc(c_sharp, 1).
% Java (новый)
high(java, 1). decl(java, 0). interpret(java, 0). oop(java, 3). cross(java, 1). visual(java, 2). mobile(java, 1). static_typed(java, 1). gc(java, 1).
% Go (новый)
high(go, 1). decl(go, 0). interpret(go, 0). oop(go, 1). cross(go, 1). visual(go, 0). mobile(go, 0). static_typed(go, 1). gc(go, 1).
% Rust (новый)
high(rust, 1). decl(rust, 0). interpret(rust, 0). oop(rust, 1). cross(rust, 1). visual(rust, 0). mobile(rust, 0). static_typed(rust, 1). gc(rust, 0).

% --- Главный предикат ---
pr :-
    question1(X1), question2(X2), question3(X3), question4(X4),
    question5(X5), question6(X6), question7(X7), question8(X8), question9(X9),
    high(X, X1), decl(X, X2), interpret(X, X3), oop(X, X4),
    cross(X, X5), visual(X, X6), mobile(X, X7), static_typed(X, X8), gc(X, X9),
    write("Результат: "), write(X).

% --- Предикаты для работы с файлами (из исходного кода) ---
read_str(A) :- get0(X), r_str(X, A, []).
r_str(10, A, A) :- !.
r_str(X, A, B) :- append(B, [X], B1), get0(X1), r_str(X1, A, B1).

high_r(X, Y) :-
    repeat,
    (high(X, Y) -> (put(32), write(X), nl, write(Y), write("."), nl, retract(high(X, Y))) ; (X = nil, Y = nil).

pr2 :- tell('111.txt'), high_r(X, _), X = nil, told.
pr3 :- see('111.txt'), get0(Sym), read_high(Sym), seen.
read_high(-1) :- !.
read_high(_) :- read_str(Lang), name(X, Lang), read(Y), asserta(high(X, Y)), get0(Sym), read_high(Sym).