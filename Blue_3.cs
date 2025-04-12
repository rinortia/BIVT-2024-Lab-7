using System;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltytimes == null) return null;
                    int[] copyArr = new int[_penaltytimes.Length];
                    Array.Copy(_penaltytimes, copyArr, _penaltytimes.Length);
                    return copyArr;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltytimes == null) return 0;
                    int total = 0;
                    foreach (var p in _penaltytimes)
                    {
                        total += p;
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                        return false;

                    foreach (var time in _penaltytimes)
                    {
                        if (time == 10)
                            return true;
                    }

                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0) return;

                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                bool flag;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    flag = false;
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                            flag = true;
                        }
                    }
                    if (!flag)
                        break;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} - {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                        return false;

                    int foulsOverFive = 0;
                    int totalFouls = 0;

                    foreach (var foul in _penaltytimes)
                    {
                        if (foul == 5) foulsOverFive++;
                        totalFouls += foul;
                    }

                    double matchCount = _penaltytimes.Length;
                    return foulsOverFive > 0.1 * matchCount || totalFouls > 2 * matchCount;
                }
            }

            public override void PlayMatch(int foul)
            {
                if (foul < 0 || foul > 5) return;
                base.PlayMatch(foul);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _totalPenaltyTimeAllPlayers = 0;
            private static int _numberHockeyPlayers = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _numberHockeyPlayers++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                        return false;

                    foreach (var time in _penaltytimes)
                    {
                        if (time == 10) return true;
                    }

                    if (_numberHockeyPlayers == 0) return false;

                    double avg = (double)_totalPenaltyTimeAllPlayers / _numberHockeyPlayers;
                    return Total > 0.1 * avg;
                }
            }

            public override void PlayMatch(int penaltyTime)
            {
                if (penaltyTime < 0 || penaltyTime > 10) return;

                base.PlayMatch(penaltyTime);
                _totalPenaltyTimeAllPlayers += penaltyTime;
            }
        }

    }
}
