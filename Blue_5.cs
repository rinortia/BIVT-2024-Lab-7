using System;

namespace Lab_6
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _flag;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _flag = true;
            }

            public void SetPlace(int place)
            {
                if (_flag)
                {
                    _place = place;
                    _flag = false;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Место: {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public int SummaryScore
            {
                get
                {
                    int score = 0;
                    if (_sportsmen == null) return 0;
                    for (int i = 0; i < _count; i++)
                    {
                        switch (_sportsmen[i].Place)
                        {
                            case 1:
                                score += 5;
                                break;
                            case 2:
                                score += 4;
                                break;
                            case 3:
                                score += 3;
                                break;
                            case 4:
                                score += 2;
                                break;
                            case 5:
                                score += 1;
                                break;
                            default:
                                score += 0;
                                break;
                        }
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int result = 18;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i] != null && _sportsmen[i].Place > 0 && _sportsmen[i].Place < result) result = _sportsmen[i].Place;
                    }
                    return result;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (sportsman == null || _count >= 6) return;
                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                int n = teams.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore ||
                            (teams[j].SummaryScore == teams[j + 1].SummaryScore &&
                             teams[j].TopPlace > teams[j + 1].TopPlace))
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team answer = teams[0];
                double maxStrength = answer.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double strength = teams[i].GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        answer = teams[i];
                    }
                }
                return answer;
            }

            public void Print()
            {
                Console.WriteLine($"Команда: {Name}, Очки: {SummaryScore}, Лучшее место: {TopPlace}, Сила команды: {GetTeamStrength():F2}");
                foreach (var sportsman in _sportsmen)
                {
                    if (sportsman != null)
                        sportsman.Print();
                }
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sum = 0;
                int count = 0;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sum += sportsman.Place;
                        count++;
                    }
                }
                if (count > 0)
                {
                    double average = sum / count;
                    return 100.0 / average;
                }
                else
                {
                    return 0;
                }
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sumPlaces += sportsman.Place;
                        productPlaces *= sportsman.Place;
                        count++;
                    }
                }

                if (productPlaces == 0)
                {
                    return 0;
                }
                else
                {
                    return (100 * sumPlaces * count) / productPlaces;
                }
            }
        }
    }
}