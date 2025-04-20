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

