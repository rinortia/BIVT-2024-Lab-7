using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Blue_2
    {
        public struct Participant
        {
            // поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _counterJump;
            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) { return null; }
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];

                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }

                    return copy;
                }
            }   
            public int TotalScore //если есть только геттер, то свойство только для чтения
            {
                get //возвращает вычисленное значение
                {
                    if (_marks == null) return 0;
                    int total = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            total += _marks[i, j];
                        }
                    }
                    return total;
                }
            }

            //конструктор
            public Participant(string name, string surname)
            {                
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _counterJump = 0;
            }

            //методы
            public void Jump(int[] result)
            {
                if (result == null || _marks == null || _counterJump >= 2) { return; }

                for (int j = 0; j < 5; j++) 
                {
                    _marks[_counterJump, j] = result[j];
                }
                _counterJump++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) { return; }
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j]; //тип temp должен совпадать с типом элементов массива array
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine("Оценки судей:");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    Console.Write($"Прыжок {i + 1}: ");
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.Write($"{_marks[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Общий балл: {TotalScore}");
                Console.WriteLine();
            }
        }
        public abstract class WaterJump
        {            
            private string _name; 
            private int _bank; 
            private Participant[] _participants;
            private int _participantCount;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0]; 
                _participantCount = 0;
            }

            public void Add(Participant participant)
            {            
                    Array.Resize(ref _participants, _participantCount + 1);
                    _participants[_participantCount] = participant;
                    _participantCount++;
                
            }            
            public void Add(params Participant[] participants)
            {
                if (participants != null)
                {
                    foreach (var participant in participants)
                    {
                        Add(participant); 
                    }
                }
            }

            public void SortParticipants()
            {
                Participant.Sort(_participants.ToArray());
            }
                       
            public void Print()
            {
                Console.WriteLine($"Турнир: {Name}");
                Console.WriteLine($"Призовой фонд: {Bank}");
                Console.WriteLine("Участники:");
                foreach (var participant in _participants)
                {
                    participant.Print();
                }
            }
        }

        
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return new double[0]; // Если участников меньше 3, призовые не распределяются
                    }

                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5; // 50% за первое место
                    prizes[1] = Bank * 0.3; // 30% за второе место
                    prizes[2] = Bank * 0.2; // 20% за третье место

                    return prizes;
                }
            }
        }

        
        public class WaterJump5m : WaterJump
        {
            
            public WaterJump5m(string name, int bank) : base(name, bank) { }
                       
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return new double[0]; // Если участников меньше 3, призовые не распределяются
                    }

                    int topCount = Math.Min(Math.Max(Participants.Length / 2, 3), 10); // Количество участников выше середины
                    double[] prizes = new double[topCount + 3]; // Призовые для топ-участников и первых трех мест

                    // Распределение призовых для топ-участников
                    double topPrize = Bank * 0.2 / topCount; // 20% фонда делим на количество топ-участников
                    for (int i = 0; i < topCount; i++)
                    {
                        prizes[i] = topPrize;
                    }

                    // Распределение призовых для первых трех мест
                    prizes[topCount] = Bank * 0.4; // 40% за первое место
                    prizes[topCount + 1] = Bank * 0.25; // 25% за второе место
                    prizes[topCount + 2] = Bank * 0.15; // 15% за третье место

                    return prizes;
                }
            }
        }

    }
}    
