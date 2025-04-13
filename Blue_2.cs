using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
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
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return null;

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
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;
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
                if (array == null || array.Length <= 1) return;
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

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;

            }
            public void Add(Participant[] participants)
            {
                if (participants != null || participants.Length != 0)
                {
                    foreach (var participant in participants)
                    {
                        Add(participant);
                    }
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
                    if (Participants.Length <= 3 || this.Participants == null)
                    {
                        return new double[0];
                    }
                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5;
                    prizes[1] = Bank * 0.3;
                    prizes[2] = Bank * 0.2;

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
                    if (Participants == null || Participants.Length < 3)
                    {
                        return new double[0];
                    }

                    var sortedParticipants = Participants.OrderByDescending(p => p.TotalScore).ToArray();

                    int halfCount = sortedParticipants.Length / 2;
                    int prizeCount = Math.Max(3, Math.Min(halfCount, 10));

                    double[] rewards = new double[prizeCount];

                    double baseReward = Bank * 0.20 / prizeCount;
                    for (int i = 0; i < prizeCount; i++)
                    {
                        rewards[i] = baseReward;
                    }

                    if (prizeCount > 0) rewards[0] += Bank * 0.40;
                    if (prizeCount > 1) rewards[1] += Bank * 0.25;
                    if (prizeCount > 2) rewards[2] += Bank * 0.15;

                    return rewards;

                }
            }
        }


    }
}





