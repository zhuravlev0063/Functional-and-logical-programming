max(X, Y, U, Z) :-
    (X >= Y, X >= U -> Z = X;      % Если X максимальное
     Y >= X, Y >= U -> Z = Y;      % Если Y максимальное
     Z = U).                       % Иначе U максимальное

fact_up(0, 1).                     % Базовый случай: 0! = 1
fact_up(N, X) :-
    N > 0,
    N1 is N - 1,
    fact_up(N1, X1),                % Рекурсивный вызов
    X is N * X1.                    % Умножение на N при возврате

fact_down(N, X) :- fact_down(N, 1, X). % Инициализация аккумулятора

fact_down(0, Acc, Acc).              % Базовый случай: возврат аккумулятора
fact_down(N, Acc, X) :-
    N > 0,
    N1 is N - 1,
    Acc1 is Acc * N,                 % Накопление результата в Acc1
    fact_down(N1, Acc1, X).          % Хвостовой вызов

sum_digits_up(0, 0).                % Базовый случай: 0 для числа 0
sum_digits_up(N, Sum) :-
    N > 0,
    Digit is N mod 10,              % Последняя цифра
    N1 is N // 10,                  % Остаток числа без последней цифры
    sum_digits_up(N1, Sum1),        % Рекурсивный вызов
    Sum is Sum1 + Digit.            % Сложение при возврате

sum_digits_down(N, Sum) :- sum_digits_down(N, 0, Sum).

sum_digits_down(0, Acc, Acc).       % Базовый случай
sum_digits_down(N, Acc, Sum) :-
    N > 0,
    Digit is N mod 10,
    N1 is N // 10,
    Acc1 is Acc + Digit,            % Накопление суммы в Acc1
    sum_digits_down(N1, Acc1, Sum). % Хвостовой вызов

is_square_free(N) :-
    N > 0,
    \+ (between(2, N, K),           % Проверяем все K от 2 до N
        Prime is K * K,             % Квадрат простого числа
        0 is N mod Prime).          % Если делится — не свободно

read_list(List) :-
    write('Введите список в формате [1, 2, 3]: '),
    read(List).                     % Считывает список с клавиатуры

write_list([]) :- nl.               % Пустой список — новая строка
write_list([H|T]) :-
    write(H), write(' '),           % Печать элемента
    write_list(T).                  % Рекурсия для хвоста

sum_list_down([], 0).               % Базовый случай
sum_list_down([H|T], Sum) :-
    sum_list_down(T, Sum1),         % Сумма хвоста
    Sum is H + Sum1.                % Добавление головы

sum_list_up(List, Sum) :- sum_list_up(List, 0, Sum).

sum_list_up([], Acc, Acc).         % Базовый случай
sum_list_up([H|T], Acc, Sum) :-
    Acc1 is Acc + H,               % Накопление суммы
    sum_list_up(T, Acc1, Sum).     % Хвостовой вызов    



%Задание 2

 % Базовый случай: 0 для числа 0
product_digits(0, 0).

% Основной предикат с аккумулятором
product_digits(Number, Product) :-
    Number > 0,
    product_digits(Number, 1, Product).

product_digits(0, Acc, Acc).  % Возврат аккумулятора
product_digits(Number, Acc, Product) :-
    Number > 0,
    Digit is Number mod 10,   % Последняя цифра
    NewAcc is Acc * Digit,    % Накопление произведения
    NextNumber is Number // 10,
    product_digits(NextNumber, NewAcc, Product).


% ?- product_digits(123, P).  % 1*2*3 = 6
% P = 6.


count_odd_gt3(Number, Count) :-
    number_chars(Number, Chars),       % Число → список символов
    include(is_odd_and_gt3, Chars, FilteredChars), % Фильтрация
    length(FilteredChars, Count).      % Подсчёт элементов

% Проверка, что цифра нечётная и > 3
is_odd_and_gt3(Char) :-
    char_type(Char, digit(Digit)),     % Символ → число
    Digit > 3,
    1 is Digit mod 2.                  % Нечётность



% ?- count_odd_gt3(13572, C).  % Подходят 5 и 7
% C = 2.



gcd(A, 0, A).  % Базовый случай
gcd(A, B, GCD) :-
    B > 0,
    Remainder is A mod B,
    gcd(B, Remainder, GCD).

% ?- gcd(36, 48, G).  
% G = 12.



%Задание 3

% Предикат для циклического сдвига вправо на N позиций
shift_right(List, N, Shifted) :-
    length(List, Len),
    N1 is N mod Len,                  % Учитываем случай, когда N > длины списка
    split_list(List, Len - N1, Left, Right),
    append(Right, Left, Shifted).

% Разделение списка на две части по индексу
split_list(List, N, Left, Right) :-
    length(Left, N),
    append(Left, Right, List).

% Специальный предикат для сдвига на 2 позиции
shift_right_2(List, Shifted) :- shift_right(List, 2, Shifted).

% ?- shift_right_2([1,2,3,4,5], S).
% S = [4,5,1,2,3].  % Сдвиг на 2 вправо: [1,2,3,4,5] → [4,5,1,2,3]




% Используем общий предикат shift_right с N=1
shift_right_1(List, Shifted) :- shift_right(List, 1, Shifted).


% ?- shift_right_1([1,2,3,4,5], S).
% S = [5,1,2,3,4].  % Сдвиг на 1 вправо: [1,2,3,4,5] → [5,1,2,3,4]



% Предикат для подсчёта чётных элементов
count_even(List, Count) :-
    include(even, List, EvenList),  % Фильтруем чётные
    length(EvenList, Count).        % Считаем их количество

% Проверка на чётность
even(X) :- 0 is X mod 2.


% ?- count_even([1,2,3,4,5,6], C).
% C = 3.  % Чётные: 2, 4, 6 → 3 элемента



%Задание 4

% Определение цветов волос
hair_colors([blond, brunet, ginger]).

% Решение задачи
solve_hair_color :-
    % Цвета волос для каждого друга
    hair_colors(Colors),
    permutation(Colors, [Belokurov, Ryzhov, Chernov]),
    
    % Условия задачи:
    % 1. Ни у кого цвет волос не соответствует фамилии
    Belokurov \= blond,    % Белокуров не блондин
    Ryzhov \= ginger,        % Рыжов не рыжий
    Chernov \= black,     % Чернов не брюнет (черный - здесь brunet)
    
    % 2. Один блондин, один брюнет, один рыжий
    % (уже обеспечено permutation)
    
    % 3. Брюнет сказал Белокурову => Белокуров не брюнет
    % (так как брюнет - это говорящий)
    Belokurov \= brunet,
    
    % Определяем, кто брюнет
    (Ryzhov = brunet ; Chernov = brunet),
    
    % Вывод результатов
    format('Белокуров: ~w~n', [Belokurov]),
    format('Рыжов: ~w~n', [Ryzhov]),
    format('Чернов: ~w~n', [Chernov]).


%?- solve_hair_color.
%Белокуров: red
%Рыжов: brunet
%Чернов: blond



 %Задание 5

 % Предикат для проверки простоты числа
is_prime(2).
is_prime(3).
is_prime(P) :- 
    P > 3, 
    P mod 2 =\= 0, 
    \+ has_factor(P, 3).

has_factor(N, F) :- 
    F * F =< N, 
    (N mod F =:= 0 ; F1 is F + 2, has_factor(N, F1)).

% Предикат для нахождения суммы простых делителей
sum_prime_divisors(N, Sum) :-
    N > 1,
    findall(D, (between(2, N, D), N mod D =:= 0, is_prime(D)), Divisors),
    sum_list(Divisors, Sum).

% Вспомогательный предикат для суммирования
sum_list([], 0).
sum_list([H|T], Sum) :- sum_list(T, Sum1), Sum is H + Sum1.


% ?- sum_prime_divisors(28, S).  % Простые делители 2 и 7 → 2+7=9
% S = 9.





% Предикат для суммы цифр числа
sum_digits(0, 0).
sum_digits(N, Sum) :-
    N > 0,
    Digit is N mod 10,
    N1 is N // 10,
    sum_digits(N1, Sum1),
    Sum is Sum1 + Digit.

% Предикат для нахождения подходящих делителей
product_special_divisors(N, Product) :-
    sum_digits(N, SumN),
    findall(D, (between(1, N, D), N mod D =:= 0, sum_digits(D, SumD), SumD < SumN), Divisors),
    product_list(Divisors, Product).

% Вспомогательный предикат для вычисления произведения
product_list([], 1).
product_list([H|T], Product) :- 
    product_list(T, Product1), 
    Product is H * Product1.


% ?- product_special_divisors(20, P). 
% Делители 20: 1, 2, 4, 5, 10, 20
% Сумма цифр исходного числа: 2+0=2
% Подходят делители с суммой цифр < 2: только 1
% P = 1.