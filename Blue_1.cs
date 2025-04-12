using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            // поля
            private string _name;
            protected int _votes;

            // свойства (что разрешено делать с нашими полями)
            public string Name => _name;
            public int Votes => _votes;

            // конструкторы (название как и у структуры)
            public Response(string name)
            {
                _name = name;    //проинициализировали поля                
                _votes = 0;

            }

            //метод
            public virtual int CountVotes(Response[] responses) //массив ответов (responses), где каждый элемент — это объект Response, содержащий имя и фамилию.
            {
                if (responses == null) { return 0; }
                int count = 0;
                foreach (var response in responses) //var автоматически определяет тип переменной
                {
                    if (response.Name == this.Name)
                    {
                        count++;
                    }
                }
                this._votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Кандидат: {Name}, Голосов: {Votes}");
            }

        }
        public class HumanResponse : Response //класс-наследник
        {            
            private string _surname;            
            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }
            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;

                int count = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse &&                       humanResponse.Name == this.Name &&
                        humanResponse.Surname == this.Surname)
                    {
                        count++;
                    }
                }
                this._votes = count;
                return count;
            }
            public override void Print()
            {
                Console.WriteLine($"Кандидат: {Name} {Surname}, Голосов: {Votes}");
            }
        }
    }
}
